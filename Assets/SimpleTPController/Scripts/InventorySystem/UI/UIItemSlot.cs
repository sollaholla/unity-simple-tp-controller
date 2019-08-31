using System;
using ThirdPersonController.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ThirdPersonController.InventorySystem.UI
{
    /// <summary>
    /// A user interface representation of an inventory item slot.
    /// </summary>
    public class UIItemSlot : UIDragTransferHandler<ItemDataInstance>, IPointerDownHandler
    {
        [SerializeField] private Image m_IconImage = null;
        [SerializeField] private TMPro.TMP_Text m_StackText = null;

        private Sprite m_InitialIcon;

        /// <summary>
        /// The UI item collection.
        /// </summary>
        public UIItemCollection uiItemCollection { get; private set; }

        /// <summary>
        /// The item in this slot.
        /// </summary>
        public ItemDataInstance itemData { get; private set; }

        /// <summary>
        /// The slot index.
        /// </summary>
        /// <value></value>
        public uint slot { get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_InitialIcon = m_IconImage.sprite;
            m_StackText.text = string.Empty;
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (itemData != null)
            {
                itemData.stackChanged -= OnStackChanged;
            }
        }

        /// <summary>
        /// Initialize this item slot with the given UI collection.
        /// </summary>
        /// <param name="uiItemCollection">The UI collection.</param>
        public virtual void Initialize(uint slot, UIItemCollection uiItemCollection)
        {
            this.slot = slot;
            this.uiItemCollection = uiItemCollection;
        }

        /// <summary>
        /// Called when this item slot's item has changed.
        /// </summary>
        /// <param name="itemData">The item data instance.</param>
        public virtual void OnChanged(ItemDataInstance itemData)
        {
            if (this.itemData != null)
            {
                this.itemData.stackChanged -= OnStackChanged;
            }

            this.itemData = itemData;

            if (this.itemData != null)
            {
                this.itemData.stackChanged += OnStackChanged;
            }

            if (itemData == null)
            {
                SetStackText(0);
                m_IconImage.sprite = m_InitialIcon;
            }
            else
            {
                SetStackText(itemData.stack);
                m_IconImage.sprite = itemData.item.icon;
            }
        }

        protected virtual void OnStackChanged(uint newStack)
        {
            SetStackText(newStack);
        }

        protected virtual void SetStackText(uint stack)
        {
            if (stack <= 1)
            {
                m_StackText.text = string.Empty;
            }
            else
            {
                m_StackText.text = $"x{stack}";
            }
        }

        public override ItemDataInstance GetDragableData()
        {
            return itemData;
        }

        public override bool CanDrag()
        {
            return itemData != null;
        }

        public override void OnReceiveDrag(UIDragTransferHandler<ItemDataInstance> sender, ItemDataInstance data)
        {
            if (!(sender is UIItemSlot uiSlot))
            {
                return;
            }

            var inventory = uiItemCollection.itemCollection.inventory;
            inventory.MoveItem(data, uiItemCollection.itemCollection, this.slot);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (this.itemData == null)
            {
                return;
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                var inventory = uiItemCollection.itemCollection.inventory;
                inventory.AutoMoveItem(this.itemData, false);
            }
        }
    }
}