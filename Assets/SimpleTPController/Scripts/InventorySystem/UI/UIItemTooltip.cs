using UnityEngine;
using UnityEngine.EventSystems;

namespace ThirdPersonController.InventorySystem.UI
{
    /// <summary>
    /// Calls to the <see cref="UITooltipController" /> 
    /// </summary>
    [RequireComponent(typeof(UIItemSlot))]
    public class UIItemTooltip : UIBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool m_Tooltip;
        private UIItemSlot m_Slot;
        
        /// <summary>
        /// The current item on this slot.
        /// </summary>
        public ItemDataInstance currentItem => m_Slot?.itemData;

        protected override void Awake()
        {
            base.Awake();
            m_Slot = GetComponent<UIItemSlot>();
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
            OnPointerExit(default);
        }

        protected virtual void Update()
        {
            if (!m_Tooltip) 
            {
                return;
            }

            DrawTooltip(currentItem);
        }

        protected virtual void DrawTooltip(ItemDataInstance slot)
        {
            if (slot == null) 
            {
                return;
            }

            UITooltipController.DrawTooltip(slot);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (currentItem == null) 
            {
                return;
            }

            m_Tooltip = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            m_Tooltip = false;
        }
    }
}