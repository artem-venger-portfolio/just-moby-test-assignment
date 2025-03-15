using UnityEngine;

namespace BlockTower.Building
{
    public class WithinScreenCondition : ICondition
    {
        public bool Check(BlockBase block)
        {
            var corners = block.GetWorldCorners();
            var bottomLeft = corners[0];
            var topRight = corners[2];

            const int minX = 0;
            var maxX = Screen.width;
            const int minY = 0;
            var maxY = Screen.height;

            return bottomLeft.x >= minX &&
                   bottomLeft.y >= minY &&
                   topRight.x <= maxX &&
                   topRight.y <= maxY;
        }
    }
}