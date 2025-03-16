using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class GameInstaller : InstallerBase
    {
        private readonly IGameConfig _config;
        private readonly ScrollBlock _scrollBlockTemplate;
        private readonly Transform _scrollContent;

        public GameInstaller(IGameConfig config, ScrollBlock scrollBlockTemplate, Transform scrollContent)
        {
            _config = config;
            _scrollBlockTemplate = scrollBlockTemplate;
            _scrollContent = scrollContent;
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

            Container.BindFactory<ScrollBlock, ScrollBlock.Factory>()
                     .FromComponentInNewPrefab(_scrollBlockTemplate)
                     .UnderTransform(_scrollContent)
                     .AsSingle()
                     .NonLazy();
        }
    }
}