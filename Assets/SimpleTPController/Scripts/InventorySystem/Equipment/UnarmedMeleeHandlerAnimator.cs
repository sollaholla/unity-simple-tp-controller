using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(UnarmedMeleeHandler))]
    public class UnarmedMeleeHandlerAnimator : MonoBehaviour
    {
        [SerializeField] private UnarmedMeleeHandlerAnimatorSettings m_AnimationSettings = null;

        private Animator m_Animator;
        private UnarmedMeleeHandler m_MeleeHandler;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_MeleeHandler = GetComponent<UnarmedMeleeHandler>();

            m_MeleeHandler.meleeAttack += OnMeleeAttack;
        }

        protected virtual void OnMeleeAttack(int meleeID)
        {
            m_Animator.SetFloat(m_AnimationSettings.meleeIDParam, meleeID);
            m_Animator.SetTrigger(m_AnimationSettings.meleeAnimParam);
        }
    }
}