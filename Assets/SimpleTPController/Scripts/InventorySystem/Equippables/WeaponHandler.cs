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
        public event Action<IWeaponItemInstance> weaponUsed;

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
        /// Use this weapon in a primary context.
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

            if (primaryWeapon.PrimaryUse(useRay))
            {
                weaponUsed?.Invoke(primaryWeapon);
                m_CharacterMotor.MoveLock(primaryWeapon.weaponData.movementCooldown);
            }
        }

        /// <summary>
        /// Use this weapon in a secondary context.
        /// </summary>
        public virtual void SecondaryUse()
        {
            if (secondaryWeapon == null)
            {
                return;
            }

            secondaryWeapon.SecondaryUse();
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
        }
    }
}