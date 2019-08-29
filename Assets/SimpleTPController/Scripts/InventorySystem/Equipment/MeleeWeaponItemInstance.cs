using System;
using System.Collections;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Describes a melee weapon item instance.
    /// </summary>
    public class MeleeWeaponItemInstance : WeaponItemInstanceBase<MeleeWeaponInventoryItem>
    {
        [Header("Melee Settings")]
        [SerializeField] private Collider m_BoundingBox = null;
        
        private IEnumerator m_DamageWindow;

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
            CancelDamage();
        }

        protected override bool OnPrimaryUse(Vector3 point)
        {
            m_DamageWindow = RunDamageWindow();
            StartCoroutine(RunDamageWindow());
            return true;
        }

        public void CancelDamage()
        {
            if (m_DamageWindow != null)
            {
                StopCoroutine(m_DamageWindow);
            }
        }

        protected virtual IEnumerator RunDamageWindow()
        {
            yield return new WaitForSeconds(item.damageDelay);
            m_BoundingBox.enabled = true;
            yield return new WaitForSeconds(item.damageCooldown);
            m_BoundingBox.enabled = false;
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
            var hitbox = other.GetComponent<HitBox>();
            if (hitbox == null)
            {
                return;
            }
            
            var hitPoint = other.ClosestPoint(transform.position);
            var damageInfo = new DamageInfo(handler.gameObject, hitPoint, item.damage);
            hitbox.OnDamage(damageInfo);
            CancelDamage();
        }
    }
}