using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Describes an inventory item for ammunition.
    /// </summary>
    [CreateAssetMenu(menuName = "Inventory/AmmoInventoryItem")]
    public class AmmoInventoryItem : InventoryItem
    {
        [SerializeField] private GameObject m_ProjectilePrefab = null;

        /// <summary>
        /// The projectile prefab to spawn for this ammunition when used.
        /// </summary>
        /// <value></value>
        public GameObject projectilePrefab
        {
            get { return m_ProjectilePrefab; }
        }
    }
}