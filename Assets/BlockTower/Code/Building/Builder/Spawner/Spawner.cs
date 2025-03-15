using System;
using BlockTower.Building.Update;

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
    }
}