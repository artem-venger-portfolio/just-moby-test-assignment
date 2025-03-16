using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        private readonly List<IBuildCondition> _conditions;
        private readonly ITower _tower;
        private readonly IDestructionAnimator _destructionAnimator;
        private IDisposable _eventSubscription;

        public TowerBuilder(ScrollBase scroll, IProjectLogger logger, DropZone towerDropZone,
                            TowerBlockFactory blockFactory, List<IBuildCondition> conditions, ITower tower,
                            IDestructionAnimator destructionAnimator)
        {
            _scroll = scroll;
            _logger = logger;
            _towerDropZone = towerDropZone;
            _blockFactory = blockFactory;
            _conditions = conditions;
            _tower = tower;
            _destructionAnimator = destructionAnimator;
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
            var isInZone = _towerDropZone.IsInZone(data.ScreenPoint);
            if (isInZone == false)
            {
                return false;
            }

            var conditionData = new BuildConditionData(data.WorldCorners);
            var allConditionsMet = _conditions.All(c => c.CanBuild(conditionData));

            return allConditionsMet;
        }

        private void PlaceBlock(DropData data)
        {
            LogInfo(nameof(PlaceBlock));
            var block = CreateTowerBlock(data);
            _tower.Add(block);
        }

        private void DestroyBlock(DropData data)
        {
            LogInfo(nameof(DestroyBlock));
            var block = CreateTowerBlock(data);
            _destructionAnimator.StartAnimation(block.Transform, block.Image)
                                .OnComplete(() => block.DestroySelf());
        }

        private TowerBlockBase CreateTowerBlock(DropData data)
        {
            var block = _blockFactory.Create();
            block.transform.position = data.ScreenPoint;
            block.Color = data.Color;
            return block;
        }

        private void LogInfo(string message)
        {
            _logger.LogInfo(message, nameof(TowerBuilder));
        }
    }
}