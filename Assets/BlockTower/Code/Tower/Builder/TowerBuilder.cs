using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using JetBrains.Annotations;
using R3;
using UnityEngine;

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
        private readonly Canvas _canvas;
        private readonly IActionEventBus _bus;
        private readonly IPlacementAnimator _placementAnimator;
        private IDisposable _eventSubscription;

        public TowerBuilder(ScrollBase scroll, IProjectLogger logger, DropZone towerDropZone,
                            TowerBlockFactory blockFactory, List<IBuildCondition> conditions, ITower tower,
                            IDestructionAnimator destructionAnimator, Canvas canvas, IActionEventBus bus,
                            IPlacementAnimator placementAnimator)
        {
            _scroll = scroll;
            _logger = logger;
            _towerDropZone = towerDropZone;
            _blockFactory = blockFactory;
            _conditions = conditions;
            _tower = tower;
            _destructionAnimator = destructionAnimator;
            _canvas = canvas;
            _bus = bus;
            _placementAnimator = placementAnimator;
        }

        public void Start()
        {
            _eventSubscription = _scroll.BlockDropped.Subscribe(data =>
            {
                if (CanPlaceBlock(data))
                {
                    FireAction(ActionEvent.BlockDroppedInAppropriatePlace);
                    PlaceBlock(data);
                }
                else
                {
                    FireAction(ActionEvent.BlockDroppedInInappropriatePlace);
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
            StartPlacementAnimation(data, block);
            _tower.Add(block);
        }

        private void DestroyBlock(DropData data)
        {
            LogInfo(nameof(DestroyBlock));
            var block = CreateTowerBlock(data);
            block.Transform.position = data.ScreenPoint;
            _destructionAnimator.StartAnimation(block.Transform, block.Image)
                                .OnComplete(() => block.DestroySelf());
        }

        private TowerBlockBase CreateTowerBlock(DropData data)
        {
            var block = _blockFactory.Create();
            block.Color = data.Color;
            return block;
        }

        private void StartPlacementAnimation(DropData data, TowerBlockBase block)
        {
            var screenPoint = data.ScreenPoint;
            var targetPosition = _tower.IsEmpty()
                    ? (Vector3)screenPoint
                    : GetPositionAboveLastBlock(block, screenPoint);
            _placementAnimator.StartAnimation(block.Transform, targetPosition);
        }

        private Vector3 GetPositionAboveLastBlock(TowerBlockBase placingBlock, Vector2 screenPoint)
        {
            var placingBlockDistanceToBottom = placingBlock.Transform.rect.xMin * _canvas.scaleFactor;

            var targetX = screenPoint.x;
            var targetY = _tower.TopY - placingBlockDistanceToBottom;
            const int target_z = 0;
            var targetPosition = new Vector3(targetX, targetY, target_z);

            return targetPosition;
        }

        private void LogInfo(string message)
        {
            _logger.LogInfo(message, nameof(TowerBuilder));
        }

        private void FireAction(ActionEvent actionEvent)
        {
            _bus.Fire(actionEvent);
        }
    }
}