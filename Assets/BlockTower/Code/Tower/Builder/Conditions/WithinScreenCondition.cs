using UnityEngine;

namespace BlockTower
{
    public class WithinScreenCondition : IBuildCondition
    {
        public bool CanBuild(BuildConditionData data)
        {
            var corners = data.CheckingBlockCorners;
            var bottomLeft = corners[0];
            var topRight = corners[2];

            const int min_x = 0;
            var maxX = Screen.width;
            const int min_y = 0;
            var maxY = Screen.height;

            return bottomLeft.x >= min_x &&
                   bottomLeft.y >= min_y &&
                   topRight.x <= maxX;
        }
    }
}