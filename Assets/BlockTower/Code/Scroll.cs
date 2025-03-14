using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlockTower
{
    [RequireComponent(typeof(ScrollRect))]
    public class Scroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private ScrollRect _scrollRect;
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
            _draggingBlock.SetDraggingColor();
            SetIsDraggingAndChangeScrollActivity(true);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            _draggingBlock.ResetColor();

            SetIsDraggingAndChangeScrollActivity(false);
            _draggingBlock = null;
            LogInfo(message: "Drag end");
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }
        }

        private void SetIsDraggingAndChangeScrollActivity(bool isDragging)
        {
            _isDragging = isDragging;
            _scrollRect.horizontal = !_isDragging;
        }

        private void LogInfo(string message)
        {
            Debug.Log($"[{name}] {message}");
        }
    }
}