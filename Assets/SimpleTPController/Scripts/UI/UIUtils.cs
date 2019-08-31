using UnityEngine;

namespace ThirdPersonController.UI
{
    /// <summary>
    /// Utilities for UI.
    /// </summary>
    public static class UIUtils
    {
        /// <summary>
        /// Keep the given rect transform within screen bounds.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="transform">The rect transform.</param>
        /// <param name="position">The position of the rect transform.</param>
        public static Vector2 KeepWithinScreenBounds(Canvas canvas, RectTransform transform, Vector2 position)
        {
            Vector2 retVal = position;
            Vector2 size = canvas.transform.TransformVector(transform.sizeDelta);
            
            if (retVal.x + size.x > Screen.width) 
            {
                retVal.x -= size.x;
            }

            if (retVal.y + size.y > Screen.height) 
            {
                retVal.y -= size.y;
            }
            
            return retVal;
        }
    }
}