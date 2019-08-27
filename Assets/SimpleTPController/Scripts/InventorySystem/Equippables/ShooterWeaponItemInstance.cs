using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class ShooterWeaponItemInstance : WeaponItemInstanceBase<ShooterWeaponInventoryItem>
    {
        [SerializeField] private Transform m_ProjectileSpawnPoint = null;

        protected override bool OnPrimaryUse(Ray useRay)
        {
            

            return true;
        }
    }
}