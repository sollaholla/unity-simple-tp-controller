using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class EquipmentRendererItem
    {
        [SerializeField] private InventoryItem m_Item = null;
        public InventoryItem item
        {
            get { return m_Item; }
        }

        [SerializeField] private GameObject m_Renderer = null;
        public GameObject renderer
        {
            get { return m_Renderer; }
        }
    }
}