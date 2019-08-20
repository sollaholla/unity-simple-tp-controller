using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class PickupHandlerPhysicsSettings
    {
        [SerializeField] private float m_MaxPickupDistance = 2f;
        public float maxPickupDistance
        {
            get { return m_MaxPickupDistance; }
            set { m_MaxPickupDistance = value; }
        }

        [SerializeField] private LayerMask m_PickupLayers = Physics.DefaultRaycastLayers;
        public LayerMask pickupLayers
        {
            get { return m_PickupLayers; }
            set { m_PickupLayers = value; }
        }
    }
}