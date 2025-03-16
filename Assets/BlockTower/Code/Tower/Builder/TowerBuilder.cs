using System;
using JetBrains.Annotations;
using R3;

namespace BlockTower
{
    [UsedImplicitly]
    public class TowerBuilder : ITowerBuilder
    {
        private readonly ScrollBase _scroll;
        private readonly IProjectLogger _logger;
        private readonly DropZone _towerDropZone;
        private readonly TowerBlockFactory _blockFactory;
        private IDisposable _eventSubscription;

        public TowerBuilder(ScrollBase scroll, IProjectLogger logger, DropZone towerDropZone,
                            TowerBlockFactory blockFactory)
        {
            _scroll = scroll;
            _logger = logger;
            _towerDropZone = towerDropZone;
            _blockFactory = blockFactory;
        }

        public void Start()
        {
            _eventSubscription = _scroll.BlockDropped.Subscribe(data =>
            {
                if (CanPlaceBlock(data))
                {
                    PlaceBlock(data);
                }
                else
                {
                    DestroyBlock(data);
                }
            });
        }

        public void Stop()
        {
            _eventSubscription.Dispose();
            _eventSubscription = null;
        }

        private bool CanPlaceBlock(DropData data)
        {
            return _towerDropZone.IsInZone(data.ScreenPoint);
        }

        private void PlaceBlock(DropData data)
        {
            LogInfo(nameof(PlaceBlock));
            var block = _blockFactory.Create();
            block.transform.position = data.ScreenPoint;
            block.Color = data.Color;
        }

        private void DestroyBlock(DropData data)
        {
            LogInfo(nameof(DestroyBlock));
        }

        private void LogInfo(string message)
        {
            _logger.LogInfo(message, nameof(TowerBuilder));
        }
    }
}