using UnityEngine;

namespace BlockTower
{
    public readonly struct DropData
    {
        public readonly Vector2 ScreenPoint;
        public readonly Color Color;

        public DropData(Vector2 screenPoint, Color color)
        {
            ScreenPoint = screenPoint;
            Color = color;
        }
    }
}