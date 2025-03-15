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

            const int min_x = 0;
            var maxX = Screen.width;
            const int min_y = 0;
            var maxY = Screen.height;

            return bottomLeft.x >= min_x &&
                   bottomLeft.y >= min_y &&
                   topRight.x <= maxX &&
                   topRight.y <= maxY;
        }
    }
}