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
        /// <param name="useRay">The use direction data.</param>
        bool PrimaryUse(Ray useRay);

        /// <summary>
        /// Use this weapon in a secondary context.
        /// </summary>
        void SecondaryUse(Vector3 direction, bool use);
    }
}