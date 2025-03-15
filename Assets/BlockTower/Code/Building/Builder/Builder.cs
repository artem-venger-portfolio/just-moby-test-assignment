namespace BlockTower.Building
{
    public class Builder : IBuilder
    {
        private readonly ITower _tower;
        private readonly IBuildingBlocksProvider _buildingBlocksProvider;

        public Builder(ITower tower, IBuildingBlocksProvider buildingBlocksProvider)
        {
            _tower = tower;
            _buildingBlocksProvider = buildingBlocksProvider;
        }

        public void Start()
        {
            _buildingBlocksProvider.SuitableBlockCreated += BuildingBlockCreatedEventHandler;
            _buildingBlocksProvider.Start();
        }

        public void Stop()
        {
            _buildingBlocksProvider.SuitableBlockCreated -= BuildingBlockCreatedEventHandler;
            _buildingBlocksProvider.Stop();
        }

        private void BuildingBlockCreatedEventHandler(BlockBase block)
        {
            _tower.Add(block);
        }
    }
}