using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/MeleeWeaponItem")]
    public class MeleeWeaponInventoryItem : WeaponInventoryItem
    {
        [Header("Melee")]
        [SerializeField] private float m_DamageCooldown = 1.5f;

        /// <summary>
        /// The duration that this melee weapon attempts to deal damage after it is primarily used.
        /// </summary>
        public float damageCooldown
        {
            get { return m_DamageCooldown; }
        }
        
        [SerializeField] private float m_DamageDelay = 0.5f;

        /// <summary>
        /// The amount of delay before applying damage.
        /// </summary>
        public float damageDelay
        {
            get { return m_DamageDelay; }
        }
    }
}