using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class MeleeWeaponItemInstance : WeaponItemInstanceBase<MeleeWeaponInventoryItem>
    {
        [Header("Melee Settings")]
        [SerializeField] private Collider m_BoundingBox = null;
        
        private float m_DamageTimer;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_BoundingBox.isTrigger = true;
            m_BoundingBox.enabled = false;
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected virtual void OnDisable()
        {
            m_DamageTimer = 0f;
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            if (item.kind == WeaponKind.Primary)
            {
                if (Time.fixedTime > m_DamageTimer && m_BoundingBox.enabled)
                {
                    m_BoundingBox.enabled = false;
                }
            }
        }

        protected override bool OnPrimaryUse(Ray useRay)
        {
            m_BoundingBox.enabled = true;
            m_DamageTimer = Time.fixedTime + item.damageCooldown;
            return true;
        }

        protected override void OnSecondaryUse(Vector3 usePoint, bool use)
        {
            m_BoundingBox.enabled = use;
        }
        
        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        protected virtual void OnTriggerEnter(Collider other)
        {
            // TODO: Deal damage!
        }
    }
}