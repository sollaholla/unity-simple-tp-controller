using System;
using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemCollection[] m_ItemCollections = null;
        public ItemCollection[] itemCollections
        {
            get { return m_ItemCollections; }
        }

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

        public virtual void Add(InventoryItemInstance itemInstance)
        {
            var data = new ItemData(itemInstance);
            if (data.stack <= 0)
            {
                return;
            }

            var collection = GetBestCollectionForItem(data);
            var existingItems = collection.items.Where(x => x.stack < x.item.maxStack).ToArray();
            if (existingItems.Length > 0)
            {
                
            }
        }

        public virtual InventoryItemInstance[] Drop(string itemID, int count)
        {
            throw new System.NotImplementedException();
        }

        public virtual ItemCollection GetBestCollectionForItem(ItemData itemData)
        {
            return Array
                .FindAll(m_ItemCollections, x => x.AllowItem(itemData))
                .OrderBy(x => x.priority)
                .FirstOrDefault();
        }
    }

    public class ItemData
    {
        public ItemData(InventoryItemInstance instance)
        {
            item = instance.item;
            stack = instance.stack;
        }

        public InventoryItem item { get; private set; }
        public uint stack { get; set; }
    }
}