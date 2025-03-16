using Zenject;

namespace BlockTower
{
    public class Scroll : ScrollBase
    {
        private IGameConfig _config;
        private IProjectLogger _logger;

        public override void CreateBlocks()
        {
            LogInfo(nameof(CreateBlocks));
        }

        [Inject]
        private void InjectDependencies(IGameConfig config, IProjectLogger logger)
        {
            _config = config;
            _logger = logger;
        }

        private void LogInfo(string message)
        {
            _logger.LogInfo($"[{nameof(Scroll)}] {message}");
        }
    }
}