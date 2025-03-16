using System;

namespace BlockTower
{
    public interface IConfigLoader
    {
        void Load(Action<IGameConfig> completed);
    }
}