using System;
using BlockTower.Building.Update;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlockTower.Building
{
    public class Spawner : ISpawner
    {
        private readonly IApplicationEvents _events;
        private readonly BlockBase _blockTemplate;
        private readonly Transform _blockContainer;
        private BlockBase _currentBlock;

        public Spawner(IApplicationEvents events, BlockBase blockTemplate, Transform blockContainer)
        {
            _events = events;
            _blockTemplate = blockTemplate;
            _blockContainer = blockContainer;
        }

        public event Action<BlockBase> Spawned;

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
            if (_currentBlock.IsNullRef() == false)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _currentBlock = Object.Instantiate(_blockTemplate, Input.mousePosition, Quaternion.identity,
                                                   _blockContainer);
                _currentBlock.Dropped += CurrentBlockDroppedEventHandler;
                _currentBlock.FollowMouse();
            }
        }

        private void CurrentBlockDroppedEventHandler(BlockBase block)
        {
            _currentBlock.Dropped -= CurrentBlockDroppedEventHandler;
            _currentBlock = null;
        }
    }
}