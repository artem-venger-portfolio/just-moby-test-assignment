using System;

namespace BlockTower
{
    public interface IGameConfigLoader
    {
        void Load(Action<IGameConfig> completed);
    }
}