using System;
using JetBrains.Annotations;
using R3;

namespace BlockTower.Tower.Builder
{
    [UsedImplicitly]
    public class TowerBuilder : ITowerBuilder
    {
        private readonly ScrollBase _scroll;
        private readonly IProjectLogger _logger;
        private readonly DropZone _towerDropZone;
        private IDisposable _scrollSubscription;

        public TowerBuilder(ScrollBase scroll, IProjectLogger logger, DropZone towerDropZone)
        {
            _scroll = scroll;
            _logger = logger;
            _towerDropZone = towerDropZone;
        }

        public void Start()
        {
            _scrollSubscription = _scroll.BlockDropped.Subscribe(BlockDroppedEventHandler);
        }

        public void Stop()
        {
            _scrollSubscription.Dispose();
            _scrollSubscription = null;
        }

        private void BlockDroppedEventHandler(DropData dropData)
        {
            LogInfo(nameof(BlockDroppedEventHandler));
        }

        private void LogInfo(string message)
        {
            _logger.LogInfo(message, nameof(TowerBuilder));
        }
    }
}