using System;
using UnityEngine;

namespace ThirdPersonController
{

    /// <summary>
    /// Describes a hitscan projectile.
    /// </summary>
    public class HitscanProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private float m_Range = 100f;
        [SerializeField] private LayerMask m_HitLayers = Physics.DefaultRaycastLayers;

        /// <summary>
        /// Called when this hitscan projectile has hit something.
        /// </summary>
        public event Action<Vector3> hit;

        private GameObject m_Owner;
        private float m_Damage;
        private bool m_DidHit;

        /// <summary>
        /// Call this to initialize this projectile with projectile information.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="damage"></param>
        public void OnSpawn(GameObject owner, float damage)
        {
            m_Owner = owner;
            m_Damage = damage;
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            if (m_DidHit)
            {
                return;
            }

            if (Physics.Raycast(transform.position, transform.forward, out var hit, m_Range, m_HitLayers))
            {
                var hitBox = hit.collider.GetComponent<HitBox>();

                if (hitBox)
                {
                    var damageInfo = new DamageInfo(m_Owner, hit.point, m_Damage);
                    hitBox.OnDamage(damageInfo);
                    this.hit?.Invoke(hit.point);
                }
            }

            Destroy(gameObject);
            m_DidHit = true;
        }
    }
}