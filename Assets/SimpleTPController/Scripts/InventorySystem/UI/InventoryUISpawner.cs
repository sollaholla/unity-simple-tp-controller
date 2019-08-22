using UnityEngine;

namespace ThirdPersonController.InventorySystem.UI
{
    /// <summary>
    /// Responsible for spawning inventory UI and initializing it with the item collections.
    /// </summary>
    [RequireComponent(typeof(Inventory))]
    public class InventoryUISpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_InventoryUIPrefab = null;

        private Inventory m_Inventory;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Inventory = GetComponent<Inventory>();
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            var uiInst = Instantiate(m_InventoryUIPrefab);
            var collectionUIs = uiInst.GetComponentsInChildren<UIItemCollection>();
            foreach (var collectionUI in collectionUIs)
            {
                var collection = m_Inventory.GetCollection(collectionUI.collectionName);
                collectionUI.Initialize(collection);
            }
        }
    }
}