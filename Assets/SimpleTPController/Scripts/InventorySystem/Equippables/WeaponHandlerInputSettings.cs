using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class WeaponHandlerInputSettings
    {
        [SerializeField] private string m_PrimaryUseButton = "Fire1";
        public string primaryUseButton
        {
            get { return m_PrimaryUseButton; }
        }

        [SerializeField] private string m_SecondaryUseButton = "Fire2";
        public string secondaryUseButton
        {
            get { return m_SecondaryUseButton; }
        }
    }
}