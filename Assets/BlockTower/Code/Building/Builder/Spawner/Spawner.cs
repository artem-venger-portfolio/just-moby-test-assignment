using System;
using BlockTower.Building.Update;
using UnityEngine;

namespace BlockTower.Building
{
    public class Spawner : ISpawner
    {
        private readonly IApplicationEvents _events;

        public Spawner(IApplicationEvents events)
        {
            _events = events;
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
                Debug.Log("S key down");
            }
        }
    }
}