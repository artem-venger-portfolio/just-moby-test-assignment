using System;

namespace BlockTower.Building
{
    public interface IBuildingBlocksProvider
    {
        event Action<BlockBase> SuitableBlockCreated;
        void Start();
        void Stop();
    }
}