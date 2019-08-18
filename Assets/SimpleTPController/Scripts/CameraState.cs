using UnityEngine;

namespace ThirdPersonController
{
    [System.Serializable]
    public class CameraState
    {
        public CameraState() {}
        public CameraState(string name)
        {
            m_Name = name;
        }

        [SerializeField] private string m_Name = "New_State";
        public string name
        {
            get { return m_Name; }
        }
        
        [SerializeField] private Vector3 m_CameraOffset = new Vector3(1.5f, 1f, -2f);
        public Vector3 cameraOffset
        {
            get { return m_CameraOffset; }
        }

        [Range(0.1f, 10f)]
        [SerializeField] private float m_FieldOfViewMultiplier = 1f;
        public float FieldOfViewMultiplier
        {
            get { return m_FieldOfViewMultiplier; }
        }

        [Range(0.1f, 10f)]
        [SerializeField] private float m_SensitivityMultiplier = 1f;
        public float sensitivityMultiplier
        {
            get { return m_SensitivityMultiplier; }
        }

        [SerializeField] private float m_MinimumXAngle = -70;
        public float minimumXAngle
        {
            get { return m_MinimumXAngle; }
        }
        
        [SerializeField] private float m_MaximumXAngle = 70;
        public float maximumXAngle
        {
            get { return m_MaximumXAngle; }
        }
        
        [Range(1, 50)]
        [SerializeField] private float m_CameraPositionLerpRate = 10f;
        public float cameraPositionLerpRate
        {
            get { return m_CameraPositionLerpRate; }
        }

        [Range(1, 50)]
        [SerializeField] private float m_CameraFovLerpRate = 10f;
        public float cameraFovLerpRate
        {
            get { return m_CameraFovLerpRate; }
        }
    }
}