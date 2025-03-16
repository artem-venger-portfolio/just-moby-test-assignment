using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField]
        private SOGameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<IGameConfigLoader>()
                     .To<SOGameConfigLoader>()
                     .AsSingle()
                     .WithArguments(_gameConfig)
                     .NonLazy();
        }
    }
}