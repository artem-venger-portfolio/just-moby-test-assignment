using DG.Tweening;
using UnityEngine;

namespace BlockTower
{
    public interface IPlacementAnimator
    {
        Tween StartAnimation(RectTransform transform, Vector3 targetPositoin);
    }
}