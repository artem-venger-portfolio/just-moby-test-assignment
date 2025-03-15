using System;

namespace BlockTower.Building
{
    public interface ISpawner
    {
        event Action<BlockBase> Spawned;
    }
}