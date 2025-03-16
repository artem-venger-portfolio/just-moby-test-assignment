using JetBrains.Annotations;
using UnityEngine;

namespace BlockTower
{
    [UsedImplicitly]
    public class UIUtility
    {
        private readonly Canvas _canvas;

        public UIUtility(Canvas canvas)
        {
            _canvas = canvas;
        }

        public bool RectangleContainsScreenPoint(RectTransform transform, Vector2 screenPoint)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(transform, screenPoint, _canvas.worldCamera);
        }
    }
}