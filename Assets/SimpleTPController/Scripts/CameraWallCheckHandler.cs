using UnityEngine;

namespace ThirdPersonController
{
    /// <summary>
    /// Provides the ability to use wall protection for free look camera rig setups.
    /// </summary>
    public abstract class CameraWallCheckHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask m_WallCheckLayers = Physics.DefaultRaycastLayers;
        [SerializeField] private float m_WallCheckRadius = 0.1f;

        /// <summary>
        /// The desired camera offset.
        /// </summary>
        public abstract Vector3 desiredCameraOffset { get; }

        /// <summary>
        /// The camera's pivot transform.
        /// </summary>
        public abstract Transform pivot { get; }

        private bool m_Overriding;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            var direction = pivot.TransformPoint(desiredCameraOffset) - pivot.position;
            if (Physics.SphereCast(pivot.position, m_WallCheckRadius, direction.normalized, out var hit, 
                direction.magnitude, m_WallCheckLayers))
            {
                var hitOffset = direction.normalized * (hit.distance - m_WallCheckRadius * 2);
                var offset = pivot.InverseTransformPoint(pivot.position + hitOffset);
                SetCameraOffset(offset);
                m_Overriding = true;
            }
            else
            {
                if (m_Overriding)
                {
                    ResetCameraOffset();
                    m_Overriding = false;
                }
            }
        }

        protected abstract void ResetCameraOffset();
        protected abstract void SetCameraOffset(Vector3 offset);
    }
}