using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlockTower
{
    [RequireComponent(typeof(ScrollRect))]
    public class Scroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private Transform _draggingObjectContainer;

        private ScrollRect _scrollRect;
        private DraggableObject _draggingObject;
        private bool _isDragging;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var isDragMove = eventData.delta.y > 0;
            if (isDragMove == false)
            {
                return;
            }

            var raycastTarget = eventData.pointerPressRaycast.gameObject;
            var draggingObject = raycastTarget.GetComponentInParent<DraggableObject>();
            if (draggingObject == null)
            {
                return;
            }

            _draggingObject = draggingObject;
            _draggingObject.SetDraggingObjectContainer(_draggingObjectContainer);
            _draggingObject.OnBeginDrag();
            SetIsDraggingAndChangeScrollActivity(isDragging: true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            _draggingObject.OnDrag(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            _draggingObject.OnEndDrag();
            _draggingObject = null;

            SetIsDraggingAndChangeScrollActivity(isDragging: false);
        }

        private void SetIsDraggingAndChangeScrollActivity(bool isDragging)
        {
            _isDragging = isDragging;
            _scrollRect.horizontal = !_isDragging;
        }
    }
}