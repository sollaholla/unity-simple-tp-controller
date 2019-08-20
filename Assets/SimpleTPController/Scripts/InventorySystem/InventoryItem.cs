using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Describes metadata for an inventory item.
    /// </summary>
    [CreateAssetMenu(menuName = "Inventory/InventoryItem")]
    public class InventoryItem : InventoryIdentity
    {
        [SerializeField] private Sprite m_Icon = null;

        /// <summary>
        /// The icon sprite for this inventory item.
        /// </summary>
        public Sprite icon
        {
            get { return m_Icon; }
        }

        [SerializeField] private uint m_MaxStack = 999;

        /// <summary>
        /// The maximum stack that this item can have in the inventory.
        /// </summary>
        public uint maxStack
        {
            get { return m_MaxStack; }
        }

        [SerializeField] private GameObject m_DropObject = null;

        /// <summary>
        /// The object that gets instantiated when this item is dropped from the inventory.
        /// </summary>
        public GameObject dropObject
        {
            get { return m_DropObject; }
        }

        [SerializeField] private ItemCategory m_Category = null;

        /// <summary>
        /// This inventory items category.
        /// </summary>
        public ItemCategory category
        {
            get { return m_Category; }
        }
    }
}