using System;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(WeaponHandler))]
    public class WeaponHandlerAnimator : MonoBehaviour
    {
        [SerializeField] private WeaponHandlerAnimatorSettings m_AnimatorSettings = null;

        private Animator m_Animator;
        private WeaponHandler m_WeaponHandler;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_WeaponHandler = GetComponent<WeaponHandler>();
            m_WeaponHandler.weaponUsed += OnWeaponUsed;
        }

        protected virtual void OnWeaponUsed(IWeaponItemInstance weaponItem)
        {
            m_Animator.SetTrigger(weaponItem.weaponData.attackAnimationParam);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            if (m_WeaponHandler.primaryWeapon != null)
            {
                m_Animator.SetFloat(
                    m_AnimatorSettings.weaponIDParam, 
                    m_WeaponHandler.primaryWeapon.weaponData.animationID, 
                    m_AnimatorSettings.weaponIDDampTime, 
                    Time.deltaTime);
            }
            else
            {
                m_Animator.SetFloat(
                    m_AnimatorSettings.weaponIDParam,
                    0,
                    m_AnimatorSettings.weaponIDDampTime,
                    Time.deltaTime);
            }
        }
    }
}