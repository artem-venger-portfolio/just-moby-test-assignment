using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace BlockTower
{
    [UsedImplicitly]
    public class PlacementAnimator : IPlacementAnimator
    {
        public Tween StartAnimation(RectTransform transform, Vector3 targetPositoin)
        {
            var sequence = DOTween.Sequence();

            var currentPosition = transform.position;
            var spinPosition = new Vector3(targetPositoin.x, currentPosition.y, targetPositoin.z);
            const float move_to_spin_position_speed = 1f;
            var moveToSpinPositionDuration = Vector3.Distance(currentPosition, spinPosition) /
                                             move_to_spin_position_speed;
            var moveToSpinPositionAnimation = transform.DOMove(spinPosition, moveToSpinPositionDuration)
                                                       .SetEase(Ease.Linear);
            sequence.Append(moveToSpinPositionAnimation);

            const float placement_duration = 1f;
            var moveToTargetPositionAnimation = transform.DOMove(targetPositoin, placement_duration)
                                                         .SetEase(Ease.InBack);
            sequence.Append(moveToTargetPositionAnimation);

            var spinAngle = new Vector3(x: 0, y: 0, z: 360);
            const float spin_relative_duration = 0.7f;
            var spinDuration = placement_duration * spin_relative_duration;
            var spinAnimation = transform.DORotate(spinAngle, spinDuration, RotateMode.FastBeyond360)
                                         .SetEase(Ease.OutCubic);
            sequence.Join(spinAnimation);

            return sequence;
        }
    }
}