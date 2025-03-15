using System;

namespace BlockTower.Building
{
    public class Spawner : ISpawner
    {
        public event Action<BlockBase> Spawned;
    }
}