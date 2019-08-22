using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(Inventory))]
    public class EquipmentHandler : MonoBehaviour
    {
        [SerializeField] private EquipmentHandlerSettings m_EquipmentSettings = null;

        private Inventory m_Inventory;
        private ItemCollection m_EquipmentCollection;
        private Dictionary<uint, ItemDataInstance> m_ItemInstances = new Dictionary<uint, ItemDataInstance>();

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Inventory = GetComponent<Inventory>();
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            m_EquipmentCollection = m_Inventory.itemCollections[m_EquipmentSettings.equipmentCollectionIndex];
            m_EquipmentCollection.slotChanged += OnEquipmentSlotChanged;
        }

        protected virtual void OnEquipmentSlotChanged(uint slot, ItemDataInstance itemData)
        {
            foreach (var eqSlot in m_EquipmentSettings.equipmentSlots)
            {
                if (eqSlot.slot == slot)
                {
                    if (m_ItemInstances.ContainsKey(slot) && m_ItemInstances[slot] != null)
                    {
                        DisableRenderItem(itemData, eqSlot);
                    }

                    m_ItemInstances[slot] = itemData;
                    EnableRenderItem(itemData, eqSlot);
                    break;
                }
            }
        }

        public virtual void EnableRenderItem(ItemDataInstance itemData, EquipmentSlot eqSlot)
        {
            eqSlot.equipmentRenderer.EnableRenderer(itemData.item);
        }

        public virtual void DisableRenderItem(ItemDataInstance itemData, EquipmentSlot eqSlot)
        {
            eqSlot.equipmentRenderer.DisableRenderer(itemData.item);
        }
    }
}