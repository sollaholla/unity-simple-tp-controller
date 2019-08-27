using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/ShooterWeaponInventoryItem")]
    public class ShooterWeaponInventoryItem : WeaponInventoryItem
    {
        [SerializeField] private GameObject m_ProjectilePrefab = null;
        public GameObject projectilePrefab
        {
            get { return m_ProjectilePrefab; }
        }
    }
}