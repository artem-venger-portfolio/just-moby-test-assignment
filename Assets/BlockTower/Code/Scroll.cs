using UnityEngine;
using UnityEngine.EventSystems;

namespace BlockTower
{
    public class Scroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Block _draggingBlock;
        private bool _isDragging;

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
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            _draggingBlock.ResetColor();

            _isDragging = false;
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

        private void LogInfo(string message)
        {
            Debug.Log($"[{name}] {message}");
        }
    }
}