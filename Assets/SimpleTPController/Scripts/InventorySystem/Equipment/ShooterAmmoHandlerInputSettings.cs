using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class ShooterAmmoHandlerInputSettings
    {
        [SerializeField] private string m_ReloadButton = "Reload";
        public string reloadButton
        {
            get { return m_ReloadButton; }
        }
    }
}