using UnityEngine;

namespace BlockTower
{
    public class AboveLastBlockCondition : IBuildCondition
    {
        private readonly ITower _tower;

        public AboveLastBlockCondition(ITower tower)
        {
            _tower = tower;
        }

        public bool CanBuild(BuildConditionData data)
        {
            var result = _tower.IsEmpty();
            if (result == false)
            {
                result = IsAboveLastBlock(data.CheckingBlockCorners);
            }

            return result;
        }

        private bool IsAboveLastBlock(Vector3[] checkingBlockCorners)
        {
            var lastBlock = _tower.GetLastBlock();
            var lastBlockCorners = lastBlock.GetWorldCorners();

            var checkingBlockBottomLeft = checkingBlockCorners[0];
            var checkingBlockBottomRight = checkingBlockCorners[3];
            var lastBlockTopLeft = lastBlockCorners[1];
            var lastBlockTopRight = lastBlockCorners[2];

            var checkingBlockBottomY = checkingBlockBottomLeft.y;
            var lastBlockTopY = _tower.TopY;
            var isCheckingBlockBottomHigherThanLastBlockTop = checkingBlockBottomY >= lastBlockTopY;

            var isCheckingBlockWidthAndLastBlockWidthIntersects = RangesIntersect(checkingBlockBottomLeft.x,
                     checkingBlockBottomRight.x, lastBlockTopLeft.x, lastBlockTopRight.x);

            return isCheckingBlockBottomHigherThanLastBlockTop && isCheckingBlockWidthAndLastBlockWidthIntersects;
        }

        private static bool RangesIntersect(float x1, float x2, float y1, float y2)
        {
            return x1 <= y2 && y1 <= x2;
        }
    }
}