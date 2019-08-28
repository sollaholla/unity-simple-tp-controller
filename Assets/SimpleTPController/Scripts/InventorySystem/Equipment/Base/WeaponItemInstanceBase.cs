using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// A base weapon item instance type.
    /// </summary>
    /// <typeparam name="TItemType">The type of weapon item.</typeparam>
    public abstract class WeaponItemInstanceBase<TItemType> : EquippableItemInstanceBase<TItemType>, IWeaponItemInstance where TItemType : WeaponInventoryItem
    {
        [SerializeField] private Transform m_IKTransform = null;

        /// <summary>
        /// The hand IK transform.
        /// </summary>
        public Transform ikTransform => m_IKTransform;

        /// <summary>
        /// The weapon item metadata.
        /// </summary>
        public WeaponInventoryItem weaponData => item;

        protected float m_CurrentCooldown;

        /// <summary>
        /// Called when this weapon is used in a primary context (e.g. shooting, swinging sword).
        /// </summary>
        public bool PrimaryUse(Vector3 point)
        {
            if (Time.time < m_CurrentCooldown)
            {
                return false;
            }

            m_CurrentCooldown = Time.time + item.primaryCooldown;
            return OnPrimaryUse(point);
        }

        protected virtual bool OnPrimaryUse(Vector3 point)
        {
            return true;
        }

        /// <summary>
        /// Called when this weapon is used in a secondary context (e.g. aiming, blocking)
        /// </summary>
        public void SecondaryUse(Vector3 direction, bool use)
        {
            OnSecondaryUse(direction, use);
        }

        protected virtual void OnSecondaryUse(Vector3 direction, bool use)
        {
        }
    }
}