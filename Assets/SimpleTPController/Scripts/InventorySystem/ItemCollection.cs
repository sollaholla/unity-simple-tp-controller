using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Describes a collection of inventory items.
    /// </summary>
    [System.Serializable]
    public class ItemCollection
    {
        [SerializeField] private string m_Name = "New_Collection";
        [SerializeField] private int m_Priority = 0;
        [SerializeField] private int m_MaxItems = 10;
        [Tooltip("If no categories are defined, we will allow any item into this collection.")]
        [SerializeField] private ItemCategory[] m_Categories = null;
        [SerializeField] private ItemSlotMask[] m_SlotMasks = null;

        private ItemDataInstance[] m_Items;

        /// <summary>
        /// The name of this collection.
        /// </summary>
        public string name
        {
            get { return m_Name; }
        }

        /// <summary>
        /// This collections priority.
        /// </summary>
        public int priority
        {
            get { return m_Priority; }
        }

        /// <summary>
        /// The maximum number of items in this collection.
        /// </summary>
        public int maxItems
        {
            get { return m_MaxItems; }
        }

        /// <summary>
        /// The item categories allowed in this item collection.
        /// </summary>
        public ItemCategory[] categories
        {
            get { return m_Categories; }
        }

        /// <summary>
        /// The items contained in this item collection.
        /// </summary>
        public IReadOnlyCollection<ItemDataInstance> items => m_Items;

        /// <summary>
        /// The inventory that owns this collection.
        /// </summary>
        public Inventory inventory { get; private set; }

        /// <summary>
        /// Called when an item slot has changed.
        /// </summary>
        public event Action<uint, ItemDataInstance> slotChanged;

        /// <summary>
        /// Initialize this item collection with the given inventory.
        /// </summary>
        /// <param name="inventory">The inventory that owns this collection.</param>
        public virtual void Initialize(Inventory inventory)
        {
            this.m_Items = new ItemDataInstance[maxItems];
            this.inventory = inventory;
        }

        /// <summary>
        /// True if this item collection allows the given item.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        public virtual bool AllowItem(ItemDataInstance itemData)
        {
            if (!AllowsCategory(itemData.item.category))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// True if this item collection allows the given category.
        /// </summary>
        /// <param name="category">The item category.</param>
        public bool AllowsCategory(ItemCategory category)
        {
            return m_Categories.Length <= 0 || m_Categories.Any(x => x == category);
        }

        /// <summary>
        /// True if the given item is allowed in the given slot.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        /// <param name="slot">The item slot.</param>
        /// <returns></returns>
        public bool SlotAllows(ItemDataInstance itemData, uint slot)
        {
            var slotMask = m_SlotMasks.FirstOrDefault(x => x.slot == slot);
            if (slotMask != null)
            {
                if (!slotMask.itemCategories.Contains(itemData.item.category))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// True if this item collection is full.
        /// </summary>
        public virtual bool IsFull()
        {
            return items.All(x => x != null && x.stack == x.item.maxStack);
        }

        public virtual bool IsFullyOccupied()
        {
            return items.All(x => x != null);
        }

        /// <summary>
        /// Sets the item in the given slot.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        /// <param name="slot">The slot to place the item in.</param>
        public virtual void SetSlot(ItemDataInstance itemData, uint slot)
        {
            m_Items[slot] = itemData;
            slotChanged?.Invoke(slot, itemData);
        }

        /// <summary>
        /// Insert the given item data into the inventory items in the first empty slot.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        public virtual bool Insert(ItemDataInstance itemData)
        {
            if (itemData.stack <= 0)
            {
                return false;
            }

            if (IsFullyOccupied())
            {
                return false;
            }

            int index = GetFirstEmptySlot();
            SetSlot(itemData, (uint)index);

            return true;
        }

        /// <summary>
        /// Gets the first empty slot in this collection.
        /// </summary>
        public virtual int GetFirstEmptySlot()
        {
            return Array.FindIndex(m_Items, x => x == null);
        }

        /// <summary>
        /// True if the item data is contained within the inventory.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        public virtual bool Contains(ItemDataInstance itemData)
        {
            return m_Items.Contains(itemData);
        }

        /// <summary>
        /// Gets the slot containing the given item data.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        public virtual int GetSlot(ItemDataInstance itemData)
        {
            return Array.FindIndex(m_Items, x => x == itemData);
        }
    }
}