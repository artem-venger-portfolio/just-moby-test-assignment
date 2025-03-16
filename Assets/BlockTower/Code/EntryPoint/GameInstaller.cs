using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class GameInstaller : InstallerBase
    {
        private readonly IGameConfig _config;
        private readonly ScrollBlock _scrollBlockTemplate;
        private readonly Transform _scrollContent;
        private readonly Canvas _canvas;

        public GameInstaller(IGameConfig config, ScrollBlock scrollBlockTemplate, Transform scrollContent,
                             Canvas canvas)
        {
            _config = config;
            _scrollBlockTemplate = scrollBlockTemplate;
            _scrollContent = scrollContent;
            _canvas = canvas;
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

            Container.Bind<Canvas>()
                     .FromInstance(_canvas)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<UIUtility>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}