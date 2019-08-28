using System;
using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(EquipmentHandler))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(CharacterMotor))]
    public class WeaponHandler : MonoBehaviour
    {
        private EquipmentHandler m_EquipmentHandler;
        private Inventory m_Inventory;
        private CharacterMotor m_CharacterMotor;
        private IWeaponHandlerBehaviourOverride[] m_BehaviourOverrides;

        /// <summary>
        /// The currently equipped (primary) weapon.
        /// </summary>
        public IWeaponItemInstance primaryWeapon { get; private set; }

        /// <summary>
        /// The currently equipped (secondary) weapon.
        /// </summary>
        public IWeaponItemInstance secondaryWeapon { get; private set; }

        /// <summary>
        /// True if the secondary weapon is the primary weapon and the weapons are equipped.
        /// </summary>
        public bool isTwoHanded => secondaryWeapon != null && secondaryWeapon == primaryWeapon;

        /// <summary>
        /// Invoked when the primary weapon is used.
        /// </summary>
        public event Action<IWeaponItemInstance> primaryUsed;

        /// <summary>
        /// Invoked when the primary weapon has changed.
        /// </summary>
        public event Action<IWeaponItemInstance> primaryWeaponChanged;

        /// <summary>
        /// Invoked when the secondary weapon has changed.
        /// </summary>
        public event Action<IWeaponItemInstance> secondaryWeaponChanged;

        /// <summary>
        /// True if the <see cref="secondaryWeapon" /> is being used.
        /// </summary>
        public bool usingSecondary { get; private set; }

        /// <summary>
        /// The direction in which the <see cref="secondaryWeapon" /> was used.
        /// </summary>
        public Vector3 secondaryUseDirection { get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_EquipmentHandler = GetComponent<EquipmentHandler>();
            m_EquipmentHandler.equipped += OnEquippedItem;
            m_EquipmentHandler.unequipped += OnUnEquippedItem;
            
            m_Inventory = GetComponent<Inventory>();
            m_CharacterMotor = GetComponent<CharacterMotor>();
            m_BehaviourOverrides = GetComponents<IWeaponHandlerBehaviourOverride>();
        }

        /// <summary>
        /// Use the <see cref="primaryWeapon" />.
        /// </summary>
        public virtual void PrimaryUse(Vector3 point)
        {
            if (primaryWeapon == null)
            {
                return;
            }

            if (!m_CharacterMotor.isGrounded && !primaryWeapon.weaponData.canUseAirially)
            {
                return;
            }

            if (usingSecondary && secondaryWeapon != primaryWeapon)
            {
                return;
            }

            if (!m_BehaviourOverrides.Any(x => x.CanUsePrimary(point, primaryWeapon)))
            {
                return;
            }

            if (primaryWeapon.PrimaryUse(point))
            {
                m_CharacterMotor.MoveLock(primaryWeapon.weaponData.movementCooldown);
                primaryUsed?.Invoke(primaryWeapon);
            }
        }

        /// <summary>
        /// Use the <see cref="secondaryWeapon" />.
        /// </summary>
        /// <param name="useDirection">The direction that the weapon was used (e.g. aim direction).</param>
        /// <param name="use">Set to true to use this item now.</param>
        public virtual void SecondaryUse(Vector3 useDirection, bool use)
        {
            if (secondaryWeapon == null)
            {
                usingSecondary = false;
                return;
            }

            if (!m_CharacterMotor.isGrounded && !secondaryWeapon.weaponData.canUseAirially)
            {
                use = false;
            }

            if (m_CharacterMotor.isMovementLocked)
            {
                use = false;
            }
            
            if (!m_BehaviourOverrides.Any(x => x.CanUseSecondary(useDirection, use, secondaryWeapon)))
            {
                use = false;
            }

            secondaryUseDirection = useDirection;
            secondaryWeapon.SecondaryUse(useDirection, use);
            usingSecondary = use;
        }

        protected virtual void OnEquippedItem(IEquippableItemInstance item)
        {
            var weaponItem = item as IWeaponItemInstance;
            if (weaponItem == null)
            {
                return;
            }

            HandleWeaponOccupation(weaponItem);

            switch (weaponItem.weaponData.kind)
            {
                case WeaponKind.Primary:
                    SetPrimaryWeapon(weaponItem);
                    if (weaponItem.weaponData.occupation == WeaponOccupation.TwoHanded)
                    {
                        SetSecondaryWeapon(weaponItem);
                    }
                    break;
                case WeaponKind.Secondary:
                    SetSecondaryWeapon(weaponItem);
                    break;
            }
        }

        // This method essentially clears the players weapon slots
        // depending on the weapon that was just equipped.
        // (e.g. removing a shield when equipping a two handed weapon)
        private void HandleWeaponOccupation(IWeaponItemInstance weaponItem)
        {
            switch (weaponItem.weaponData.occupation)
            {
                case WeaponOccupation.OneHanded:
                    HandleOneHanded(primaryWeapon);
                    HandleOneHanded(secondaryWeapon);
                    break;
                case WeaponOccupation.TwoHanded:
                    switch (weaponItem.weaponData.kind)
                    {
                        case WeaponKind.Primary:
                            MoveWeapon(secondaryWeapon);
                            break;
                        case WeaponKind.Secondary:
                            MoveWeapon(primaryWeapon);
                            break;
                    }
                    break;
            }
        }

        private void HandleOneHanded(IWeaponItemInstance currentWeapon)
        {
            if (currentWeapon == null)
            {
                return;
            }

            if (currentWeapon.weaponData.occupation == WeaponOccupation.TwoHanded)
            {
                MoveWeapon(currentWeapon);
            }
        }

        private void MoveWeapon(IWeaponItemInstance weaponItem)
        {
            if (weaponItem == null)
            {
                return;
            }

            m_Inventory.AutoMoveItem(weaponItem.instanceData, false);
        }

        protected virtual void OnUnEquippedItem(IEquippableItemInstance item)
        {
            var weaponItem = item as IWeaponItemInstance;
            if (weaponItem == null)
            {
                return;
            }
            
            if (weaponItem == primaryWeapon)
            {
                SetPrimaryWeapon(null);
            }
            if (weaponItem == secondaryWeapon)
            {
                SetSecondaryWeapon(null);
            }
        }

        public virtual void SetPrimaryWeapon(IWeaponItemInstance primaryWeapon)
        {
            if (this.primaryWeapon != primaryWeapon)
            {
                this.primaryWeapon = primaryWeapon;
                primaryWeaponChanged?.Invoke(primaryWeapon);
            }
        }

        public virtual void SetSecondaryWeapon(IWeaponItemInstance secondaryWeapon)
        {
            if (this.secondaryWeapon != secondaryWeapon)
            {
                this.secondaryWeapon = secondaryWeapon;
                secondaryWeaponChanged?.Invoke(secondaryWeapon);
            }

            if (secondaryWeapon == null)
            {
                usingSecondary = false;
            }
        }
    }
}