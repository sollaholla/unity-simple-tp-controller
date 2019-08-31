using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class WeaponCameraControllerInputSettings
    {
        [SerializeField] private string m_AimingState = "Aim";
        public string aimingState
        {
            get { return m_AimingState; }
        }
    }
}