using UnityEngine;

namespace ThirdPersonController
{
    /// <summary>
    /// Drives the <see cref="CharacterMotor" /> using user input values.
    /// </summary>
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterMotorInput : MonoBehaviour
    {
        [SerializeField] private CharacterMotorInputSettings m_InputSettings = null;

        private CharacterMotor m_CharacterMotor;
        private Vector3 m_MovementInput;
        private bool m_CrouchInput;
        private bool m_SprintInput;
        private bool m_JumpInput;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_CharacterMotor = GetComponent<CharacterMotor>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            m_MovementInput = new Vector3(
                Input.GetAxis(m_InputSettings.horizontalInput), 0,
                Input.GetAxis(m_InputSettings.verticalInput));

            m_MovementInput = transform.TransformDirection(m_MovementInput);

            m_CrouchInput = Input.GetButton(m_InputSettings.crouchInput);
            m_SprintInput = Input.GetButton(m_InputSettings.sprintInput);
            
            if (Input.GetButtonDown(m_InputSettings.jumpInput))
            {
                m_JumpInput = true;
            }
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            m_CharacterMotor.Move(m_MovementInput.x, m_MovementInput.z, m_SprintInput, m_CrouchInput, m_JumpInput);
            m_JumpInput = false;
        }
    }
}