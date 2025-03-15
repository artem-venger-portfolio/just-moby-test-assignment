namespace BlockTower.Building
{
    public class AboveLastBlockCondition : ICondition
    {
        private readonly ITower _tower;

        public AboveLastBlockCondition(ITower tower)
        {
            _tower = tower;
        }

        public bool Check(BlockBase block)
        {
            var result = _tower.IsEmpty();
            if (result == false)
            {
                result = IsAboveLastBlock(block);
            }

            return result;
        }

        private bool IsAboveLastBlock(BlockBase checkingBlock)
        {
            var lastBlock = _tower.GetLastBlock();
            var checkingBlockCorners = checkingBlock.GetWorldCorners();
            var lastBlockCorners = lastBlock.GetWorldCorners();

            var checkingBlockBottomLeft = checkingBlockCorners[0];
            var checkingBlockBottomRight = checkingBlockCorners[3];
            var lastBlockTopLeft = lastBlockCorners[0];
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