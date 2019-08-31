using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(CharacterMotor))]
    [RequireComponent(typeof(WeaponHandler))]
    public class UnarmedMeleeHandler : MonoBehaviour
    {
        [SerializeField] private UnarmedMeleeHitbox[] m_Hitboxes = null;

        public bool isMeleeAttacking { get; private set; }
        public event Action<int> meleeAttack;

        private CharacterMotor m_CharacterMotor;
        private WeaponHandler m_WeaponHandler;
        private int m_CurrentHitboxID;
        private IEnumerator m_HitboxWindowEnumerator;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_CharacterMotor = GetComponent<CharacterMotor>();
            m_WeaponHandler = GetComponent<WeaponHandler>();
            m_WeaponHandler.primaryWeaponChanged += OnPrimaryChanged;
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected virtual void OnDisable()
        {
            CancelAttack();
        }

        /// <summary>
        /// Perform an unarmed melee attack.
        /// </summary>
        public void Attack()
        {
            if (m_CharacterMotor.isMovementLocked)
            {
                return;
            }

            var hitbox = m_Hitboxes
                .FirstOrDefault(x => x.meleeHitboxID == (m_WeaponHandler.primaryWeapon?.weaponData.meleeHitboxID ?? 0));

            m_CurrentHitboxID = hitbox.meleeHitboxID;
            m_HitboxWindowEnumerator = HitboxWindow(hitbox.initialDelay, hitbox.duration);

            m_CharacterMotor.MoveLock(hitbox.movementCooldown);
            StartCoroutine(m_HitboxWindowEnumerator);
            
            meleeAttack?.Invoke(m_CurrentHitboxID);
        }
        
        protected virtual void OnPrimaryChanged(IWeaponItemInstance weapon)
        {
            CancelAttack();
        }

        protected virtual void CancelAttack(UnarmedMeleeHitbox meleeHitbox = null)
        {
            if (!isMeleeAttacking)
            {
                return;
            }

            var hitbox = meleeHitbox ?? m_Hitboxes.FirstOrDefault(x => x.meleeHitboxID == m_CurrentHitboxID);            
            hitbox.hitbox.enabled = false;
            isMeleeAttacking = false;

            if (m_HitboxWindowEnumerator != null)
            {
                StopCoroutine(m_HitboxWindowEnumerator);
            }
        }
        
        protected virtual IEnumerator HitboxWindow(float initDelay, float endDelay)
        {
            isMeleeAttacking = true;

            yield return new WaitForSeconds(initDelay);
            var hitbox = m_Hitboxes.FirstOrDefault(x => x.meleeHitboxID == m_CurrentHitboxID);
            hitbox.hitbox.enabled = true;

            yield return new WaitForSeconds(endDelay);
            m_HitboxWindowEnumerator = null;
            CancelAttack(hitbox);
        }
    }
}