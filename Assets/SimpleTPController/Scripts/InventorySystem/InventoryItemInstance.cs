using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class InventoryItemInstance : MonoBehaviour
    {
        [SerializeField] private InventoryItem m_Item = null;
        
        public InventoryItem item => m_Item;

        public uint stack { get; set; }
    }
}