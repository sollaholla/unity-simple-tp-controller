namespace ThirdPersonController.InventorySystem
{

    /// <summary>
    /// A base type for equippable inventory items.
    /// </summary>
    /// <typeparam name="TItemType">The item type.</typeparam>
    public class EquippableItemInstanceBase<TItemType> : ItemInstanceBase<TItemType>, IEquippableItemInstance where TItemType : EquippableInventoryItem
    {
        /// <summary>
        /// True if this item is currently equipped.
        /// </summary>
        public bool isEquipped { get; private set; }

        /// <summary>
        /// The inventory item data instance for this equippable item.
        /// </summary>
        public ItemDataInstance instanceData { get; private set; }

        /// <summary>
        /// The equippable item metadata.
        /// </summary>
        public EquippableInventoryItem equippableData => item;

        /// <summary>
        /// The active equipment handler that has equipped this item.
        /// </summary>
        public EquipmentHandler handler { get; private set; }

        /// <summary>
        /// Callback for when this equippable item is equipped.
        /// </summary>
        /// <param name="handler">The handler that equipped this item.</param>
        public virtual void OnEquipped(EquipmentHandler handler, ItemDataInstance data)
        {
            isEquipped = true;
            this.instanceData = data;
            this.handler = handler;
        }

        /// <summary>
        /// Callback for when this equippable item is un-equipped.
        /// </summary>
        /// <param name="handler">The handler that un-equipped this item.</param>
        public virtual void OnUnEquipped(EquipmentHandler handler)
        {
            isEquipped = false;
            this.instanceData = null;
            this.handler = null;
        }
    }
}