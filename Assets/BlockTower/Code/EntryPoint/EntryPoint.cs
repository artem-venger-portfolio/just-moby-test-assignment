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

        [SerializeField]
        private ScrollBlock _scrollBlockTemplate;

        [SerializeField]
        private Transform _scrollContent;

        [SerializeField]
        private Canvas _canvas;

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
            var gameInstaller = new GameInstaller(config, _scrollBlockTemplate, _scrollContent, _canvas);
            _context.AddNormalInstaller(gameInstaller);
            _context.Run();
        }

        private void InitializeGame()
        {
            _scroll.CreateBlocks();
        }
    }
}