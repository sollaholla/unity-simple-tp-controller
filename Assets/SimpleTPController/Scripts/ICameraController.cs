using UnityEngine;

namespace ThirdPersonController
{
    public interface ICameraController
    {
        float xRotation { get; }
        float yRotation { get; }
        CameraState currentState { get; }
        Camera cam { get; }

        void ManualUpdate();
        void Rotate(float xDelta, float yDelta);
        void SetTarget(Transform target);
        void Zoom(float zoomLevel);
        void Recoil(float recoilStrengthX, float recoilStrengthY);
    }
}