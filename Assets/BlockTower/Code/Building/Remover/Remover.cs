using BlockTower.Building.Update;
using UnityEngine;

namespace BlockTower.Building
{
    public class Remover : IRemover
    {
        private const int INCORRECT_BLOCK_INDEX = -1;

        private readonly IApplicationEvents _events;
        private readonly ITower _tower;

        public Remover(IApplicationEvents events, ITower tower)
        {
            _events = events;
            _tower = tower;
        }

        public void Start()
        {
            _events.Updated += UpdateEventHandler;
        }

        public void Stop()
        {
            _events.Updated -= UpdateEventHandler;
        }

        private void UpdateEventHandler()
        {
            if (CanRemoveBlock())
            {
                RemoveBlockAndRebuildTower();
            }
        }

        private bool CanRemoveBlock()
        {
            var isRightMouseButtonDown = Input.GetMouseButtonDown(button: 1);
            var hasBlockAtMousePosition = GetBlockIndexAtMousePosition() != INCORRECT_BLOCK_INDEX;

            return isRightMouseButtonDown && hasBlockAtMousePosition;
        }

        private void RemoveBlockAndRebuildTower()
        {
            var blockIndex = GetBlockIndexAtMousePosition();
            var block = _tower[blockIndex];
            var blockHeight = GetHeight(block);
            block.DestroySelf();
            _tower.Remove(block);

            if (_tower.IsEmpty() == false)
            {
                RebuildTower(blockIndex, blockHeight);
            }
        }

        private int GetBlockIndexAtMousePosition()
        {
            for (var i = 0; i < _tower.Count; i++)
            {
                var currentBlock = _tower[i];
                if (IsBlockAtMousePosition(currentBlock))
                {
                    return i;
                }
            }

            return INCORRECT_BLOCK_INDEX;
        }

        private static bool IsBlockAtMousePosition(BlockBase block)
        {
            var mousePosition = Input.mousePosition;
            var isBlockInMousePosition = RectTransformUtility.RectangleContainsScreenPoint(block.Transform,
                     mousePosition, cam: null);

            return isBlockInMousePosition;
        }

        private void RebuildTower(int startIndex, float height)
        {
            for (var i = startIndex; i < _tower.Count; i++)
            {
                var currentBlock = _tower[i];
                var currentBlockTransform = currentBlock.Transform;
                currentBlockTransform.position -= new Vector3(x: 0, height, z: 0);
            }
        }

        private static float GetHeight(BlockBase block)
        {
            return block.Transform.rect.height;
        }
    }
}