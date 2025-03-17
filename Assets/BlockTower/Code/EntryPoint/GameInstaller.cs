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
        private readonly Transform _towerBlockContainer;
        private readonly TowerBlockBase _towerBlock;
        private readonly Transform _draggingObjectContainer;

        public GameInstaller(IGameConfig config, ScrollBlock scrollBlockTemplate, Transform scrollContent,
                             Canvas canvas, ScrollBase scroll, DropZone towerDropZone, Transform towerBlockContainer,
                             TowerBlockBase towerBlock, Transform draggingObjectContainer)
        {
            _config = config;
            _scrollBlockTemplate = scrollBlockTemplate;
            _scrollContent = scrollContent;
            _canvas = canvas;
            _scroll = scroll;
            _towerDropZone = towerDropZone;
            _towerBlockContainer = towerBlockContainer;
            _towerBlock = towerBlock;
            _draggingObjectContainer = draggingObjectContainer;
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
                     .WithArguments(_draggingObjectContainer)
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
                     .FromSubContainerResolve()
                     .ByMethod(InstallTowerBuilder)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<ITowerDemolisher>()
                     .To<TowerDemolisher>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<ITower>()
                     .To<Tower>()
                     .AsSingle()
                     .NonLazy();
        }

        private void InstallTowerBuilder(DiContainer subContainer)
        {
            subContainer.Bind<ITowerBuilder>()
                        .To<TowerBuilder>()
                        .AsSingle()
                        .WithArguments(_towerDropZone)
                        .NonLazy();

            subContainer.BindFactory<TowerBlockBase, TowerBlockFactory>()
                        .FromComponentInNewPrefab(_towerBlock)
                        .UnderTransform(_towerBlockContainer)
                        .AsSingle()
                        .WithArguments(_draggingObjectContainer)
                        .NonLazy();

            subContainer.Bind<IBuildCondition>()
                        .To(typeof(WithinScreenCondition), typeof(AboveLastBlockCondition))
                        .AsTransient()
                        .NonLazy();

            subContainer.Bind<IDestructionAnimator>()
                        .To<DestructionAnimator>()
                        .AsSingle()
                        .NonLazy();

            subContainer.Bind<IPlacementAnimator>()
                        .To<PlacementAnimator>()
                        .AsSingle()
                        .NonLazy();
        }
    }
}