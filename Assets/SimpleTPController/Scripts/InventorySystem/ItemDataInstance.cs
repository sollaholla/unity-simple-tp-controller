using System;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// An item data instance.
    /// </summary>
    [System.Serializable]
    public class ItemDataInstance
    {
        /// <summary>
        /// Create a new instance of <see cref="ItemDataInstance" />
        /// </summary>
        /// <param name="instance">The item monobehaviour/visual instance.</param>
        public ItemDataInstance(InventoryItem item, uint stack)
        {
            this.item = item;
            this.stack = stack;
        }

        /// <summary>
        /// Invoked when the item stack has changed.
        /// </summary>
        public event Action<uint> stackChanged;

        /// <summary>
        /// The inventory item asset.
        /// </summary>
        public InventoryItem item { get; private set; }

        /// <summary>
        /// The current stack for this item data instance.
        /// </summary>
        public uint stack { get; private set; }

        /// <summary>
        /// Sets the item stack to the given value.
        /// </summary>
        /// <param name="stack">The stack.</param>
        public void SetStack(uint stack)
        {
            if (this.stack != stack)
            {
                this.stack = (uint)Mathf.Min(item.maxStack, stack);
                stackChanged?.Invoke(this.stack);
            }
        }
    }
}