using System;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{

    public class ItemInstanceBase<TItemType> : MonoBehaviour, IItemInstance where TItemType : InventoryItem
    {
        [SerializeField] private TItemType m_Item = null;

        /// <summary>
        /// The current item.
        /// </summary>
        public TItemType item => m_Item;

        /// <summary>
        /// The base inventory item.
        /// </summary>
        public InventoryItem baseData => m_Item;

        /// <summary>
        /// Inveoked when the stack has changed.
        /// </summary>
        public event Action<uint> stackChanged;

        /// <summary>
        /// The stack of this item instance.
        /// </summary>
        public uint stack { get; private set; }

        /// <summary>
        /// Called when the item is being hovered by a pickup handler.
        /// </summary>
        public virtual void OnHovered()
        {
        }

        /// <summary>
        /// Sets the stack of this item instance.
        /// </summary>
        /// <param name="stack">The desired stack.</param>
        public virtual void SetStack(uint stack)
        {
            if (this.stack != stack)
            {
                this.stack = (uint)Mathf.Min(item.maxStack, stack);
                stackChanged?.Invoke(this.stack);
            }
        }
    }
}