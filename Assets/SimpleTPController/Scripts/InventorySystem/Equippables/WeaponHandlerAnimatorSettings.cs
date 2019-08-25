using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class WeaponHandlerAnimatorSettings
    {
        [SerializeField] private string m_WeaponIDParam = "WeaponID";
        public string weaponIDParam
        {
            get { return m_WeaponIDParam; }
        }

        [Range(0, 1)]
        [SerializeField] private float m_WeaponIDDampTime = 0.1f;
        public float weaponIDDampTime
        {
            get { return m_WeaponIDDampTime; }
        }
    }
}