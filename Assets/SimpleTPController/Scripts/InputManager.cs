using System;
using ThirdPersonController.UI;
using UnityEngine;

namespace ThirdPersonController
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager m_Instance;
        private int m_OpenedWindows;
        private bool m_InputBlocked;

        public static InputManager instance {
            get {
                if (m_Instance == null)
                {
                    m_Instance = 
                        FindObjectOfType<InputManager>() ?? 
                        new GameObject("InputManager").AddComponent<InputManager>();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
            }
            else if (m_Instance != this)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            LockCursor();
        }

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable()
        {
            UIWindow.windowOpened += WindowOpened;
            UIWindow.windowClosed += WindowClosed;
        }

        protected virtual void WindowClosed(UIWindow window)
        {
            m_OpenedWindows = Mathf.Max(0, m_OpenedWindows - 1);
            if (m_OpenedWindows == 0)
            {
                m_InputBlocked = false;
                LockCursor();
            }
        }

        protected virtual void WindowOpened(UIWindow window)
        {
            m_OpenedWindows++;
            m_InputBlocked = true;
            UnlockCursor();
        }

        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public static bool GetButtonDown(string buttonName)
        {
            if (InputManager.instance.m_InputBlocked)
            {
                return false;
            }

            return Input.GetButtonDown(buttonName);
        }

        public static bool GetButton(string buttonName)
        {
            if (InputManager.instance.m_InputBlocked)
            {
                return false;
            }

            return Input.GetButton(buttonName);
        }

        public static float GetAxis(string axisName)
        {
            if (InputManager.instance.m_InputBlocked)
            {
                return 0f;
            }

            return Input.GetAxis(axisName);
        }

        public static float GetAxisRaw(string axisName)
        {
            if (InputManager.instance.m_InputBlocked)
            {
                return 0f;
            }

            return Input.GetAxisRaw(axisName);
        }
    }
}