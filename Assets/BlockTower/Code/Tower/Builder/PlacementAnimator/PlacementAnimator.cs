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

            var currentPosition = transform.position;
            var spinPosition = new Vector3(targetPositoin.x, currentPosition.y, targetPositoin.z);
            var moveToSpinPositionDuration = Vector3.Distance(currentPosition, spinPosition) /
                                             _config.MoveToSpinPositionSpeed;
            var moveToSpinPositionAnimation = transform.DOMove(spinPosition, moveToSpinPositionDuration)
                                                       .SetEase(Ease.Linear);
            sequence.Append(moveToSpinPositionAnimation);

            var placementDuration = _config.PlacementDuration;
            var moveToTargetPositionAnimation = transform.DOMove(targetPositoin, placementDuration)
                                                         .SetEase(Ease.InBack);
            sequence.Append(moveToTargetPositionAnimation);

            var spinAngle = new Vector3(x: 0, y: 0, z: 360);
            var spinDuration = placementDuration * _config.SpinRelativeDuration;
            var spinAnimation = transform.DORotate(spinAngle, spinDuration, RotateMode.FastBeyond360)
                                         .SetEase(Ease.OutCubic);
            sequence.Join(spinAnimation);

            return sequence;
        }
    }
}