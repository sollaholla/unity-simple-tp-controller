using UnityEngine;

namespace ThirdPersonController.UI
{
    /// <summary>
    /// Responsible for toggling window state based on user input.
    /// </summary>
    [RequireComponent(typeof(UIWindow))]
    public class UIWindowInput : MonoBehaviour
    {
        [SerializeField] private string m_WindowInputButton = null;

        private UIWindow m_Window;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_Window = GetComponent<UIWindow>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            if (Input.GetButtonDown(m_WindowInputButton))
            {
                m_Window.Toggle();
            }
        }
    }
}
