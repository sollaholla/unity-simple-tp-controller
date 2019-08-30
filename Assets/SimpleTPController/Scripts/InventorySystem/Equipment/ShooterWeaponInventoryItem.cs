using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/ShooterWeaponInventoryItem")]
    public class ShooterWeaponInventoryItem : WeaponInventoryItem
    {
        [Header("Ammunition")]
        [SerializeField] private uint m_MaxAmmo = 15;
        public uint maxAmmo
        {
            get { return m_MaxAmmo; }
        }
        [SerializeField] private AmmoInventoryItem m_AmmoItemType = null;
        public AmmoInventoryItem ammoItemType
        {
            get { return m_AmmoItemType; }
        }

        [Header("Weapon Animation")]
        [SerializeField] private float m_ReloadDuration = 1.5f;
        public float reloadDuration
        {
            get { return m_ReloadDuration; }
        }

        [Header("Player")]
        [SerializeField] private float m_ZoomLevel = 30f;
        public float zoomLevel 
        {
            get { return m_ZoomLevel; }
        }
        [SerializeField] private float m_ZoomSpeed = 10f;
        public float zoomSpeed
        {
            get { return m_ZoomSpeed; }
        }

        [SerializeField] private float m_RecoilStrengthX = 0.005f;
        public float recoilStrengthX
        {
            get { return m_RecoilStrengthX; }
        }

        [SerializeField] private float m_RecoilStrengthY = 0.015f;
        public float recoilStrengthY
        {
            get { return m_RecoilStrengthY; }
        }
    }
}