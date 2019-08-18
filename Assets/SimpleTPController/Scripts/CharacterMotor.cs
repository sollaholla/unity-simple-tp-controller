using System;
using UnityEngine;

namespace ThirdPersonController
{
    /// <summary>
    /// Allows for basic character movement physics and actions.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMotor : MonoBehaviour
    {
        [SerializeField] private CharacterMotorMovementSettings m_MovementSettings = null;
        [SerializeField] private CharacterMotorPhysicsSettings m_PhysicsSettings = null;

        private CharacterController m_CharacterController;
        private Vector3 m_Motion;

        /// <summary>
        /// Gets a value indicating whether the character is on the ground.
        /// </summary>
        public bool isGrounded { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the character is sprinting.
        /// </summary>
        public bool isSprinting { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether the character is crouching.
        /// </summary>
        public bool isCrouching { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether the character is jumping.
        /// </summary>
        public bool isJumping { get; private set; }

        /// <summary>
        /// Gets the velocity of the <see cref="CharacterController" />
        /// </summary>
        public Vector3 velocity => m_CharacterController.velocity;
        
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
        }

        /// <summary>
        /// Drives the <see cref="CharacterController" /> using the given input values.
        /// </summary>
        /// <param name="x">The horizontal input value.</param>
        /// <param name="z">The vertical input value.</param>
        /// <param name="sprint">The sprinting input value.</param>
        /// <param name="crouch">The crocuhing input value.</param>
        /// <param name="jump">The jumping input value.</param>
        public virtual void Move(float x, float z, bool sprint, bool crouch, bool jump)
        {
            if (isGrounded)
            {
                isCrouching = crouch;
                isSprinting = sprint && !isCrouching;

                m_Motion = new Vector3(x, 0, z).normalized * GetDesiredMovementSpeed();
                m_Motion += Physics.gravity / Time.fixedDeltaTime;

                if (jump) 
                {
                    isJumping = true;
                    m_Motion.y = 0;
                }
            }
            else
            {
                m_Motion += Physics.gravity * Time.fixedDeltaTime;
            }

            var jumpMotion = isJumping ? GetJumpMotion() : Vector3.zero;
            m_CharacterController.Move((m_Motion + jumpMotion) * Time.fixedDeltaTime);
            
            isGrounded = m_CharacterController.isGrounded;
            if (isGrounded)
            {
                isJumping = false;
            }
            else
            {
                isCrouching = false;
                isSprinting = false;
            }

            UpdateCrouch();
        }

        protected virtual void UpdateCrouch()
        {
            if (isCrouching)
            {
                m_CharacterController.height = Mathf.Lerp(
                    m_CharacterController.height, 
                    m_PhysicsSettings.crouchHeight, 
                    Time.fixedDeltaTime * m_PhysicsSettings.crouchHeightLerpSpeed);
            }
            else
            {
                m_CharacterController.height = Mathf.Lerp(
                    m_CharacterController.height, 
                    m_PhysicsSettings.standingHeight, 
                    Time.fixedDeltaTime * m_PhysicsSettings.crouchHeightLerpSpeed);
            }

            m_CharacterController.center = new Vector3(0, m_CharacterController.height / 2f, 0);
        }

        /// <summary>
        /// Gets the motion, that is added onto the standard input motion, while jumping.
        /// </summary>
        protected virtual Vector3 GetJumpMotion()
        {
            return Vector3.up * m_PhysicsSettings.jumpForce * Time.fixedDeltaTime;
        }

        /// <summary>
        /// Gets the desired movement speed used by the character given their movement state.
        /// </summary>
        protected virtual float GetDesiredMovementSpeed()
        {
            return 
                isSprinting ? m_MovementSettings.sprintSpeed : 
                isCrouching ? m_MovementSettings.crouchSpeed : 
                m_MovementSettings.walkSpeed;
        }
    }
}