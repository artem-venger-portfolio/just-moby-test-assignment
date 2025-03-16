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
        private readonly ScrollBase _scroll;
        private readonly DropZone _towerDropZone;
        private readonly TowerBlockBase _towerBlock;

        public GameInstaller(IGameConfig config, ScrollBlock scrollBlockTemplate, Transform scrollContent,
                             Canvas canvas, ScrollBase scroll, DropZone towerDropZone, TowerBlockBase towerBlock)
        {
            _config = config;
            _scrollBlockTemplate = scrollBlockTemplate;
            _scrollContent = scrollContent;
            _canvas = canvas;
            _scroll = scroll;
            _towerDropZone = towerDropZone;
            _towerBlock = towerBlock;
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

            Container.Bind<ScrollBase>()
                     .FromInstance(_scroll)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<ITowerBuilder>()
                     .To<TowerBuilder>()
                     .AsSingle()
                     .WithArguments(_towerDropZone)
                     .NonLazy();

            Container.Bind<ITower>()
                     .To<Tower>()
                     .AsSingle()
                     .NonLazy();

            Container.BindFactory<TowerBlockBase, TowerBlockFactory>()
                     .FromComponentInNewPrefab(_towerBlock)
                     .UnderTransform(_towerDropZone.transform)
                     .AsSingle()
                     .NonLazy();
        }
    }
}