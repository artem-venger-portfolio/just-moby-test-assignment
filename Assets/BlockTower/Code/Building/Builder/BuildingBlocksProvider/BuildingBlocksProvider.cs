using System;
using BlockTower.Building.Update;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlockTower.Building
{
    public class BuildingBlocksProvider : IBuildingBlocksProvider
    {
        private readonly IApplicationEvents _events;
        private readonly BlockBase _blockTemplate;
        private readonly Transform _blockContainer;
        private readonly ITower _tower;
        private bool _isMovingBlock;

        public BuildingBlocksProvider(IApplicationEvents events, BlockBase blockTemplate, Transform blockContainer,
                                      ITower tower)
        {
            _events = events;
            _blockTemplate = blockTemplate;
            _blockContainer = blockContainer;
            _tower = tower;
        }

        public event Action<BlockBase> SuitableBlockCreated;

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
            if (_isMovingBlock)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                var currentBlock = Object.Instantiate(_blockTemplate, Input.mousePosition, Quaternion.identity,
                                                      _blockContainer);
                currentBlock.Dropped += CurrentBlockDroppedEventHandler;
                currentBlock.FollowMouse();
                _isMovingBlock = true;
            }
        }

        private void CurrentBlockDroppedEventHandler(BlockBase block)
        {
            block.Dropped -= CurrentBlockDroppedEventHandler;
            _isMovingBlock = false;

            if (_tower.CanAdd(block))
            {
                SuitableBlockCreated?.Invoke(block);
            }
            else
            {
                block.DestroySelf();
            }
        }
    }
}