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
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable()
        {
            UIWindow.windowOpened += WindowOpened;
            UIWindow.windowClosed += WindowClosed;
        }

        protected virtual void WindowClosed(UIWindow window)
        {
            m_OpenedWindows--;
            if (m_OpenedWindows == 0)
            {
                m_InputBlocked = false;
            }
        }

        protected virtual void WindowOpened(UIWindow window)
        {
            m_OpenedWindows++;
            m_InputBlocked = true;
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