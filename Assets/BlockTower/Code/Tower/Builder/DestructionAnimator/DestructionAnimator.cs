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
                MoveToNewPosition,
                SubtleDestruction,
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

        private Tween MoveToNewPosition(RectTransform transform, Image image)
        {
            var randomX = Random.Range(-Screen.width / 2f, Screen.width / 2f);
            var randomY = Random.Range(-Screen.height / 2f, Screen.height / 2f);
            return transform.DOMove(new Vector3(randomX, randomY, z: 0), duration: 1f).SetEase(Ease.InOutSine);
        }

        private Tween SubtleDestruction(RectTransform transform, Image image)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.zero, duration: 0.8f).SetEase(Ease.InQuad));
            sequence.Join(image.DOFade(endValue: 0f, duration: 0.8f).SetEase(Ease.InQuad));
            sequence.Join(transform.DORotate(new Vector3(x: 0, y: 0, Random.Range(minInclusive: -45f, maxInclusive: 45f)),
                                             duration: 0.8f, RotateMode.FastBeyond360));
            return sequence;
        }
    }
}