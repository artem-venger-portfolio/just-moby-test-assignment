using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BlockTower
{
    public class DestructionAnimator : IDestructionAnimator
    {
        private readonly Func<RectTransform, Image, Tween>[] _animations;

        public DestructionAnimator()
        {
            _animations = new Func<RectTransform, Image, Tween>[]
            {
                ScaleAndFadeDestruction,
                MoveOffScreenDestruction,
                ExplodeDestruction,
            };
        }

        public Tween StartAnimation(RectTransform transform, Image image)
        {
            var randomAnimation = _animations.GetRandom();
            return randomAnimation(transform, image);
        }

        private Tween ScaleAndFadeDestruction(RectTransform transform, Image image)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.zero, duration: 1f).SetEase(Ease.InQuad));
            sequence.Join(image.DOFade(endValue: 0f, duration: 1f).SetEase(Ease.InQuad));
            return sequence;
        }

        private Tween MoveOffScreenDestruction(RectTransform transform, Image image)
        {
            return transform.DOMoveX(endValue: -1000f, duration: 1f).SetEase(Ease.InOutCubic);
        }

        private Tween ExplodeDestruction(RectTransform transform, Image image)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.zero, duration: 0.5f).SetEase(Ease.InQuad));
            sequence.Join(image.DOFade(endValue: 0f, duration: 0.5f).SetEase(Ease.InQuad));
            sequence.Join(transform
                         .DOMove(new Vector3(Random.Range(minInclusive: -200, maxExclusive: 200), Random.Range(minInclusive: -200, maxExclusive: 200), z: 0),
                                 duration: 0.5f)
                         .SetEase(Ease.OutBounce));
            return sequence;
        }
    }
}