namespace BlockTower.Building
{
    public class Builder : IBuilder
    {
        private readonly IBuildingBlocksProvider _provider;
        private readonly IBuildingBlockPlacer _placer;

        public Builder(IBuildingBlocksProvider provider, IBuildingBlockPlacer placer)
        {
            _provider = provider;
            _placer = placer;
        }

        public void Start()
        {
            _provider.SuitableBlockCreated += CreatedEventHandler;
            _provider.Start();
        }

        public void Stop()
        {
            _provider.SuitableBlockCreated -= CreatedEventHandler;
            _provider.Stop();
        }

        private void CreatedEventHandler(BlockBase block)
        {
            _placer.Place(block);
        }
    }
}