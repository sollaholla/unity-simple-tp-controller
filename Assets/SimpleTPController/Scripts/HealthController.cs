using System;
using UnityEngine;

namespace ThirdPersonController
{
    /// <summary>
    /// Controls health, damage, and death.
    /// </summary>
    public class HealthController : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private float m_MaxHealth = 100;

        /// <summary>
        /// The current health.
        /// </summary>
        public float health { get; private set; }

        /// <summary>
        /// True if we have died from low health.
        /// </summary>
        public bool isDead { get; private set; }

        /// <summary>
        /// Invoked when this health controller has died.
        /// </summary>
        public event Action<GameObject> died;

        /// <summary>
        /// Invoked when this health controller takes damage.
        /// </summary>
        public event Action<DamageInfo> damaged;

        /// <summary>
        /// Invoked when the health is regenerated.
        /// </summary>
        public event Action<float> regenHealth;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            health = m_MaxHealth;
        }

        /// <summary>
        /// Regenerate the given amount of health.
        /// </summary>
        /// <param name="amount">The amount of health to regenerate.</param>
        public virtual void RegenHealth(float amount)
        {
            health = Mathf.Min(m_MaxHealth, health + amount);
            regenHealth?.Invoke(amount);
        }

        /// <summary>
        /// Applies damage to this health controller.
        /// </summary>
        /// <param name="damageInfo">The damage information.</param>
        public virtual void Damage(DamageInfo damageInfo)
        {
            if (isDead)
            {
                return;
            }

            ReduceHealth(damageInfo.damage);
            damaged?.Invoke(damageInfo);

            if (health <= 0)
            {
                Kill(damageInfo.sender);
            }
        }

        /// <summary>
        /// Kills this health controller.
        /// </summary>
        /// <param name="killer">The gameObject that killed this health controller.</param>
        public virtual void Kill(GameObject killer)
        {
            if (isDead)
            {
                return;
            }

            died?.Invoke(killer);
            isDead = true;
            health = 0;
        }

        protected virtual void ReduceHealth(float damage)
        {
            health -= damage;
        }
    }
}