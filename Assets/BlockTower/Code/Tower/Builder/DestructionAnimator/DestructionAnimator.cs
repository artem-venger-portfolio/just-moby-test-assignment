using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    [UsedImplicitly]
    public class DestructionAnimator : IDestructionAnimator
    {
        private readonly IGameConfig _config;

        public DestructionAnimator(IGameConfig config)
        {
            _config = config;
        }

        public Tween StartAnimation(RectTransform transform, Image image)
        {
            var sequence = DOTween.Sequence();

            var duration = _config.DestructionAnimationDuration;

            var scaleAnimation = transform.DOScale(Vector3.zero, duration)
                                          .SetEase(Ease.OutQuart);
            sequence.Append(scaleAnimation);

            var spinAngle = new Vector3(x: 0, y: 0, z: 360);
            var spinAnimation = transform.DORotate(spinAngle, duration, RotateMode.FastBeyond360)
                                         .SetEase(Ease.OutCubic);
            sequence.Join(spinAnimation);

            var fadeAnimation = image.DOFade(endValue: 0f, duration)
                                     .SetEase(Ease.OutQuart);
            sequence.Join(fadeAnimation);

            return sequence;
        }
    }
}