using UnityEngine;

namespace BlockTower
{
    public readonly struct DropData
    {
        public DropData(Vector2 screenPoint, Color color, Vector3[] worldCorners)
        {
            ScreenPoint = screenPoint;
            Color = color;
            WorldCorners = worldCorners;
        }

        public readonly Vector3[] WorldCorners;
        public readonly Vector2 ScreenPoint;
        public readonly Color Color;
    }
}