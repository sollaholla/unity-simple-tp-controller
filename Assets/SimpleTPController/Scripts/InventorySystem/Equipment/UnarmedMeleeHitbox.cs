using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class UnarmedMeleeHitbox
    {
        [SerializeField] private int m_MeleeHitboxID = 0;
        public int meleeHitboxID
        {
            get { return m_MeleeHitboxID; }
        }

        [SerializeField] private Collider m_Hitbox = null;
        public Collider hitbox
        {
            get { return m_Hitbox; }
        }

        [SerializeField] private float m_MovementCooldown = 1f;
        public float movementCooldown
        {
            get { return m_MovementCooldown; }
        }

        [SerializeField] private float m_InitialDelay = 0.5f;
        public float initialDelay
        {
            get { return m_InitialDelay; }
        }

        [SerializeField] private float m_Duration = 1.25f;
        public float duration
        {
            get { return m_Duration; }
        }
    }
}