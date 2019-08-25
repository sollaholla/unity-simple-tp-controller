using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Defines a weapon inventory item.
    /// </summary>
    public class WeaponItemInstance : EquippableItemInstanceBase<WeaponInventoryItem>
    {
        [SerializeField] private Transform m_IKPoint = null;

        /// <summary>
        /// The hand IK point for this weapon.
        /// </summary>
        /// <value></value>
        public Transform ikPoint 
        {
            get { return m_IKPoint; }
        }

        /// <summary>
        /// The hand IK Goal of the weapon item.
        /// </summary>
        public AvatarIKGoal ikGoal => item.ikGoal;

        /// <summary>
        /// True if hand ik is possible for this weapon.
        /// </summary>
        public bool useIK
        {
            get { return m_IKPoint != null; }
        }

        protected float m_CurrentCooldown;

        /// <summary>
        /// Called when this weapon is used in a primary context (e.g. shooting, swinging sword).
        /// </summary>
        /// <param name="useRay">The directional information of the use action.</param>
        public void PrimaryUse(Ray useRay)
        {
            if (Time.time < m_CurrentCooldown)
            {
                return;
            }

            OnPrimaryUse(useRay);
            m_CurrentCooldown = Time.time + item.primaryCooldown;
        }

        protected virtual void OnPrimaryUse(Ray useRay)
        {
        }

        /// <summary>
        /// Called when this weapon is used in a secondary context (e.g. aiming, blocking)
        /// </summary>
        public void SecondaryUse()
        {
        }
    }
}