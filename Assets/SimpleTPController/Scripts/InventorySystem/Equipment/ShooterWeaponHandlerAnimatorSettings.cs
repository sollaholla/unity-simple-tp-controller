using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class ShooterWeaponHandlerAnimatorSettings
    {
        [SerializeField] private string m_ReloadParam = "Reloading";
        public string reloadParam
        {
            get { return m_ReloadParam; }
        }
    }
}