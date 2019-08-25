using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(EquipmentHandler))]
    [RequireComponent(typeof(Inventory))]
    public class WeaponHandler : MonoBehaviour
    {
        private EquipmentHandler m_EquipmentHandler;
        private Inventory m_Inventory;

        /// <summary>
        /// The currently equipped (primary) weapon.
        /// </summary>
        public WeaponItemInstance primaryWeapon { get; private set; }

        /// <summary>
        /// The currently equipped (secondary) weapon.
        /// </summary>
        public WeaponItemInstance secondaryWeapon { get; private set; }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_EquipmentHandler = GetComponent<EquipmentHandler>();
            m_EquipmentHandler.equipped += OnEquippedItem;
            m_EquipmentHandler.unequipped += OnUnEquippedItem;
            
            m_Inventory = GetComponent<Inventory>();
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

            primaryWeapon.PrimaryUse(useRay);
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
            var weaponItem = item as WeaponItemInstance;
            if (weaponItem == null)
            {
                return;
            }
            
            switch(weaponItem.item.occupation)
            {
                case WeaponOccupation.OneHanded:
                    if (primaryWeapon != null && primaryWeapon.item.occupation == WeaponOccupation.TwoHanded)
                    {
                        m_Inventory.AutoMoveItem(primaryWeapon.itemData);
                    }
                    if (secondaryWeapon != null && secondaryWeapon.item.occupation == WeaponOccupation.TwoHanded)
                    {
                        m_Inventory.AutoMoveItem(secondaryWeapon.itemData);
                    }
                    break;
                case WeaponOccupation.TwoHanded:
                    switch(weaponItem.item.kind)
                    {
                        case WeaponKind.Primary:
                            if (secondaryWeapon != null)
                            {
                                m_Inventory.AutoMoveItem(secondaryWeapon.itemData);
                            }
                            break;
                        case WeaponKind.Secondary:
                            if (primaryWeapon != null)
                            {
                                m_Inventory.AutoMoveItem(primaryWeapon.itemData);
                            }
                            break;
                    }
                    break;
            }

            switch (weaponItem.item.kind)
            {
                case WeaponKind.Primary:
                    SetPrimaryWeapon(weaponItem);
                    break;
                case WeaponKind.Secondary:
                    SetSecondaryWeapon(weaponItem);
                    break;
            }
        }

        protected virtual void OnUnEquippedItem(IEquippableItemInstance item)
        {
            var weaponItem = item as WeaponItemInstance;
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

        public virtual void SetPrimaryWeapon(WeaponItemInstance primaryWeapon)
        {
            this.primaryWeapon = primaryWeapon;
        }

        public virtual void SetSecondaryWeapon(WeaponItemInstance secondaryWeapon)
        {
            this.secondaryWeapon = secondaryWeapon;
        }
    }
}