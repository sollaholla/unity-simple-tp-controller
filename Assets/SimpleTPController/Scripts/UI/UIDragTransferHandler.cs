using UnityEngine;
using UnityEngine.EventSystems;

namespace ThirdPersonController.UI
{
    /// <summary>
    /// Allows transfering draggable data from one source to another.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIDragTransferHandler<T> : MonoBehaviour, 
        IDragHandler, 
        IBeginDragHandler, 
        IEndDragHandler, 
        IDragTransferReceiver<T>
    {
        [SerializeField] private Vector2 m_DraggableSize = new Vector2(50, 50);

        private UIDragItem<T> m_DragData;

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDrag())
            {
                return;
            }

            m_DragData = new UIDragItem<T>(GetDragableData());
            m_DragData.Spawn(gameObject, GetComponentInParent<Canvas>(), m_DraggableSize);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (m_DragData == null)
            {
                return;
            }

            m_DragData.gameObject.transform.position = eventData.position;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (m_DragData == null)
            {
                return;
            }

            m_DragData.Destroy();

            foreach (var hovered in eventData.hovered)
            {
                var dragReceiver = hovered.GetComponent<IDragTransferReceiver<T>>();
                if (dragReceiver != null)
                {
                    dragReceiver.OnReceiveDrag(this, m_DragData.data);
                    break;
                }
            }

            m_DragData = null;
        }

        public virtual void OnReceiveDrag(UIDragTransferHandler<T> sender, T data) { }

        public abstract T GetDragableData();

        public abstract bool CanDrag();
    }
}