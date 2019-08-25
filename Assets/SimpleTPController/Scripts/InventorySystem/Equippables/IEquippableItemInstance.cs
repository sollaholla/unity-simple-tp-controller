namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Defines an equippable item instance.
    /// </summary>
    public interface IEquippableItemInstance
    {
        /// <summary>
        /// True if this item is equipped.
        /// </summary>
        bool isEquipped { get; }

        /// <summary>
        /// The item data instance for this equippable item.
        /// </summary>
        ItemDataInstance itemData { get; }

        /// <summary>
        /// Called when this item has been equipped by the handler.
        /// </summary>
        /// <param name="handler">The equippable handler.</param>
        /// <param name="data">The item data instance.</param>
        void OnEquipped(EquipmentHandler handler, ItemDataInstance data);

        /// <summary>
        /// Called when this item is unequipped by the handler.
        /// </summary>
        /// <param name="handler">The equippable handler.</param>
        void OnUnEquipped(EquipmentHandler handler);
    }
}