using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// A setup class for visual inventory item equipment.
    /// </summary>
    [System.Serializable]
    public class VisualEquipment
    {
        public VisualEquipment() { }

        /// <summary>
        /// Creates a new instance of this <see cref="VisualEquipment" /> with the given
        /// parameters.
        /// </summary>
        /// <param name="itemType">The item type used for this visual equipment.</param>
        /// <param name="visualTransform">The transform/gameObject that represents this visual item.</param>
        public VisualEquipment(InventoryItem itemType, Transform visualTransform) 
        { 
            m_ItemType = itemType;
            m_VisualTransform = visualTransform;
        }

        [SerializeField] private InventoryItem m_ItemType = null;

        /// <summary>
        /// The item type used for this visual equipment.
        /// </summary>
        public InventoryItem itemType
        {
            get { return m_ItemType; }
        }

        [SerializeField] private Transform m_VisualTransform = null;

        /// <summary>
        /// The transform/gameObject that represents this visual item.
        /// </summary>
        public Transform visualTransform
        {
            get { return m_VisualTransform; }
        }
    }
}