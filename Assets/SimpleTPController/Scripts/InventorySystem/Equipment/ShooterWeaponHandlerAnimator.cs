using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ShooterWeaponHandler))]
    public class ShooterWeaponHandlerAnimator : MonoBehaviour
    {
        [SerializeField] private ShooterWeaponHandlerAnimatorSettings m_AnimationSettings = null;

        private Animator m_Animator;
        private ShooterWeaponHandler m_AmmoHandler;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_AmmoHandler = GetComponent<ShooterWeaponHandler>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            m_Animator.SetBool(m_AnimationSettings.reloadParam, m_AmmoHandler.isReloading);
        }
    }
}