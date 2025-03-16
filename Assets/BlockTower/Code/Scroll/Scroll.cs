using Zenject;

namespace BlockTower
{
    public class Scroll : ScrollBase
    {
        private IGameConfig _config;
        private IProjectLogger _logger;
        private ScrollBlock.Factory _blockFactory;
        private ScrollBlock[] _blocks;

        public override void CreateBlocks()
        {
            LogInfo(nameof(CreateBlocks));
            var colors = _config.Colors;
            _blocks = new ScrollBlock[colors.Count];
            for (var i = 0; i < colors.Count; i++)
            {
                var currentColor = colors[i];
                var currentBlock = _blockFactory.Create();
                currentBlock.SetColor(currentColor);
                _blocks[i] = currentBlock;
            }
        }

        [Inject]
        private void InjectDependencies(IGameConfig config, IProjectLogger logger, ScrollBlock.Factory blockFactory)
        {
            _config = config;
            _logger = logger;
            _blockFactory = blockFactory;
        }

        private void LogInfo(string message)
        {
            _logger.LogInfo(message, nameof(Scroll));
        }
    }
}