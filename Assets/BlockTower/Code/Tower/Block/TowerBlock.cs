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

        private readonly Subject<TowerBlockBase> _droppedStream = new();
        private Transform _draggingObjectContainer;
        private Transform _beginDragParent;
        private Vector3 _beginDragPosition;
        private IActionEventBus _bus;
        private Canvas _canvas;

        public override Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public override Image Image => _image;

        public override RectTransform Transform => (RectTransform)transform;

        public override Observable<TowerBlockBase> DroppedStream => _droppedStream;

        public override Vector3[] GetWorldCorners()
        {
            return Transform.GetWorldCorners();
        }

        public override float GetHeight()
        {
            return Transform.rect.height * _canvas.scaleFactor;
        }

        public override void ReturnToBeginDragPosition()
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

        [Inject]
        private void InjectDependencies(Transform draggingObjectContainer, Canvas canvas,
                                        IActionEventBus bus)
        {
            _draggingObjectContainer = draggingObjectContainer;
            _canvas = canvas;
            _bus = bus;
        }

        private void OnDestroy()
        {
            FireAction(ActionEvent.TowerBlockDestroyed);
            _droppedStream.Dispose();
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
            _droppedStream.OnNext(this);
        }

        private void FireAction(ActionEvent actionEvent)
        {
            _bus.Fire(actionEvent);
        }
    }
}