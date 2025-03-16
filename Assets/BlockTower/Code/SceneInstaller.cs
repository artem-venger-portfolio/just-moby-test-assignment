using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField]
        private ScrollBase _scroll;

        public override void InstallBindings()
        {
            Container.Bind<ScrollBase>()
                     .FromInstance(_scroll)
                     .AsSingle()
                     .NonLazy();
        }
    }
}