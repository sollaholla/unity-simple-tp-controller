using UnityEngine;

namespace ThirdPersonController
{
    /// <summary>
    /// Describes a hitbox that can receive and manipulate damage.
    /// </summary>
    public class HitBox : MonoBehaviour
    {
        [SerializeField] private float m_DamageMultiplier = 1f;
        
        /// <summary>
        /// The damage multiplier for this hitbox.
        /// </summary>
        public float damageMultiplier => m_DamageMultiplier;

        private IDamageReceiver m_DamageReceiver;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_DamageReceiver = GetComponentInParent<IDamageReceiver>();
        }

        /// <summary>
        /// Notify this hitbox that it has received damage.
        /// </summary>
        /// <param name="damageInfo">The damage information.</param>
        public virtual void OnDamage(DamageInfo damageInfo)
        {
            damageInfo.ManipulateDamage(m_DamageMultiplier);
            m_DamageReceiver.Damage(damageInfo);
        }
    }
}