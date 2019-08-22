using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class EquipmentRenderer : MonoBehaviour
    {
        [SerializeField] private EquipmentRendererItem[] m_RendererItems = null;

        public virtual void EnableRenderer(InventoryItem item)
        {
            var rendererItem = m_RendererItems.FirstOrDefault(x => x.item.id == item.id);
            rendererItem.renderer.gameObject.SetActive(true);
        }

        public virtual void DisableRenderer(InventoryItem item)
        {
            var rendererItem = m_RendererItems.FirstOrDefault(x => x.item.id == item.id);
            rendererItem.renderer.gameObject.SetActive(false);
        }
    }
}