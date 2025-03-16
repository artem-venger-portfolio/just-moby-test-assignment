using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    public interface IDestructionAnimator
    {
        Tween StartAnimation(RectTransform transform, Image image);
    }
}