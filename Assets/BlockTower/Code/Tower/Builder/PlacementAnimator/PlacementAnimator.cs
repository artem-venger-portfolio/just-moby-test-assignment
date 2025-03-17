using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace BlockTower
{
    [UsedImplicitly]
    public class PlacementAnimator : IPlacementAnimator
    {
        private readonly PlacementAnimationConfig _config;

        public PlacementAnimator(IGameConfig gameConfig)
        {
            _config = gameConfig.PlacementAnimationConfig;
        }

        public Tween StartAnimation(RectTransform transform, Vector3 targetPositoin)
        {
            var sequence = DOTween.Sequence();

            var startPosition = targetPositoin + new Vector3(x: 0, _config.StartHeight, z: 0);
            transform.position = startPosition;

            var duration = _config.Duration;
            var spinAngle = new Vector3(x: 0, y: 0, z: 360);
            var spinDuration = duration * _config.SpinRelativeDuration;
            var spinAnimation = transform.DORotate(spinAngle, spinDuration, RotateMode.FastBeyond360)
                                         .SetEase(Ease.OutCubic);
            sequence.Append(spinAnimation);

            var moveUpDuration = duration * _config.MoveUpRelativeDuration;
            var moveUpAnimation = transform.DOMoveY(_config.MoveUpDistance, moveUpDuration)
                                           .SetRelative()
                                           .SetEase(Ease.OutQuart);
            sequence.Join(moveUpAnimation);

            var moveDownDuration = duration - moveUpDuration;
            var moveToTargetPositionAnimation = transform.DOMove(targetPositoin, moveDownDuration)
                                                         .SetEase(Ease.InQuart);
            sequence.Append(moveToTargetPositionAnimation);

            return sequence;
        }
    }
}