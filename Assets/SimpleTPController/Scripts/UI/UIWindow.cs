using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using System;

namespace ThirdPersonController.UI
{
    /// <summary>
    /// A simple window system that enables/disables a group of UI elements.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] private bool m_CloseOnStart = true;

        private UIWindow[] m_ChildWindows;
        private Image m_Image;

        public UnityEvent opened;
        public UnityEvent closed;

        /// <summary>
        /// This event is globally executed when a window has opened.
        /// </summary>
        public static event Action<UIWindow> windowOpened;

        /// <summary>
        /// This event is globally executed when a window has closed.
        /// </summary>
        public static event Action<UIWindow> windowClosed;

        /// <summary>
        /// True if this window is open.
        /// </summary>
        public bool isOpen { get; private set; } = true;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_ChildWindows = GetComponentsInChildren<UIWindow>()
                .Where(x => x != this)
                .ToArray();
                
            m_Image = GetComponent<Image>();
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            if (m_CloseOnStart)
            {
                Close();
            }
        }

        /// <summary>
        /// Toggles the <see cref="isOpen" /> value.
        /// </summary>
        public virtual void Toggle()
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        /// <summary>
        /// Opens this UI window.
        /// </summary>
        public virtual void Open()
        {
            if (isOpen)
            {
                return;
            }

            if (m_Image != null)
            {
                m_Image.enabled = true;
            }
            
            foreach (var childWindow in m_ChildWindows)
            {
                childWindow.Open();
            }

            foreach (Transform t in this.transform)
            {
                if (t != this.transform)
                {
                    t.gameObject.SetActive(true);
                }
            }

            opened?.Invoke();
            windowOpened?.Invoke(this);

            isOpen = true;
        }

        /// <summary>
        /// Closes this UI window.
        /// </summary>
        public virtual void Close()
        {
            if (!isOpen)
            {
                return;
            }

            if (m_Image != null)
            {
                m_Image.enabled = false;
            }

            foreach (var childWindow in m_ChildWindows)
            {
                childWindow.Close();
            }

            foreach (Transform t in this.transform)
            {
                if (t != this.transform)
                {
                    t.gameObject.SetActive(false);
                }
            }

            closed?.Invoke();
            windowClosed?.Invoke(this);

            isOpen = false;
        }
    }
}