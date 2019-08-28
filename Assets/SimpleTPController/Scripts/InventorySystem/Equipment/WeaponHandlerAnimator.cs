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
        private Vector3 m_SmoothLookat;
        private float m_AimWeight;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_WeaponHandler = GetComponent<WeaponHandler>();
            m_WeaponHandler.primaryUsed += OnWeaponUsed;
        }

        protected virtual void OnWeaponUsed(IWeaponItemInstance weaponItem)
        {
            m_Animator.SetTrigger(weaponItem.weaponData.attackAnimationParam);
        }

        protected virtual bool BlockIK()
        {
            for (int i = 0; i < m_Animator.layerCount; i++)
            {
                var layer = m_Animator.GetCurrentAnimatorStateInfo(i);
                if (layer.IsTag(m_AnimatorSettings.blockIKStateTag))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            AnimateForWeapon(m_WeaponHandler.primaryWeapon, m_AnimatorSettings.primaryWeaponTypeParam);
            AnimateForWeapon(m_WeaponHandler.secondaryWeapon, m_AnimatorSettings.secondaryWeaponTypeParam);

            if (m_WeaponHandler.secondaryWeapon != null && m_WeaponHandler.usingSecondary)
            {
                m_Animator.SetBool(m_AnimatorSettings.secondaryUseAnimationParam, true);
            }
            else
            {
                m_Animator.SetBool(m_AnimatorSettings.secondaryUseAnimationParam, false);
            }
        }

        protected virtual void AnimateForWeapon(IWeaponItemInstance item, string paramName)
        {
            if (item != null)
            {
                m_Animator.SetFloat(
                    paramName, 
                    item.weaponData.primaryAnimationType, 
                    m_AnimatorSettings.weaponTypeAnimDampTime, 
                    Time.deltaTime);
            }
            else
            {
                m_Animator.SetFloat(
                    paramName,
                    0,
                    m_AnimatorSettings.weaponTypeAnimDampTime,
                    Time.deltaTime);
            }
        }

        /// <summary>
        /// LateUpdate is called every frame, if the Behaviour is enabled.
        /// It is called after all Update functions have been called.
        /// </summary>
        protected virtual void LateUpdate()
        {
            UpdateSecondarySpineIK();
        }

        private void UpdateSecondarySpineIK()
        {
            if (m_WeaponHandler.secondaryWeapon == null)
            {
                return;
            }

            if (m_WeaponHandler.usingSecondary && !BlockIK())
            {
                m_AimWeight = Mathf.Lerp(m_AimWeight, 1f, Time.deltaTime * m_AnimatorSettings.aimSmoothRate);
            }
            else
            {
                m_AimWeight = Mathf.Lerp(m_AimWeight, 0f, Time.deltaTime * m_AnimatorSettings.aimSmoothRate);
            }

            var spineBone = m_Animator.GetBoneTransform(HumanBodyBones.Spine);
            var rotation = Quaternion.LookRotation(m_WeaponHandler.secondaryUseDirection);

            spineBone.transform.rotation = Quaternion.Lerp(
                spineBone.transform.rotation,
                rotation * m_AnimatorSettings.secondarySpineRotationOffset,
                m_AimWeight);
        }

        /// <summary>
        /// Callback for setting up animation IK (inverse kinematics).
        /// </summary>
        /// <param name="layerIndex">Index of the layer on which the IK solver is called.</param>
        protected virtual void OnAnimatorIK(int layerIndex)
        {
            var primaryWeapon = m_WeaponHandler.primaryWeapon;
            switch(layerIndex)
            {
                case 0:
                    if (primaryWeapon != null && primaryWeapon.ikTransform != null && !BlockIK())
                    {
                        m_Animator.SetIKPosition(primaryWeapon.weaponData.ikGoal, primaryWeapon.ikTransform.position);
                        m_Animator.SetIKRotation(primaryWeapon.weaponData.ikGoal, primaryWeapon.ikTransform.rotation);
                        m_Animator.SetIKPositionWeight(primaryWeapon.weaponData.ikGoal, 1f);
                        m_Animator.SetIKRotationWeight(primaryWeapon.weaponData.ikGoal, 1f);
                    }
                    break;
            }
        }
    }
}