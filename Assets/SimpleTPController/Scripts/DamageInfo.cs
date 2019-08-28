using UnityEngine;

namespace ThirdPersonController
{
    public class DamageInfo
    {
        public DamageInfo(GameObject sender, Vector3 hitPoint, float damage)
        {
            this.sender = sender;
            this.hitPoint = hitPoint;
            this.damage = damage;
        }

        public GameObject sender { get; }
        public Vector3 hitPoint { get; }
        public float damage { get; }
    }
}