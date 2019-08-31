using UnityEngine;

namespace ThirdPersonController
{
    [System.Serializable]
    public class CharacterMotorPhysicsSettings
    {
        [Range(0, 1000)]
        [SerializeField] private float m_JumpForce = 250.0f;
        public float jumpForce
        {
            get { return m_JumpForce; }
        }

        [Range(0.1f, 4)]
        [SerializeField] private float m_StandingHeight = 2.0f;
        public float standingHeight
        {
            get { return m_StandingHeight; }
        }
        
        [Range(0.1f, 4)]
        [SerializeField] private float m_CrouchHeight = 1.5f;
        public float crouchHeight
        {
            get { return m_CrouchHeight; }
        }
        
        [Range(1, 50)]
        [SerializeField] private float m_CrouchHeightLerpSpeed = 10.0f;
        public float crouchHeightLerpSpeed
        {
            get { return m_CrouchHeightLerpSpeed; }
        }

        [Range(0, 10)]
        [SerializeField] private float m_GroundStickForceMultiplier = 1f;

        public float groundStickForceMultiplier
        {
            get { return m_GroundStickForceMultiplier; }
        }

        [SerializeField] private LayerMask m_HeadCheckLayers = Physics.DefaultRaycastLayers;
        public LayerMask headCheckLayers
        {
            get { return m_HeadCheckLayers; }
        }

        [Range(0.1f, 5)]
        [SerializeField] private float m_HeadCheckDistance = 2.1f;
        public float headCheckDistance
        {
            get { return m_HeadCheckDistance; }
        }
    }
}