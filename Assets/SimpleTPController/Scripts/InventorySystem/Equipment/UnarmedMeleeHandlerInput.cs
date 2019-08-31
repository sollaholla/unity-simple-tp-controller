using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(UnarmedMeleeHandler))]
    public class UnarmedMeleeHandlerInput : MonoBehaviour
    {
        [SerializeField] private UnarmedMeleeHandlerInputSettings m_UnarmedSettings = null;

        private UnarmedMeleeHandler m_UnarmedMeleeHandler;
        private bool m_MeleeInput;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_UnarmedMeleeHandler = GetComponent<UnarmedMeleeHandler>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            if (InputManager.GetButtonDown(m_UnarmedSettings.meleeButton))
            {
                m_MeleeInput = true;
            }
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            if (m_MeleeInput)
            {
                m_UnarmedMeleeHandler.Attack();
            }
            m_MeleeInput = false;
        }
    }
}