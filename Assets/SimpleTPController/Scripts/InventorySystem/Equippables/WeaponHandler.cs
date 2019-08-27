using System;
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

        /// <summary>
        /// The currently equipped (primary) weapon.
        /// </summary>
        public IWeaponItemInstance primaryWeapon { get; private set; }

        /// <summary>
        /// The currently equipped (secondary) weapon.
        /// </summary>
        public IWeaponItemInstance secondaryWeapon { get; private set; }

        /// <summary>
        /// Called when a weapon is used.
        /// </summary>
        public event Action<IWeaponItemInstance> primaryUsed;

        /// <summary>
        /// True if the <see cref="secondaryWeapon" /> is being secondarily used.
        /// </summary>
        public bool usingSecondary { get; private set; }

        /// <summary>
        /// The active secondary use point that's assigned during <see cref="UseSecondary" />.
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
        }

        /// <summary>
        /// Use the <see cref="primaryWeapon" /> in a primary context.
        /// </summary>
        /// <param name="useRay">The directional information for the use action.</param>
        public virtual void PrimaryUse(Ray useRay)
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

            if (primaryWeapon.PrimaryUse(useRay))
            {
                primaryUsed?.Invoke(primaryWeapon);
                m_CharacterMotor.MoveLock(primaryWeapon.weaponData.movementCooldown);
            }
        }

        /// <summary>
        /// Use the <see cref="secondaryWeapon" /> in a secondary context.
        /// </summary>
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
                primaryWeapon = null;
            }
            if (weaponItem == secondaryWeapon)
            {
                secondaryWeapon = null;
            }
        }

        public virtual void SetPrimaryWeapon(IWeaponItemInstance primaryWeapon)
        {
            this.primaryWeapon = primaryWeapon;
        }

        public virtual void SetSecondaryWeapon(IWeaponItemInstance secondaryWeapon)
        {
            this.secondaryWeapon = secondaryWeapon;

            if (secondaryWeapon == null)
            {
                usingSecondary = false;
            }
        }
    }
}