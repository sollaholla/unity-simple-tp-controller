using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Descirbes a weapon item instance.
    /// </summary>
    public interface IWeaponItemInstance : IEquippableItemInstance
    {
        /// <summary>
        /// The hand IK transform.
        /// </summary>
        Transform ikTransform { get; }

        /// <summary>
        /// The weapon item metadata.
        /// </summary>
        /// <value></value>
        WeaponInventoryItem weaponData { get; }

        /// <summary>
        /// Use this weapon in a primary context.
        /// </summary>
        bool PrimaryUse(Vector3 point);

        /// <summary>
        /// Use this weapon in a secondary context.
        /// </summary>
        void SecondaryUse(Vector3 direction, bool use);
    }
}