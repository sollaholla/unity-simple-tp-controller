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
            if (InputManager.GetButton(m_InputSettings.primaryUseButton))
            {
                var useRay = m_CameraControllerInput.cameraController.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                m_WeaponHandler.PrimaryUse(useRay);
            }
        }
    }
}