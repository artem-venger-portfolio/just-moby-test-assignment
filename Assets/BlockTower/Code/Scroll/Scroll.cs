using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace BlockTower
{
    public class Scroll : ScrollBase, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private Transform _draggingObjectContainer;

        [SerializeField]
        private ScrollRect _scrollRect;

        private IGameConfig _config;
        private IProjectLogger _logger;
        private ScrollBlock.Factory _blockFactory;
        private DraggableObject _draggingObject;
        private bool _isDragging;
        private ScrollBlock[] _blocks;

        public override void CreateBlocks()
        {
            LogInfo(nameof(CreateBlocks));
            var colors = _config.Colors;
            _blocks = new ScrollBlock[colors.Count];
            for (var i = 0; i < colors.Count; i++)
            {
                var currentColor = colors[i];
                var currentBlock = _blockFactory.Create();
                currentBlock.SetColor(currentColor);
                _blocks[i] = currentBlock;
            }
        }

        [Inject]
        private void InjectDependencies(IGameConfig config, IProjectLogger logger, ScrollBlock.Factory blockFactory)
        {
            _config = config;
            _logger = logger;
            _blockFactory = blockFactory;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
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

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            _draggingObject.OnDrag(eventData.position);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
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

        private void LogInfo(string message)
        {
            _logger.LogInfo(message, nameof(Scroll));
        }
    }
}