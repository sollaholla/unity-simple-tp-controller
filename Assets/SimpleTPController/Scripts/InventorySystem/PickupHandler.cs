using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    /// <summary>
    /// Gives the ability to pickup inventory items.
    /// </summary>
    [RequireComponent(typeof(Inventory))]
    public class PickupHandler : MonoBehaviour
    {
        [SerializeField] private PickupHandlerPhysicsSettings m_PhysicsSettings = null;

        private Collider[] m_Colliders = new Collider[10];
        private InventoryItemInstance[] m_NearbyItems = new InventoryItemInstance[10];
        private InventoryItemInstance m_NearestItem;
        private Inventory m_Inventory;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Inventory = GetComponent<Inventory>();
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            var itemCount = GetNearbyPickups(m_NearbyItems);
            m_NearestItem = HoverPickups(itemCount, m_NearbyItems);
        }

        /// <summary>
        /// Pickup the nearest item.
        /// </summary>
        public virtual void PickupNearestItem()
        {
            Pickup(m_NearestItem);
        }

        /// <summary>
        /// Pickups the given inventory item instance.
        /// </summary>
        /// <param name="item">The item to pickup.</param>
        public virtual void Pickup(InventoryItemInstance item)
        {
            if (item == null)
            {
                return;
            }

            m_Inventory.Add(item);
        }

        protected virtual InventoryItemInstance HoverPickups(int count, InventoryItemInstance[] items)
        {
            if (count == 0)
            {
                return null;
            }

            var nearestDistance = Mathf.Infinity;
            InventoryItemInstance nearest = null;
            for (int i = 0; i < count; i++)
            {
                var item = items[i];
                item.OnHovered();

                var distance = Vector3.SqrMagnitude(item.transform.position - transform.position);
                if (distance < nearestDistance)
                {
                    nearest = item;
                    nearestDistance = distance;
                }
            }

            return nearest;
        }

        protected virtual int GetNearbyPickups(InventoryItemInstance[] items)
        {
            var count = Physics.OverlapSphereNonAlloc(
                transform.position, 
                m_PhysicsSettings.maxPickupDistance, 
                m_Colliders, m_PhysicsSettings.pickupLayers);

            var pickupCount = 0;

            for (int i = 0; i < count; i++)
            {
                var collider = m_Colliders[i];
                var pickup = 
                    collider.GetComponent<InventoryItemInstance>() ?? 
                    collider.attachedRigidbody?.GetComponent<InventoryItemInstance>();
                if (pickup != null)
                {
                    items[pickupCount] = pickup;
                    pickupCount++;

                    if (pickupCount == items.Length)
                    {
                        break;
                    }
                }
            }

            return pickupCount;
        }
    }
}