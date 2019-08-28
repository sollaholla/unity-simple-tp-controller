using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(ShooterAmmoHandler))]
    public class ShooterAmmoHandlerInput : MonoBehaviour
    {
        [SerializeField] private ShooterAmmoHandlerInputSettings m_InputSettings = null;

        private ShooterAmmoHandler m_ShooterAmmoHandler;
        private bool m_ReloadInput;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_ShooterAmmoHandler = GetComponent<ShooterAmmoHandler>();
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
                m_ShooterAmmoHandler.Reload();
            }
            m_ReloadInput = false;
        }
    }
}