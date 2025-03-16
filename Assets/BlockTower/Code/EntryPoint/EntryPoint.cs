using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private SceneContext _context;

        private void Awake()
        {
            IConfigLoader configLoader = new TextConfigLoader();
            configLoader.Load(ConfigLoadedEventHandler);
        }

        private void ConfigLoadedEventHandler(IGameConfig config)
        {
            var gameInstaller = new GameInstaller(config);
            _context.AddNormalInstaller(gameInstaller);
            _context.Run();
            Destroy(gameObject);
        }
    }
}