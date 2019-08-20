using System;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class InventoryItemInstance : MonoBehaviour, IPickupObject
    {
        [SerializeField] private InventoryItem m_Item = null;
        
        /// <summary>
        /// The current item.
        /// </summary>
        public InventoryItem item => m_Item;

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