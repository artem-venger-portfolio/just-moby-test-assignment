using UnityEngine;

namespace BlockTower.Building
{
    public class BuildingBlockPlacer : IBuildingBlockPlacer
    {
        private readonly ITower _tower;

        public BuildingBlockPlacer(ITower tower)
        {
            _tower = tower;
        }

        public void Place(BlockBase placingBlock)
        {
            if (_tower.IsEmpty() == false)
            {
                placingBlock.Transform.position = GetPositionAboveLastBlock(placingBlock);
            }

            _tower.Add(placingBlock);
        }

        private Vector3 GetPositionAboveLastBlock(BlockBase placingBlock)
        {
            var lastBlock = _tower.GetLastBlock();
            var lastBlockTransform = lastBlock.Transform;
            var lastBlockPosition = lastBlockTransform.position;
            var lastBlockDistanceToTop = lastBlockTransform.rect.yMax;
            var lastBlockTopY = lastBlockPosition.y + lastBlockDistanceToTop;

            var placingBlockTransform = placingBlock.Transform;
            var placingBlockPosition = placingBlockTransform.position;
            var placingBlockDistanceToBottom = placingBlockTransform.rect.xMin;

            var targetX = placingBlockPosition.x;
            var targetY = lastBlockTopY - placingBlockDistanceToBottom;
            var targetZ = placingBlockPosition.z;
            var targetPosition = new Vector3(targetX, targetY, targetZ);

            return targetPosition;
        }
    }
}