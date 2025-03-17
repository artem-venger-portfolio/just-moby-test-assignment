using R3;
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

        private readonly Subject<TowerBlockBase> _droppedInHole = new();
        private Transform _draggingObjectContainer;
        private Transform _beginDragParent;
        private Vector3 _beginDragPosition;
        private DropZone _holeDropZone;

        public override Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public override Image Image => _image;

        public override RectTransform Transform => (RectTransform)transform;

        public override Observable<TowerBlockBase> DroppedInHole => _droppedInHole;

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
        private void InjectDependencies(Transform draggingObjectContainer, DropZone holeDropZone)
        {
            _draggingObjectContainer = draggingObjectContainer;
            _holeDropZone = holeDropZone;
        }

        private void OnDestroy()
        {
            _droppedInHole.Dispose();
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
            if (_holeDropZone.IsInZone(eventData.position))
            {
                _droppedInHole.OnNext(this);
                _droppedInHole.OnCompleted();
            }
            else
            {
                Parent = _beginDragParent;
                Position = _beginDragPosition;
            }
        }
    }
}