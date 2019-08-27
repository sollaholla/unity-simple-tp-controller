using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class WeaponHandlerAnimatorSettings
    {
        [SerializeField] private string m_PrimaryWeaponTypeParam = "PrimaryWeaponType";
        public string primaryWeaponTypeParam
        {
            get { return m_PrimaryWeaponTypeParam; }
        }
        
        [SerializeField] private string m_SecondaryWeaponTypeParam = "SecondaryWeaponType";
        public string secondaryWeaponTypeParam
        {
            get { return m_SecondaryWeaponTypeParam; }
        }

        [Range(0, 1)]
        [SerializeField] private float m_WeaponTypeAnimDampTime = 0.1f;
        public float weaponTypeAnimDampTime
        {
            get { return m_WeaponTypeAnimDampTime; }
        }
        
        [SerializeField] private string m_SecondaryUseAnimationParam = "Block/Aim";
        public string secondaryUseAnimationParam
        {
            get { return m_SecondaryUseAnimationParam; }
        }

        [Range(1f, 15)]
        [SerializeField] private float m_AimSmoothRate = 5f;
        public float aimSmoothRate
        {
            get { return m_AimSmoothRate; }
        }

        [SerializeField] private Vector3 m_SecondarySpineRotationOffset = default;
        public Quaternion secondarySpineRotationOffset
        {
            get { return Quaternion.Euler(m_SecondarySpineRotationOffset); }
        }
    }
}