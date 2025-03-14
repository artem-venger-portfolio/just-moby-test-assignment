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
        private Transform _beginDragParent;
        private Block _draggingBlock;
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
            var block = raycastTarget.GetComponentInParent<Block>();
            if (block == null)
            {
                return;
            }

            _draggingBlock = block;
            _beginDragParent = _draggingBlock.transform.parent;
            SetDraggingBlockParent(_draggingObjectContainer);
            _draggingBlock.SetDraggingColor();
            SetIsDraggingAndChangeScrollActivity(isDragging: true);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            SetDraggingBlockParent(_beginDragParent);
            SetDraggingBlockPosition(eventData.pressPosition);
            _draggingBlock.ResetColor();
            _draggingBlock = null;

            SetIsDraggingAndChangeScrollActivity(isDragging: false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            SetDraggingBlockPosition(eventData.position);
        }

        private void SetIsDraggingAndChangeScrollActivity(bool isDragging)
        {
            _isDragging = isDragging;
            _scrollRect.horizontal = !_isDragging;
        }

        private void SetDraggingBlockPosition(Vector2 position)
        {
            _draggingBlock.transform.position = position;
        }

        private void SetDraggingBlockParent(Transform parent)
        {
            _draggingBlock.transform.SetParent(parent, true);
        }
    }
}