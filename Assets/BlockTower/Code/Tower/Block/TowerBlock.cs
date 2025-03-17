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
        private IActionEventBus _bus;
        private Canvas _canvas;

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

        public override float GetHeight()
        {
            return Transform.rect.height * _canvas.scaleFactor;
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
        private void InjectDependencies(Transform draggingObjectContainer, DropZone holeDropZone, Canvas canvas,
                                        IActionEventBus bus)
        {
            _draggingObjectContainer = draggingObjectContainer;
            _holeDropZone = holeDropZone;
            _canvas = canvas;
            _bus = bus;
        }

        private void OnDestroy()
        {
            FireAction(ActionEvent.TowerBlockDestroyed);
            _droppedInHole.Dispose();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _beginDragParent = Parent;
            Parent = _draggingObjectContainer;
            _beginDragPosition = Position;
            FireAction(ActionEvent.TowerBlockDragged);
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
                FireAction(ActionEvent.TowerBlockDroppedInHole);
            }
            else
            {
                Parent = _beginDragParent;
                Position = _beginDragPosition;
                FireAction(ActionEvent.TowerBlockDropped);
            }
        }

        private void FireAction(ActionEvent actionEvent)
        {
            _bus.Fire(actionEvent);
        }
    }
}