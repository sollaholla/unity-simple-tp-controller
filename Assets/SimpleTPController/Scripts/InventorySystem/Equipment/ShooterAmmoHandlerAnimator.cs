using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ShooterAmmoHandler))]
    public class ShooterAmmoHandlerAnimator : MonoBehaviour
    {
        [SerializeField] private ShooterAmmoHandlerAnimatorSettings m_AnimationSettings = null;

        private Animator m_Animator;
        private ShooterAmmoHandler m_AmmoHandler;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_AmmoHandler = GetComponent<ShooterAmmoHandler>();
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