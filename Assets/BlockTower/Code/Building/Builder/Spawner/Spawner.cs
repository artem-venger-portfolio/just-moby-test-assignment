using System;
using BlockTower.Building.Update;
using UnityEngine;

namespace BlockTower.Building
{
    public class Spawner : ISpawner
    {
        private readonly IApplicationEvents _events;
        private readonly BlockBase _blockTemplate;
        private readonly Transform _blockContainer;

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
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log(message: "S key down");
            }
        }
    }
}