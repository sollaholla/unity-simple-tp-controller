using System;
using System.Linq;
using UnityEngine;

namespace ThirdPersonController
{
    public enum ThirdPersonCameraFollowMode
    {
        Update,
        FixedUpdate,
        LateUpdate,
        ManualUpdate
    }

    public class ThirdPersonCameraController : MonoBehaviour
    {
        [SerializeField] private Transform m_Target = null;
        [SerializeField] private Camera m_Camera = null;
        [SerializeField] private Transform m_Pivot = null;
        [SerializeField] private ThirdPersonCameraFollowMode m_FollowMode = ThirdPersonCameraFollowMode.FixedUpdate;
        [SerializeField] private float m_FieldOfView = 60;
        [SerializeField] private CameraState m_DefaultCameraState = new CameraState("Default");
        [Obsolete("Do not use this field to itterate camera states please use m_RuntimeStates instead.")]
        [SerializeField] private CameraState[] m_CameraStates = new CameraState[] { new CameraState() };

        public float xRotation { get; private set; }
        public float yRotation { get; private set; }
        public CameraState currentState { get; private set; }

        private CameraState[] m_RuntimeStates;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            currentState = m_DefaultCameraState;

            #pragma warning disable 612,618
            m_RuntimeStates = m_CameraStates.Concat(new [] { m_DefaultCameraState }).ToArray();
            #pragma warning restore 612,618
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            UpdateCamera();

            if (m_FollowMode != ThirdPersonCameraFollowMode.Update)
            {
                return;
            }

            DoUpdate();
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            if (m_FollowMode != ThirdPersonCameraFollowMode.FixedUpdate)
            {
                return;
            }

            DoUpdate();
        }

        /// <summary>
        /// LateUpdate is called every frame, if the Behaviour is enabled.
        /// It is called after all Update functions have been called.
        /// </summary>
        protected virtual void LateUpdate()
        {
            if (m_FollowMode != ThirdPersonCameraFollowMode.LateUpdate)
            {
                return;
            }

            DoUpdate();
        }

        public virtual void DoUpdate()
        {
            FollowTarget();
        }

        protected virtual void UpdateCamera()
        {
            m_Camera.transform.localPosition = Vector3.Lerp(
                m_Camera.transform.localPosition, 
                currentState.cameraOffset, 
                currentState.cameraPositionLerpRate * Time.deltaTime);

            m_Camera.fieldOfView = Mathf.Lerp(
                m_Camera.fieldOfView,
                m_FieldOfView * currentState.FieldOfViewMultiplier,
                currentState.cameraFovLerpRate * Time.deltaTime);
        }

        protected virtual void FollowTarget()
        {
            if (m_Target == null)
            {
                return;
            }

            transform.position = m_Target.position;
        }

        /// <summary>
        /// Sets the current camera state to a state with the given name.
        /// </summary>
        /// <param name="stateName">The name of the camera state.</param>
        public virtual void SetState(string stateName)
        {
            if (currentState.name == stateName)
            {
                return;
            }

            var state = m_RuntimeStates.FirstOrDefault(x => x.name == stateName);
            if (state == null)
            {
                return;
            }

            currentState = state;
        }

        /// <summary>
        /// Rotates the pivot given the x and y delta values.
        /// </summary>
        /// <param name="xDelta">The x delta (i.e. mouse x delta)</param>
        /// <param name="yDelta">The y delta (i.e. mouse y delta)</param>
        public virtual void Rotate(float xDelta, float yDelta)
        {
            xRotation += yDelta * currentState.sensitivityMultiplier;
            yRotation += xDelta * currentState.sensitivityMultiplier;
            xRotation = Mathf.Clamp(xRotation, currentState.minimumXAngle, currentState.maximumXAngle);
            m_Pivot.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }

        /// <summary>
        /// Sets the camera to target the given transform.
        /// </summary>
        /// <param name="target">The target transform.</param>
        public virtual void SetTarget(Transform target)
        {
            this.m_Target = target;
        }
    }
}