using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Defines a base item instance type.
    /// </summary>
    public interface IItemInstance : IPickupObject
    {
        /// <summary>
        /// The base item type.
        /// </summary>
        InventoryItem baseItem { get; }

        /// <summary>
        /// The <see cref="GameObject"> associated with this item instance.
        /// </summary>
        GameObject gameObject { get; }
        
        /// <summary>
        /// The <see cref="Transform"> associated with this item instance.
        /// </summary>
        Transform transform { get; }

        /// <summary>
        /// The stack of this item instance.
        /// </summary>
        uint stack { get; }

        /// <summary>
        /// Sets the item instance's current stack.
        /// </summary>
        void SetStack(uint stack);
    }
}