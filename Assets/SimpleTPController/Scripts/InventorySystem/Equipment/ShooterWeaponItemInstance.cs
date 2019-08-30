using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class ShooterWeaponItemInstance : WeaponItemInstanceBase<ShooterWeaponInventoryItem>
    {
        [SerializeField] private Transform m_ProjectileSpawnPoint = null;
        [SerializeField] private ParticleSystem m_MuzzleFlashParticles = null;
        [SerializeField] private float m_ParticlesDuration = 0.1f;

        /// <summary>
        /// The current ammunition that is loaded in this weapon.
        /// </summary>
        public uint currentAmmo { get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_MuzzleFlashParticles?.Stop();
            m_MuzzleFlashParticles?.gameObject.SetActive(false);
        }

        protected override bool OnPrimaryUse(Vector3 target)
        {
            if (currentAmmo == 0)
            {
                return false;
            }

            var targetRotation = Quaternion.LookRotation((target - m_ProjectileSpawnPoint.position).normalized);
            var projectile = Instantiate(item.ammoItemType.projectilePrefab, m_ProjectileSpawnPoint.position, targetRotation);
            var projectileData = projectile.GetComponent<IProjectile>();
            if (projectileData != null)
            {
                projectileData.OnSpawn(handler.gameObject, item.damage);
            }

            if (m_MuzzleFlashParticles != null)
            {
                m_MuzzleFlashParticles.gameObject.SetActive(true);
                m_MuzzleFlashParticles.Play(true);
                Invoke(nameof(StopParticles), m_ParticlesDuration);
            }
            
            currentAmmo--;
            return true;
        }

        protected virtual void StopParticles()
        {
            m_MuzzleFlashParticles?.Stop(true);
            m_MuzzleFlashParticles?.gameObject.SetActive(false);
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