using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class ShooterWeaponHandlerInputSettings
    {
        [SerializeField] private string m_ReloadButton = "Reload";
        public string reloadButton
        {
            get { return m_ReloadButton; }
        }

        [SerializeField] private float m_AimRecoilMultiplier = 0.25f;
        public float aimRecoilMultiplier
        {
            get { return m_AimRecoilMultiplier; }
        }
    }
}