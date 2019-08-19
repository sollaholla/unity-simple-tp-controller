using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class ItemCollection
    {
        [SerializeField] private int m_Priority = 0;
        public int priority
        {
            get { return m_Priority; }
        }

        [Tooltip("If no categories are defined, we will allow any item into this collection.")]
        [SerializeField] private ItemCategory[] m_Categories = null;
        public ItemCategory[] categories
        {
            get { return m_Categories; }
        }

        [SerializeField] private string m_Name = "New_Collection";
        public string name
        {
            get { return m_Name; }
        }

        [SerializeField] private int m_MaxItems = 10;
        public int MaxItems
        {
            get { return m_MaxItems; }
        }

        public ItemData[] items { get; private set; }
        public Inventory inventory { get; private set; }

        public virtual void Initialize(Inventory inventory)
        {
            this.items = new ItemData[MaxItems];
            this.inventory = inventory;
        }

        public virtual bool AllowItem(ItemData data)
        {
            if (IsFull())
            {
                return false;
            }
            
            if (!AllowsCategory(data.item.category))
            {
                return false;
            }

            return true;
        }

        private bool AllowsCategory(ItemCategory category)
        {
            return m_Categories.Length <= 0 || m_Categories.Any(x => x == category);
        }

        public virtual bool IsFull()
        {
            return items.All(x => x != null && x.stack == x.item.maxStack);
        }
    }
}