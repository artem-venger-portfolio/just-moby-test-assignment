using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace BlockTower
{
    public class TowerBlock : TowerBlockBase, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private Image _image;

        private Transform _draggingObjectContainer;
        private Transform _beginDragParent;
        private Vector3 _beginDragPosition;

        public override Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public override Image Image => _image;

        public override RectTransform Transform => (RectTransform)transform;

        public override Vector3[] GetWorldCorners()
        {
            return Transform.GetWorldCorners();
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

        [Inject]
        private void InjectDependencies(Transform draggingObjectContainer)
        {
            _draggingObjectContainer = draggingObjectContainer;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _beginDragParent = Parent;
            Parent = _draggingObjectContainer;
            _beginDragPosition = Position;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Position = eventData.position;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Parent = _beginDragParent;
            Position = _beginDragPosition;
        }
    }
}