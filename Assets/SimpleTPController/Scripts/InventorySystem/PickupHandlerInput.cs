using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Passes input to the <see cref="PickupHandler" /> to pickup items.
    /// </summary>
    [RequireComponent(typeof(PickupHandler))]
    public class PickupHandlerInput : MonoBehaviour
    {
        [SerializeField] private string m_PickupInput = "Pickup";

        private PickupHandler m_PickupHandler;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_PickupHandler = GetComponent<PickupHandler>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            if (Input.GetButtonDown(m_PickupInput))
            {
                m_PickupHandler.PickupNearestItem();
            }
        }
    }
}