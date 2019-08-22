using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class EquipmentSlot
    {
        [SerializeField] private uint m_SlotIndex = 0;
        public uint slot
        {
            get { return m_SlotIndex; }
        }

        [SerializeField] private EquipmentRenderer m_EquipmentTransform = null;
        public EquipmentRenderer equipmentRenderer
        {
            get { return m_EquipmentTransform; }
        }
    }
}