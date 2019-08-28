using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Overrides the default weapon handler behaviour.
    /// </summary>
    public interface IWeaponHandlerBehaviourOverride
    {
        /// <summary>
        /// True if we can use the primary weapon.
        /// </summary>
        bool CanUsePrimary(Vector3 point, IWeaponItemInstance weapon);

        /// <summary>
        /// True if we can use the secondary weapon.
        /// </summary>
        /// <param name="direction">The secondary use direction.</param>
        /// <param name="use">True if we wish to use this weapon.</param>
        /// <param name="weapon">The weapon we're trying to use.</param>
        /// <returns></returns>
        bool CanUseSecondary(Vector3 direction, bool use, IWeaponItemInstance weapon);
    }
}