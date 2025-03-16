using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private SceneContext _context;

        [SerializeField]
        private ScrollBase _scroll;

        private void Awake()
        {
            IConfigLoader configLoader = new SOConfigLoader();
            configLoader.Load(ConfigLoadedEventHandler);
        }

        private void ConfigLoadedEventHandler(IGameConfig config)
        {
            RunContext(config);
            InitializeGame();
            Destroy(gameObject);
        }

        private void RunContext(IGameConfig config)
        {
            var gameInstaller = new GameInstaller(config);
            _context.AddNormalInstaller(gameInstaller);
            _context.Run();
        }

        private void InitializeGame()
        {
            _scroll.CreateBlocks();
        }
    }
}