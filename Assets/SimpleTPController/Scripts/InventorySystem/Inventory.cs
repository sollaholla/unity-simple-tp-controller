using System;
using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Allows for basic inventory management.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemCollection[] m_ItemCollections = null;

        /// <summary>
        /// The item collections within this inventory.
        /// </summary>
        public ItemCollection[] itemCollections
        {
            get { return m_ItemCollections; }
        }

        /// <summary>
        /// Invoked when an item is added to the inventory (and not combined)
        /// </summary>
        public event Action<ItemDataInstance, ItemCollection> itemAdded;

        /// <summary>
        /// Invoked when an item is dropped from the inventory.
        /// </summary>
        public event Action<GameObject> itemDropped;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeCollections();
        }

        protected virtual void InitializeCollections()
        {
            foreach (var collection in m_ItemCollections)
            {
                collection.Initialize(this);
            }
        }

        /// <summary>
        /// Add an inventory item instance to the inventory.
        /// </summary>
        /// <param name="itemInstance">The item instance.</param>
        public virtual void Add(InventoryItemInstance itemInstance)
        {
            var data = new ItemDataInstance(itemInstance);
            if (data.stack <= 0)
            {
                return;
            }

            var collection = GetBestCollectionForItem(data);

            // Combine with existing items in the inventory.
            var existingItems = collection.items
                .Where(x => x.stack < x.item.maxStack && x.item.id == data.item.id)
                .ToArray();

            if (existingItems.Length > 0)
            {
                foreach (var itemData in existingItems)
                {
                    Combine(data, itemData);

                    if (data.stack == 0)
                    {
                        break;
                    }
                }
            }

            // Item has been emptied.
            if (data.stack == 0)
            {
                Destroy(itemInstance.gameObject);
                return;
            }

            // Insert this item into the first available (empty) slot.
            collection.Insert(data);
            itemAdded?.Invoke(data, collection);
        }

        /// <summary>
        /// Drop a given item from the inventory.
        /// </summary>
        /// <param name="itemData">The items data instance.</param>
        /// <param name="dropPoint">The point at which to drop the item.</param>
        /// <param name="dropRotation">The rotation of the dropped item.</param>
        public virtual GameObject DropItem(ItemDataInstance itemData, Vector3 dropPoint, Quaternion dropRotation)
        {
            var fromCollection = itemCollections.FirstOrDefault(x => x.Contains(itemData));
            fromCollection.SetSlot(null, (uint)fromCollection.GetSlot(itemData));

            var objInstance = Instantiate(itemData.item.dropObject, dropPoint, dropRotation);
            itemDropped?.Invoke(objInstance);
            
            return objInstance;
        }

        /// <summary>
        /// Move the item to the given slot in its current collection.
        /// </summary>
        /// <param name="itemData">The item to move.</param>
        /// <param name="slot">The slot to move the item to.</param>
        public virtual void MoveItem(ItemDataInstance itemData, uint slot)
        {
            MoveItem(itemData, itemCollections.FirstOrDefault(x => x.Contains(itemData)), slot);
        }

        /// <summary>
        /// Move an item from its slot in its current collection, to the target collection in the given slot.
        /// </summary>
        /// <param name="itemData">The item to move.</param>
        /// <param name="collection">The collection to move it to.</param>
        /// <param name="slot">The slot to move the item to.</param>
        public virtual void MoveItem(ItemDataInstance itemData, ItemCollection collection, uint slot)
        {
            if (!collection.AllowItem(itemData))
            {
                return;
            }

            var fromCollection = itemCollections.FirstOrDefault(x => x.Contains(itemData));
            var fromSlot = fromCollection.GetSlot(itemData);

            var existingItem = collection.items.ElementAt((int)slot);
            if (existingItem != null)
            {
                if (existingItem.item.id == itemData.item.id && existingItem.stack < existingItem.item.maxStack)
                {
                    Combine(itemData, existingItem);
                }
                else
                {
                    Swap(itemData, fromCollection, existingItem, collection);
                }
            }
            else
            {
                collection.SetSlot(null, (uint)fromSlot);
                collection.SetSlot(itemData, slot);
            }
        }

        /// <summary>
        /// Swaps item1 with item2.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        protected virtual void Swap(
            ItemDataInstance item1, ItemCollection item1Collection, 
            ItemDataInstance item2, ItemCollection item2Collection)
        {
            item1Collection.SetSlot(item2, (uint)item1Collection.GetSlot(item1));
            item2Collection.SetSlot(item1, (uint)item2Collection.GetSlot(item2));
        }

        /// <summary>
        /// Attempts to combine item1 with item2.
        /// </summary>
        /// <param name="item1">The item to combine with item2.</param>
        /// <param name="item2">The item receiving the combination.</param>
        public virtual void Combine(ItemDataInstance item1, ItemDataInstance item2)
        {
            var amount1 = item1.stack;
            var amount2 = item2.stack;

            while (amount2 < item2.item.maxStack && amount1 > 0)
            {
                amount1--;
                amount2++;
            }

            item1.SetStack(amount1);
            item2.SetStack(amount2);
        }

        /// <summary>
        /// Gets the highest priority collection for the given item where the
        /// collection allows the item.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        public virtual ItemCollection GetBestCollectionForItem(ItemDataInstance itemData)
        {
            return Array
                .FindAll(m_ItemCollections, x => x.AllowItem(itemData))
                .OrderBy(x => x.priority)
                .FirstOrDefault();
        }
    }
}