using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// A simple pickup spawner that will spawn a pickup prefab and set
    /// the starting stack for the inventory item.
    /// </summary>
    public class SimplePickupSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_Prefab = null;
        [SerializeField] private uint m_StartingStack = 1;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            var objInstance = Instantiate(m_Prefab, transform.position, transform.rotation);
            var itemInstance = objInstance.GetComponent<IItemInstance>();
            itemInstance.SetStack(m_StartingStack);
        }
    }
}