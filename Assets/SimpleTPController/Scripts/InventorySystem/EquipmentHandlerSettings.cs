using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class EquipmentHandlerSettings
    {
        [SerializeField] private uint m_EquipmentCollectionIndex = 1;
        public uint equipmentCollectionIndex
        {
            get { return m_EquipmentCollectionIndex; }
        }

        [SerializeField] private EquipmentSlot[] m_EquipmentSlots = null;
        public EquipmentSlot[] equipmentSlots
        {
            get { return m_EquipmentSlots; }
        }
        
    }
}