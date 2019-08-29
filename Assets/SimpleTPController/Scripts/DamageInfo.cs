using UnityEngine;

namespace ThirdPersonController
{
    /// <summary>
    /// Describes damage information.
    /// </summary>
    public class DamageInfo
    {
        /// <summary>
        /// Creates a new instance of <see cref="DamageInfo" />.
        /// </summary>
        /// <param name="sender">The sending GameObject of the damage.</param>
        /// <param name="hitPoint">The point at which the damage was dealt.</param>
        /// <param name="damage">The amount of damage to deal.</param>
        public DamageInfo(GameObject sender, Vector3 hitPoint, float damage)
        {
            this.sender = sender;
            this.hitPoint = hitPoint;
            this.damage = damage;
        }

        /// <summary>
        /// The sender of the damage.
        /// </summary>
        public GameObject sender { get; }

        /// <summary>
        /// The hit point of the damage.
        /// </summary>
        public Vector3 hitPoint { get; }

        /// <summary>
        /// The amount of damage.
        /// </summary>
        public float damage { get; private set; }

        /// <summary>
        /// Manipulates the damage by the given multiplier value.
        /// </summary>
        /// <param name="multiplier">The damage multiplier.</param>
        public void ManipulateDamage(float multiplier)
        {
            damage = Mathf.Max(0, damage * multiplier);
        }
    }
}