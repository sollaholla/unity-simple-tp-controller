using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class ShooterWeaponItemInstance : WeaponItemInstanceBase<ShooterWeaponInventoryItem>
    {
        [SerializeField] private Transform m_ProjectileSpawnPoint = null;

        /// <summary>
        /// The current ammunition that is loaded in this weapon.
        /// </summary>
        public uint currentAmmo { get; private set; }

        protected override bool OnPrimaryUse(Vector3 target)
        {
            if (currentAmmo == 0)
            {
                return false;
            }

            var targetRotation = Quaternion.LookRotation((target - m_ProjectileSpawnPoint.position).normalized);
            var projectile = Instantiate(item.ammoItemType.projectilePrefab, m_ProjectileSpawnPoint.position, targetRotation);
            
            currentAmmo--;
            return true;
        }

        /// <summary>
        /// Sets the current ammunition to the given value.
        /// </summary>
        /// <param name="ammo">The target amount of ammunition.</param>
        public virtual void SetCurrentAmmo(uint ammo)
        {
            currentAmmo = (uint)Mathf.Min(ammo, item.maxAmmo);
        }
    }
}