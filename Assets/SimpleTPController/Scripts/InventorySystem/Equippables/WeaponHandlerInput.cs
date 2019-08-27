using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Passes input to the <see cref="WeaponHandler" />.
    /// </summary>
    [RequireComponent(typeof(WeaponHandler))]
    [RequireComponent(typeof(ICameraControllerInput))]
    public class WeaponHandlerInput : MonoBehaviour
    {
        [SerializeField] private WeaponHandlerInputSettings m_InputSettings = null;

        private WeaponHandler m_WeaponHandler;
        private ICameraControllerInput m_CameraControllerInput;

        private bool m_PrimaryInput;
        private bool m_SecondaryInput;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_WeaponHandler = GetComponent<WeaponHandler>();
            m_CameraControllerInput = GetComponent<ICameraControllerInput>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            m_PrimaryInput = InputManager.GetButton(m_InputSettings.primaryUseButton);
            m_SecondaryInput = InputManager.GetButton(m_InputSettings.secondaryUseButton);

            var cam = m_CameraControllerInput.cameraController.cam;            
            if (m_PrimaryInput)
            {
                m_WeaponHandler.PrimaryUse(new Ray(cam.transform.position, cam.transform.forward));
            }

            m_WeaponHandler.SecondaryUse(cam.transform.forward, m_SecondaryInput || m_InputSettings.debugAim);
        }
    }
}