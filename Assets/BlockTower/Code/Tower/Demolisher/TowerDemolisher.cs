using DG.Tweening;
using JetBrains.Annotations;
using R3;

namespace BlockTower
{
    [UsedImplicitly]
    public class TowerDemolisher : ITowerDemolisher
    {
        private readonly CompositeDisposable _compositeDisposable;
        private readonly RebuildAnimationConfig _animationConfig;
        private readonly ITowerBuilder _builder;
        private readonly ITower _tower;
        private Tween _rebuildAnimation;

        public TowerDemolisher(ITower tower, IGameConfig gameConfig, ITowerBuilder builder)
        {
            _compositeDisposable = new CompositeDisposable();
            _tower = tower;
            _builder = builder;
            _animationConfig = gameConfig.RebuildAnimationConfig;
        }

        public void Start()
        {
            _tower.BlockAdded
                  .Subscribe(block => block.DroppedInHole.Subscribe(BlockDroppedInHoleEventHandler))
                  .AddTo(_compositeDisposable);
        }

        public void Stop()
        {
            _compositeDisposable.Clear();
        }

        private void BlockDroppedInHoleEventHandler(TowerBlockBase blockBase)
        {
            CompletePreviousOperationIfNeeded();
            var blockIndex = _tower.IndexOf(blockBase);
            var blockHeight = blockBase.GetHeight();
            RemoveBlock(blockBase);
            if (_tower.IsEmpty() == false)
            {
                RebuildTower(blockIndex, blockHeight);
            }
        }

        private void CompletePreviousOperationIfNeeded()
        {
            if (_rebuildAnimation.IsActive())
            {
                _rebuildAnimation.Complete();
            }
        }

        private void RemoveBlock(TowerBlockBase blockBase)
        {
            _tower.Remove(blockBase);
            blockBase.DestroySelf();
        }

        private void RebuildTower(int startIndex, float height)
        {
            CompletePlacementAnimationIfNeeded();
            _rebuildAnimation = GetRebuildAnimation(startIndex, height);
        }

        private Tween GetRebuildAnimation(int startIndex, float height)
        {
            var sequence = DOTween.Sequence();
            for (var i = startIndex; i < _tower.Count; i++)
            {
                var currentBlock = _tower[i];
                var currentBlockTransform = currentBlock.Transform;

                var moveAnimation = currentBlockTransform.DOMoveY(-height, _animationConfig.Duration)
                                                         .SetRelative()
                                                         .SetEase(Ease.OutQuart);
                var startTime = i * _animationConfig.Interval;
                sequence.Insert(startTime, moveAnimation);
            }

            return sequence;
        }

        private void CompletePlacementAnimationIfNeeded()
        {
            if (_builder.IsPlacementAnimationActive())
            {
                _builder.CompletePlacementAnimation();
            }
        }
    }
}