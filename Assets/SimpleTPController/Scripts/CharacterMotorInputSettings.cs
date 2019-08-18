
using UnityEngine;

namespace ThirdPersonController
{
    [System.Serializable]
    public class CharacterMotorInputSettings
    {
        [SerializeField] private string m_HorizontalInput = "Horizontal";
        public string horizontalInput
        {
            get { return m_HorizontalInput; }
        }
        
        [SerializeField] private string m_VerticalInput = "Vertical";
        public string verticalInput
        {
            get { return m_VerticalInput; }
        }
        
        [SerializeField] private string m_JumpInput = "Jump";
        public string jumpInput
        {
            get { return m_JumpInput; }
        }

        [SerializeField] private string m_SprintInput = "Sprint";
        public string sprintInput
        {
            get { return m_SprintInput; }
        }

        [SerializeField] private string m_CrouchInput = "Crouch";
        public string crouchInput
        {
            get { return m_CrouchInput; }
        }
    }
}