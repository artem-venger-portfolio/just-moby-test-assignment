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
        private Tween _placementAnimation;

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

        public bool IsPlacementAnimationActive()
        {
            return _placementAnimation.IsActive();
        }

        public void CompletePlacementAnimation()
        {
            _placementAnimation.Complete();
            _placementAnimation = null;
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

            if (IsPlacementAnimationActive())
            {
                CompletePlacementAnimation();
            }

            var block = CreateTowerBlock(data);
            StartPlacementAnimation(block);
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
            block.Color = data.Color;
            block.Transform.position = data.ScreenPoint;
            return block;
        }

        private void StartPlacementAnimation(TowerBlockBase newBlock)
        {
            var targetPosition = CalculateTargetPositionOfNewBlock(newBlock);
            _placementAnimation = _placementAnimator.StartAnimation(newBlock.Transform, targetPosition);
        }

        private Vector3 CalculateTargetPositionOfNewBlock(TowerBlockBase newBlock)
        {
            var newBlockPosition = newBlock.Transform.position;

            Vector3 targetPosition;
            if (_tower.IsEmpty())
            {
                targetPosition = newBlockPosition;
            }
            else
            {
                var lastBlock = _tower.GetLastBlock();
                var lastBlockTransform = lastBlock.Transform;
                var lastBlockDistanceToTop = lastBlockTransform.rect.yMax * _canvas.scaleFactor;
                var lastBlockTopY = lastBlockTransform.position.y + lastBlockDistanceToTop;

                var placingBlockDistanceToBottom = newBlock.Transform.rect.xMin * _canvas.scaleFactor;
                var targetY = lastBlockTopY - placingBlockDistanceToBottom;
                targetPosition = new Vector3(newBlockPosition.x, targetY, newBlockPosition.z);
            }

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