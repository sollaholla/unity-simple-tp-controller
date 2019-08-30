using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Handles shooter weapon ammunition.
    /// </summary>
    [RequireComponent(typeof(WeaponHandler))]
    [RequireComponent(typeof(Inventory))]
    public class ShooterWeaponHandler : MonoBehaviour, IWeaponHandlerBehaviourOverride
    {
        [SerializeField] private string m_AmmoCollectionName = "Items";

        /// <summary>
        /// True if we're reloading the current weapon.
        /// </summary>
        public bool isReloading { get; private set; }

        private WeaponHandler m_WeaponHandler;
        private Inventory m_Inventory;
        private ItemCollection m_AmmoCollection;
        private IEnumerator m_ReloadEnumerator;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_WeaponHandler = GetComponent<WeaponHandler>();
            m_Inventory = GetComponent<Inventory>();
            m_WeaponHandler.primaryWeaponChanged += OnPrimaryChanged;
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            m_AmmoCollection = m_Inventory.GetCollection(m_AmmoCollectionName);
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected virtual void OnDisable()
        {
            CancelReload();
        }

        private void OnPrimaryChanged(IWeaponItemInstance newWeapon)
        {
            CancelReload();
        }

        /// <summary>
        /// Reload the current primary weapon.
        /// </summary>
        public virtual void Reload()
        {
            if (isReloading)
            {
                return;
            }

            if (!(m_WeaponHandler.primaryWeapon is ShooterWeaponItemInstance shooterWeapon))
            {
                return;
            }

            if (shooterWeapon.currentAmmo == shooterWeapon.item.maxAmmo)
            {
                return;
            }

            var ammoCount = m_AmmoCollection.items
                .Where(x => x != null && x.item == shooterWeapon.item.ammoItemType)
                .Sum(x => x.stack);

            if (ammoCount == 0)
            {
                return;
            }

            var ammoNeeded = shooterWeapon.item.maxAmmo - shooterWeapon.currentAmmo;
            var ammoToUse = (uint)Mathf.Min(ammoNeeded, ammoCount);
            
            m_ReloadEnumerator = AwaitReload(shooterWeapon, shooterWeapon.item.reloadDuration, ammoToUse);
            StartCoroutine(m_ReloadEnumerator);
        }

        /// <summary>
        /// Cancels the current reload.
        /// </summary>
        public virtual void CancelReload()
        {
            if (m_ReloadEnumerator != null)
            {
                StopCoroutine(m_ReloadEnumerator);
            }

            isReloading = false;
            m_ReloadEnumerator = null;
        }

        protected virtual IEnumerator AwaitReload(ShooterWeaponItemInstance weapon, float reloadTime, uint ammoToUse)
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            isReloading = false;

            if (weapon != (ShooterWeaponItemInstance)m_WeaponHandler.primaryWeapon)
            {
                yield break;
            }

            var removed = m_Inventory.RemoveItems(weapon.item.ammoItemType, ammoToUse, m_AmmoCollection);
            weapon.SetCurrentAmmo(weapon.currentAmmo + (uint)removed);
        }

        public virtual bool CanUsePrimary(Vector3 point, IWeaponItemInstance weapon)
        {
            if (isReloading)
            {
                return false;
            }

            return true;
        }

        public virtual bool CanUseSecondary(Vector3 direction, bool use, IWeaponItemInstance weapon)
        {
            if (isReloading)
            {
                return false;
            }

            return true;
        }
    }
}