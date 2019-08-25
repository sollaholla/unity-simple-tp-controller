using UnityEngine;
using static ThirdPersonController.InventorySystem.WeaponItemInstance;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// The kind of weapon (e.g. a primary or secondary weapon)
    /// </summary>
    public enum WeaponKind
    {
        Primary,
        Secondary
    }

    /// <summary>
    /// This weapons space occupation (e.g. one-handed/two-handed)
    /// </summary>
    public enum WeaponOccupation
    {
        OneHanded,
        TwoHanded
    }

    [CreateAssetMenu(menuName = "Inventory/WeaponItem")]
    public class WeaponInventoryItem : EquippableInventoryItem
    {
        private enum WeaponIKHand
        {
            Left,
            Right
        }

        [SerializeField] private WeaponIKHand m_IKHand = WeaponIKHand.Left;

        /// <summary>
        /// The avatar IK Goal for this weapon.
        /// </summary>
        public AvatarIKGoal ikGoal 
        {
            get { return m_IKHand == WeaponIKHand.Right ? AvatarIKGoal.RightHand : AvatarIKGoal.LeftHand; }
        }

        [SerializeField] private int m_AnimationID = 0;

        /// <summary>
        /// The weapons animation ID. Used when this wepaon is weilded by a character.
        /// </summary>
        public int animationID 
        {
            get { return m_AnimationID; }
        }
        
        [SerializeField] private float m_PrimaryCooldown = 0.75f;

        /// <summary>
        /// The primary cooldown is, for example, the fire-rate in a gun, or the attack cooldown
        /// of a melee weapon.
        /// </summary>
        public float primaryCooldown 
        {
            get { return m_PrimaryCooldown; }
        }

        [SerializeField] private WeaponKind m_WeaponKind = WeaponKind.Primary;
        public WeaponKind kind
        {
            get { return m_WeaponKind; }
        }

        [SerializeField] private WeaponOccupation m_WeaponOccupation = WeaponOccupation.OneHanded;
        public WeaponOccupation occupation
        {
            get { return m_WeaponOccupation; }
        }
    }
}