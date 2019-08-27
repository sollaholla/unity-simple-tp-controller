using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public abstract class WeaponItemInstanceBase<TItemType> : EquippableItemInstanceBase<TItemType>, IWeaponItemInstance where TItemType : WeaponInventoryItem
    {
        [SerializeField] private Transform m_IKTransform = null;
        public Transform ikTransform => m_IKTransform;

        /// <summary>
        /// The weapon item metadata.
        /// </summary>
        public WeaponInventoryItem weaponData => item;

        protected float m_CurrentCooldown;

        /// <summary>
        /// Called when this weapon is used in a primary context (e.g. shooting, swinging sword).
        /// </summary>
        /// <param name="useRay">The directional information of the use action.</param>
        public bool PrimaryUse(Ray useRay)
        {
            if (Time.time < m_CurrentCooldown)
            {
                return false;
            }

            m_CurrentCooldown = Time.time + item.primaryCooldown;
            return OnPrimaryUse(useRay);
        }

        protected virtual bool OnPrimaryUse(Ray useRay)
        {
            return true;
        }

        /// <summary>
        /// Called when this weapon is used in a secondary context (e.g. aiming, blocking)
        /// </summary>
        public void SecondaryUse(Vector3 usePoint, bool use)
        {
            OnSecondaryUse(usePoint, use);
        }

        protected virtual void OnSecondaryUse(Vector3 usePoint, bool use)
        {
        }
    }
}