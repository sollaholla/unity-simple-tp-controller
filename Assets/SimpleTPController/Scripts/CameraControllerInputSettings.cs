using UnityEngine;

namespace ThirdPersonController
{
    [System.Serializable]
    public class CameraControllerInputSettings
    {
        [SerializeField] private string m_MouseXInput = "Mouse X";
        public string mouseXInput
        {
            get { return m_MouseXInput; }
        }

        [SerializeField] private string m_MouseYInput = "Mouse Y";
        public string mouseYInput
        {
            get { return m_MouseYInput; }
        }

        [SerializeField] private string m_CrouchingStateName = "Crouching";
        public string crouchingStateName
        {
            get { return m_CrouchingStateName; }
        }
        
        [SerializeField] private string m_SprintingStateName = "Sprinting";
        public string sprintingStateName
        {
            get { return m_SprintingStateName; }
        }
        
        [SerializeField] private string m_DefaultStateName = "Default";
        public string defaultStateName
        {
            get { return m_DefaultStateName; }
        }
    }
}