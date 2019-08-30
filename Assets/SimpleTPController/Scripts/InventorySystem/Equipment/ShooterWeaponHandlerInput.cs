using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(ShooterWeaponHandler))]
    [RequireComponent(typeof(WeaponHandler))]
    [RequireComponent(typeof(ICameraControllerInput))]
    public class ShooterWeaponHandlerInput : MonoBehaviour
    {
        [SerializeField] private ShooterWeaponHandlerInputSettings m_InputSettings = null;

        private WeaponHandler m_WeaponHandler;
        private ShooterWeaponHandler m_ShooterWeaponHandler;
        private ICameraControllerInput m_CameraInput;
        private bool m_ReloadInput;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_CameraInput = GetComponent<ICameraControllerInput>();
            m_ShooterWeaponHandler = GetComponent<ShooterWeaponHandler>();
            m_WeaponHandler = GetComponent<WeaponHandler>();
            m_WeaponHandler.primaryUsed += OnPrimaryUsed;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            if (InputManager.GetButtonDown(m_InputSettings.reloadButton))
            {
                m_ReloadInput = true;
            }
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            if (m_ReloadInput)
            {
                m_ShooterWeaponHandler.Reload();
            }
            m_ReloadInput = false;
        }
        
        protected virtual void OnPrimaryUsed(IWeaponItemInstance weapon)
        {
            if (weapon is ShooterWeaponItemInstance shooterWeapon)
            {
                ApplyCameraRecoil(shooterWeapon);
            }
        }

        protected virtual void ApplyCameraRecoil(ShooterWeaponItemInstance shooterWeapon)
        {
            var strengthMultiplier = 1f;
            if (m_WeaponHandler.isTwoHanded && m_WeaponHandler.aiming && m_WeaponHandler.aimingWalkMode)
            {
                strengthMultiplier *= m_InputSettings.aimRecoilMultiplier;
            }
            
            // TODO: Check if crouching.
            m_CameraInput.cameraController.Recoil(
                shooterWeapon.item.recoilStrengthX * strengthMultiplier, 
                shooterWeapon.item.recoilStrengthY * strengthMultiplier);
        }
    }
}