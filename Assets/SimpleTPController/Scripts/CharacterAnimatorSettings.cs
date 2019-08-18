using UnityEngine;

namespace ThirdPersonController
{
    [System.Serializable]
    public class CharacterAnimatorSettings
    {
        [Range(0, 1)]
        [SerializeField] private float m_MovementDampTime = 0.1f;
        public float movementDampTime
        {
            get { return m_MovementDampTime; }
        }

        [SerializeField] private string m_VelocityXParameter = "VelocityX";
        public string velocityXParameter
        {
            get { return m_VelocityXParameter; }
        }
        
        [SerializeField] private string m_VelocityZParameter = "VelocityZ";
        public string velocityZParameter
        {
            get { return m_VelocityZParameter; }
        }

        [SerializeField] private string m_GroundedParameter = "Grounded";
        public string groundedParameter
        {
            get { return m_GroundedParameter; }
        }

        [SerializeField] private string m_JumpingParameter = "Jumping";
        public string jumpingParameter
        {
            get { return m_JumpingParameter; }
        }

        [SerializeField] private string m_CrouchingParameter = "Crouching";
        public string crouchingParameter
        {
            get { return m_CrouchingParameter; }
        }
    }
}