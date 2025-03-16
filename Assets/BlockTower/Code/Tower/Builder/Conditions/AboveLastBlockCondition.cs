﻿using UnityEngine;

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
            var lastBlockTopY = lastBlockTopLeft.y;
            var isCheckingBlockBottomHigherThanLastBlockTop = checkingBlockBottomY >= lastBlockTopY;

            var checkingBlockCenterX = (checkingBlockBottomLeft.x + checkingBlockBottomRight.x) / 2;
            var isCheckingBlockCenterWithinLastBlockWidth = checkingBlockCenterX >= lastBlockTopLeft.x &&
                                                            checkingBlockCenterX <= lastBlockTopRight.x;

            return isCheckingBlockBottomHigherThanLastBlockTop && isCheckingBlockCenterWithinLastBlockWidth;
        }
    }
}