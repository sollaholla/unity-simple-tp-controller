using UnityEngine;

namespace ThirdPersonController
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CameraControllerInput : MonoBehaviour, ICameraStateController, ICameraControllerInput
    {
        [SerializeField] private GameObject m_CameraControllerPrefab = null;
        [SerializeField] private CameraControllerInputSettings m_DefaultSettings = null;

        private CharacterMotor m_CharacterMotor;

        /// <summary>
        /// The current camera controller.
        /// </summary>
        public ICameraController cameraController { get; private set; }

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
            cameraController = Instantiate(m_CameraControllerPrefab).GetComponent<ICameraController>();
            cameraController.SetTarget(transform);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            cameraController.Rotate(
                InputManager.GetAxis(m_DefaultSettings.mouseXInput),
                InputManager.GetAxis(m_DefaultSettings.mouseYInput));
            transform.eulerAngles = new Vector3(0, cameraController.yRotation, 0);
        }

        public virtual string GetCurrentState()
        {
            return
                m_CharacterMotor.isCrouching ? m_DefaultSettings.crouchingStateName :
                m_CharacterMotor.isSprinting ? m_DefaultSettings.sprintingStateName :
                m_DefaultSettings.defaultStateName;
        }
    }
}