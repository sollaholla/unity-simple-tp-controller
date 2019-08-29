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
        private bool m_AimInput;
        private float m_WeaponZoom;

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
                var ray = new Ray(cam.transform.position, cam.transform.forward);
                var point = ray.GetPoint(100f);
                if (Physics.Raycast(ray, out var hit, 100f, m_InputSettings.aimLayers))
                {
                    point = hit.point;
                }

                m_WeaponHandler.PrimaryUse(point);
            }

            var hipFire = m_WeaponHandler.isTwoHanded && m_PrimaryInput && !m_SecondaryInput;
            m_WeaponHandler.SecondaryUse(cam.transform.forward, m_SecondaryInput || hipFire || m_InputSettings.debugAim, !hipFire);
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            UpdateWeaponZoom();
        }

        private void UpdateWeaponZoom()
        {
            if (m_WeaponHandler.isTwoHanded && m_WeaponHandler.primaryWeapon is ShooterWeaponItemInstance shooterWeapon)
            {
                if (m_WeaponHandler.usingSecondary && m_SecondaryInput)
                {
                    SetWeaponZoom(shooterWeapon.item.zoomLevel, shooterWeapon.item.zoomSpeed);
                }
                else
                {
                    SetWeaponZoom(0f, shooterWeapon.item.zoomSpeed);
                }
            }
            else
            {
                m_WeaponZoom = 0f;
            }

            m_CameraControllerInput.cameraController.Zoom(m_WeaponZoom);
        }

        private void SetWeaponZoom(float target, float speed)
        {
            if (speed > 0)
            {
                m_WeaponZoom = Mathf.Lerp(m_WeaponZoom, target, Time.fixedDeltaTime * speed);
            }
            else
            {
                m_WeaponZoom = target;
            }
        }
    }
}