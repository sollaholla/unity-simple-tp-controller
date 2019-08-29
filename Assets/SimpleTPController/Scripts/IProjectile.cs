using System;
using UnityEngine;

namespace ThirdPersonController
{
    /// <summary>
    /// Describes a projectile.
    /// </summary>
    public interface IProjectile
    {
        /// <summary>
        /// Invoked when this projectile hits something.
        /// </summary>
        event Action<Vector3> hit;

        /// <summary>
        /// Initializes this projectile.
        /// </summary>
        /// <param name="owner">The projectiles owner.</param>
        /// <param name="damage">The amount of damage this projectile does.</param>
        void OnSpawn(GameObject owner, float damage);
    }
}