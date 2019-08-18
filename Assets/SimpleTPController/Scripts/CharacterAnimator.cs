using UnityEngine;

namespace ThirdPersonController
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterAnimatorSettings m_AnimatorSettings = null;

        private CharacterMotor m_CharacterMotor;
        private Animator m_Animator;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_CharacterMotor = GetComponent<CharacterMotor>();
            m_Animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            var relativeVelocity = transform.InverseTransformDirection(m_CharacterMotor.velocity);
            m_Animator.SetFloat(m_AnimatorSettings.velocityXParameter, relativeVelocity.x, 
                m_AnimatorSettings.movementDampTime, Time.deltaTime);
            m_Animator.SetFloat(m_AnimatorSettings.velocityZParameter, relativeVelocity.z, 
                m_AnimatorSettings.movementDampTime, Time.deltaTime);

            m_Animator.SetBool(m_AnimatorSettings.groundedParameter, m_CharacterMotor.isGrounded);
            m_Animator.SetBool(m_AnimatorSettings.jumpingParameter, m_CharacterMotor.isJumping);
            m_Animator.SetBool(m_AnimatorSettings.crouchingParameter, m_CharacterMotor.isCrouching);
        }
    }
}