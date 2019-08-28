using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ThirdPersonController.UI
{
    public class UIDragItem<T>
    {
        public UIDragItem(T data)
        {
            this.data = data;
        }

        public T data { get; }
        public GameObject gameObject { get; private set; }
        
        public void Spawn(GameObject template, Canvas canvas, Vector2 size)
        {
            gameObject = Object.Instantiate(template, canvas.transform);
            
            var rt = gameObject.GetComponent<RectTransform>();
            rt.sizeDelta = size;

            var behaviours = gameObject.GetComponentsInChildren<MonoBehaviour>()
                .Where(x => !(x is Graphic));

            var graphics = gameObject.GetComponentsInChildren<Graphic>();
            foreach (var graphic in graphics)
            {
                graphic.raycastTarget = false;
            }

            foreach (var behaviour in behaviours)
            {
                behaviour.enabled = false;
            }
        }

        public void Destroy()
        {
            Object.Destroy(gameObject);
        }
    }
}