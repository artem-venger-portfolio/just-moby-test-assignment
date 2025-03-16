using Zenject;

namespace BlockTower
{
    public class GameInstaller : InstallerBase
    {
        private readonly IGameConfig _config;

        public GameInstaller(IGameConfig config)
        {
            _config = config;
        }

        public override void InstallBindings()
        {
            Container.Bind<IGameConfig>()
                     .FromInstance(_config)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<IProjectLogger>()
                     .To<UnityLogger>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}