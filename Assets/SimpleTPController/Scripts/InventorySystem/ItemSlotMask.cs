using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class ItemSlotMask
    {
        [SerializeField] private int m_SlotIndex = -1;
        public int slot
        {
            get { return m_SlotIndex; }
        }

        [SerializeField] private ItemCategory[] m_ItemCategory = null;
        public ItemCategory[] itemCategories
        {
            get { return m_ItemCategory; }
        }
    }
}