using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/InventoryItem")]
    public class InventoryItem : InventoryIdentity
    {
        [SerializeField] private Sprite m_Icon = null;
        public Sprite icon
        {
            get { return m_Icon; }
        }

        [SerializeField] private uint m_MaxStack = 999;
        public uint maxStack
        {
            get { return m_MaxStack; }
        }

        [SerializeField] private GameObject m_DropObject = null;
        public GameObject dropObject
        {
            get { return m_DropObject; }
        }

        [SerializeField] private ItemCategory m_Category = null;
        public ItemCategory category
        {
            get { return m_Category; }
        }
    }
}