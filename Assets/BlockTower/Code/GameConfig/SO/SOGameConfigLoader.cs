using System;
using JetBrains.Annotations;

namespace BlockTower
{
    [UsedImplicitly]
    public class SOGameConfigLoader : IGameConfigLoader
    {
        private readonly SOGameConfig _config;

        public SOGameConfigLoader(SOGameConfig config)
        {
            _config = config;
        }

        public void Load(Action<IGameConfig> completed)
        {
            completed(_config);
        }
    }
}