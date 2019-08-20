using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Gives the ability to pickup inventory items.
    /// </summary>
    [RequireComponent(typeof(Inventory))]
    public class PickupHandler : MonoBehaviour
    {
        [SerializeField] private PickupHandlerPhysicsSettings m_PhysicsSettings = null;
    }
}