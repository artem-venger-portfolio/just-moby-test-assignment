using System;
using R3;
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

        private readonly Subject<DropData> _blockDroppedSubject = new();
        private IGameConfig _config;
        private IProjectLogger _logger;
        private ScrollBlock.Factory _blockFactory;
        private ScrollBlock _draggingBlock;
        private bool _isDragging;
        private ScrollBlock[] _blocks;

        public override Observable<DropData> BlockDropped => _blockDroppedSubject;

        public override void CreateBlocks()
        {
            LogInfo(nameof(CreateBlocks));
            var colors = _config.Colors;
            _blocks = new ScrollBlock[colors.Count];
            for (var i = 0; i < colors.Count; i++)
            {
                var currentColor = colors[i];
                var currentBlock = _blockFactory.Create();
                currentBlock.Color = currentColor;
                currentBlock.SetDraggingObjectContainer(_draggingObjectContainer);
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

            var screenPoint = eventData.pressPosition;
            if (HasBlockAtScreenPoint(screenPoint) == false)
            {
                return;
            }

            _draggingBlock = GetBlockAtScreenPoint(screenPoint);
            _draggingBlock.OnBeginDrag();
            SetIsDraggingAndChangeScrollActivity(isDragging: true);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            _draggingBlock.OnDrag(eventData.position);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
            {
                return;
            }

            var dropData = new DropData(eventData.position, _draggingBlock.Color);
            
            _draggingBlock.OnEndDrag();
            _draggingBlock = null;

            SetIsDraggingAndChangeScrollActivity(isDragging: false);
            
            _blockDroppedSubject.OnNext(dropData);
        }

        private bool HasBlockAtScreenPoint(Vector2 screenPoint)
        {
            foreach (var currentBlock in _blocks)
            {
                if (currentBlock.IsAtScreenPoint(screenPoint))
                {
                    return true;
                }
            }

            return false;
        }

        private ScrollBlock GetBlockAtScreenPoint(Vector2 screenPoint)
        {
            foreach (var currentBlock in _blocks)
            {
                if (currentBlock.IsAtScreenPoint(screenPoint))
                {
                    return currentBlock;
                }
            }

            throw new Exception($"Can't find block at screen point {screenPoint.ToString()}");
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

        private void OnDestroy()
        {
            _blockDroppedSubject.Dispose();
        }
    }
}