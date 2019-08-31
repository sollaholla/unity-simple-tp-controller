using ThirdPersonController.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ThirdPersonController.InventorySystem.UI
{
    /// <summary>
    /// Renders inventory item tooltips.
    /// </summary>
    public class UITooltipController : UIBehaviour
    {
        [SerializeField] private TMP_Text itemNameText = null;
        [SerializeField] private TMP_Text itemDescriptionText = null;
        [SerializeField] private Image itemImage = null;
        [SerializeField] private float fadeInTime = 0.1f;

        private ItemDataInstance m_CurrentItem;
        private bool m_Draw;
        private bool m_IsDrawing = true;
        private static UITooltipController m_Instance;
        private Graphic[] m_Graphics;
        private Canvas m_Canvas;
        private RectTransform m_RectTransform;

        protected override void Awake()
        {
            base.Awake();

            m_Canvas = GetComponentInParent<Canvas>();
            m_Instance = this;
            m_Graphics = GetComponentsInChildren<Graphic>(true);
            m_RectTransform = (RectTransform)transform;
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
            
            m_CurrentItem = null;
            m_Draw = false;
            m_IsDrawing = false;
        }

        protected override void Start()
        {
            if (EventSystem.current == null)
            {
                Debug.Log("Tooltip Renderer: Event system not found!");
                enabled = false;
            }
        }

        protected virtual void Update()
        {
            if (m_Draw)
            {
                Move();
                if (!m_IsDrawing)
                {
                    ForceUpdateLayout();
                    ResetAlpha();
                    m_IsDrawing = true;
                }
            }
            else if (m_IsDrawing)
            {
                SetAlpha(0, 0);
                m_CurrentItem = null;
                m_IsDrawing = false;
            }

            m_Draw = false;
        }

        private void ResetAlpha()
        {
            SetAlpha(0f, 0f);
            SetAlpha(1f, fadeInTime);
        }

        private void ForceUpdateLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(m_RectTransform);
            m_RectTransform.ForceUpdateRectTransforms();
        }

        protected virtual void Move()
        {
            if (EventSystem.current.currentInputModule == null)
            {
                Debug.Log("Tooltip Renderer: Current input module not found!");
                enabled = false;
                return;
            }

            var mousePos = EventSystem.current.currentInputModule.input.mousePosition;
            var position = UIUtils.KeepWithinScreenBounds(m_Canvas, m_RectTransform, mousePos);
            transform.position = position;
        }

        private void InternalDrawTooltip(ItemDataInstance reference)
        {
            if (m_CurrentItem != null && m_CurrentItem != reference)
            {
                return;
            }

            m_CurrentItem = reference;
            itemImage.sprite = m_CurrentItem.item.icon;
            itemNameText.text = m_CurrentItem.item.displayName;
            itemDescriptionText.text = m_CurrentItem.item.description;
            m_Draw = true;
        }

        private void SetAlpha(float alpha, float time)
        {
            foreach (Graphic g in m_Graphics)
            {
                g.CrossFadeAlpha(alpha, time, true);
            }
        }

        /// <summary>
        /// Draw a tooltip for the given item reference.
        /// </summary>
        /// <param name="reference">The item reference.</param>
        public static void DrawTooltip(ItemDataInstance reference)
        {
            if (m_Instance == null) 
            {
                return;
            }

            m_Instance.InternalDrawTooltip(reference);
        }
    }
}