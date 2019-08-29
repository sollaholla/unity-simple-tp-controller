using UnityEngine;

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

    public abstract class WeaponInventoryItem : EquippableInventoryItem
    {
        private enum WeaponIKHand
        {
            Left,
            Right
        }

        [Header("Animation")]
        [SerializeField] private WeaponIKHand m_IKHand = WeaponIKHand.Left;

        /// <summary>
        /// The avatar IK Goal for this weapon.
        /// </summary>
        public AvatarIKGoal ikGoal 
        {
            get { return m_IKHand == WeaponIKHand.Right ? AvatarIKGoal.RightHand : AvatarIKGoal.LeftHand; }
        }

        [SerializeField] private int m_PrimaryAnimationType = 0;

        /// <summary>
        /// The weapons animation ID. Used when this wepaon is weilded by a character.
        /// </summary>
        public int primaryAnimationType 
        {
            get { return m_PrimaryAnimationType; }
        }

        [SerializeField] private int m_SecondaryAnimationType = 0;
        public int secondaryAnimationType
        {
            get { return m_SecondaryAnimationType; }
        }

        [SerializeField] private string m_AttackAnimationParam = "Attack";
        public string attackAnimationParam
        {
            get { return m_AttackAnimationParam; }
        }
        
        [Header("Cooldowns")]
        [SerializeField] private float m_PrimaryCooldown = 0.75f;

        /// <summary>
        /// The primary cooldown is, for example, the fire-rate in a gun, or the attack cooldown
        /// of a melee weapon.
        /// </summary>
        public float primaryCooldown 
        {
            get { return m_PrimaryCooldown; }
        }

        [SerializeField] private float m_MovementCooldown = 1f;

        /// <summary>
        /// Once this item is successfuly used this will freeze the characters movement for the given time (seconds).
        /// </summary>
        public float movementCooldown
        {
            get { return m_MovementCooldown; }
        }

        [Header("Settings/Other")]
        [SerializeField] private WeaponKind m_WeaponKind = WeaponKind.Primary;

        /// <summary>
        /// The kind of weapon this is (primary, secondary)
        /// </summary>
        public WeaponKind kind
        {
            get { return m_WeaponKind; }
        }

        [SerializeField] private WeaponOccupation m_WeaponOccupation = WeaponOccupation.OneHanded;
        
        /// <summary>
        /// This weapons space occupation.
        /// </summary>
        public WeaponOccupation occupation
        {
            get { return m_WeaponOccupation; }
        }

        [SerializeField] private bool m_CanUseAirally = false;

        /// <summary>
        /// True if we can use this weapon in the air.
        /// </summary>
        public bool canUseAirially
        {
            get { return m_CanUseAirally; }
        }

        [SerializeField] private float m_Damage = 10;

        /// <summary>
        /// The amount of damage this weapon deals.
        /// </summary>
        public float damage
        {
            get { return m_Damage; }
        }
    }
}