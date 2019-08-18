using UnityEngine;

namespace ThirdPersonController
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CameraControllerInput : MonoBehaviour
    {
        [SerializeField] private GameObject m_CameraControllerPrefab = null;
        [SerializeField] private CameraControllerInputSettings m_DefaultSettings = null;

        private ThirdPersonCameraController m_CameraController;
        private CharacterMotor m_CharacterMotor;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_CharacterMotor = GetComponent<CharacterMotor>();
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            m_CameraController = Instantiate(m_CameraControllerPrefab).GetComponent<ThirdPersonCameraController>();
            m_CameraController.SetTarget(transform);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            m_CameraController.Rotate(
                Input.GetAxis(m_DefaultSettings.mouseXInput), 
                Input.GetAxis(m_DefaultSettings.mouseYInput));

            transform.eulerAngles = new Vector3(0, m_CameraController.yRotation, 0);

            m_CameraController.SetState(GetCurrentState());
        }

        protected virtual string GetCurrentState()
        {
            return
                m_CharacterMotor.isCrouching ? m_DefaultSettings.crouchingStateName : 
                m_CharacterMotor.isSprinting ? m_DefaultSettings.sprintingStateName :
                m_DefaultSettings.defaultStateName;
        }
    }
}