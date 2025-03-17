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

        [SerializeField]
        private DropZone _towerDropZone;

        [SerializeField]
        private DropZone _holeDropZone;

        [SerializeField]
        private Transform _towerBlockContainer;

        [SerializeField]
        private TowerBlockBase _towerBlock;

        [SerializeField]
        private Transform _draggingObjectContainer;

        [SerializeField]
        private ActionDisplay _actionDisplay;

        private ITowerBuilder _towerBuilder;
        private ITowerDemolisher _towerDemolisher;

        private void Awake()
        {
            IConfigLoader configLoader = new SOConfigLoader();
            configLoader.Load(ConfigLoadedEventHandler);
        }

        [Inject]
        private void InjectDependencies(ITowerBuilder towerBuilder, ITowerDemolisher towerDemolisher)
        {
            _towerBuilder = towerBuilder;
            _towerDemolisher = towerDemolisher;
        }

        private void ConfigLoadedEventHandler(IGameConfig config)
        {
            RunContext(config);
            InitializeGame();
            Destroy(gameObject);
        }

        private void RunContext(IGameConfig config)
        {
            var gameInstaller = new GameInstaller(config, _scrollBlockTemplate, _scrollContent, _canvas, _scroll,
                                                  _towerDropZone, _towerBlockContainer, _towerBlock,
                                                  _draggingObjectContainer, _holeDropZone, _actionDisplay);
            _context.AddNormalInstaller(gameInstaller);
            _context.Run();
        }

        private void InitializeGame()
        {
            _scroll.CreateBlocks();
            _towerBuilder.Start();
            _towerDemolisher.Start();
        }
    }
}