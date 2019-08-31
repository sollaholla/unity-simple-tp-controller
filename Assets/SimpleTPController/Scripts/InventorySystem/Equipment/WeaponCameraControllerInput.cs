using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(WeaponHandler))]
    public class WeaponCameraControllerInput : CameraControllerInput
    {
        [SerializeField] private WeaponCameraControllerInputSettings m_WeaponCameraSettings = null;

        private WeaponHandler m_WeaponHandler;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            m_WeaponHandler = GetComponent<WeaponHandler>();
        }

        public override string GetCurrentState()
        {
            return m_WeaponHandler.aiming && m_WeaponHandler.aimingWalkMode ? m_WeaponCameraSettings.aimingState : base.GetCurrentState();
        }
    }
}