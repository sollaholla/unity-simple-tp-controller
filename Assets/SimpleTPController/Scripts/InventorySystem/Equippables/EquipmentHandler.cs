using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Handles visual equipment for equippable inventory items.
    /// </summary>
    [RequireComponent(typeof(Inventory))]
    public class EquipmentHandler : MonoBehaviour
    {
        [SerializeField] private string[] m_EquipmentCollections = new string[] { "Character", "Weapons" };
        
        private Inventory m_Inventory;
        private VisualEquipment[] m_EquipmentVisuals = null;

        private Dictionary<ItemCollection, Dictionary<uint, InventoryItem>> m_Equipment = 
            new Dictionary<ItemCollection, Dictionary<uint, InventoryItem>>();

        public event Action<IEquippableItemInstance> equipped;
        public event Action<IEquippableItemInstance> unequipped;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeEquipmentVisuals();
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            m_Inventory = GetComponent<Inventory>();

            foreach (var collectionName in m_EquipmentCollections)
            {
                var collection = m_Inventory.GetCollection(collectionName);
                if (collection == null)
                {
                    continue;
                }

                collection.slotChanged += (slot, itemData) => OnSlotChanged(collection, slot, itemData);
            }
        }

        protected virtual void InitializeEquipmentVisuals()
        {
            var visuals = new List<VisualEquipment>();
            var itemInstances = GetComponentsInChildren<IItemInstance>(true);
            foreach (var itemInstance in itemInstances)
            {
                visuals.Add(new VisualEquipment(itemInstance.baseItem, itemInstance.transform));
            }
            m_EquipmentVisuals = visuals.ToArray();
        }

        protected virtual void OnSlotChanged(ItemCollection collection, uint slot, ItemDataInstance itemData)
        {
            if (!m_Equipment.TryGetValue(collection, out var targetEquipment))
            {
                targetEquipment = m_Equipment[collection] = new Dictionary<uint, InventoryItem>();
            }

            if (targetEquipment.ContainsKey(slot) && targetEquipment[slot] != null)
            {
                SetVisualActive(targetEquipment[slot], itemData, false);
                targetEquipment[slot] = null;
            }

            if (itemData == null)
            {
                return;
            }

            SetVisualActive(itemData.item, itemData, true);
            targetEquipment[slot] = itemData.item;
        }

        protected virtual void SetVisualActive(InventoryItem item, ItemDataInstance itemData, bool active)
        {
            var visualHandlers = Array.FindAll(m_EquipmentVisuals, x => x.itemType == item);
            foreach (var visualHandler in visualHandlers)
            {
                visualHandler.visualTransform.gameObject.SetActive(active);
                var equippableItem = visualHandler.visualTransform.GetComponent<IEquippableItemInstance>();
                if (equippableItem != null)
                {
                    if (active)
                    {
                        equippableItem.OnEquipped(this, itemData);
                        equipped?.Invoke(equippableItem);
                    }
                    else
                    {
                        equippableItem.OnUnEquipped(this);
                        unequipped?.Invoke(equippableItem);
                    }
                }
            }
        }
    }
}