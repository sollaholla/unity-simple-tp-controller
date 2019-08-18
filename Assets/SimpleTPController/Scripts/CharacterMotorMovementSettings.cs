using UnityEngine;

namespace ThirdPersonController
{
    [System.Serializable]
    public class CharacterMotorMovementSettings
    {
        [Range(0, 10)]
        [SerializeField] private float m_CrouchSpeed = 0.75f;
        public float crouchSpeed
        {
            get { return m_CrouchSpeed; }
        }
        
        [Range(0, 10)]
        [SerializeField] private float m_SprintSpeed = 5.0f;
        public float sprintSpeed
        {
            get { return m_SprintSpeed; }
        }

        [Range(0, 10)]
        [SerializeField] private float m_WalkSpeed = 1.5f;
        public float walkSpeed
        {
            get { return m_WalkSpeed; }
        }
    }
}