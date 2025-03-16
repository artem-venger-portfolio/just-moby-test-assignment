using UnityEngine;

namespace BlockTower
{
    public class DraggableObject : MonoBehaviour
    {
        private Transform _draggingObjectContainer;
        private Transform _beginDragParent;
        private Vector3 _beginDragPosition;

        public void SetDraggingObjectContainer(Transform container)
        {
            _draggingObjectContainer = container;
        }

        public void OnBeginDrag()
        {
            _beginDragParent = Parent;
            Parent = _draggingObjectContainer;
            _beginDragPosition = Position;
        }

        public void OnDrag(Vector2 position)
        {
            Position = position;
        }

        public void OnEndDrag()
        {
            Parent = _beginDragParent;
            Position = _beginDragPosition;
        }

        private Transform Parent
        {
            get => transform.parent;
            set => transform.SetParent(value, worldPositionStays: true);
        }

        private Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}