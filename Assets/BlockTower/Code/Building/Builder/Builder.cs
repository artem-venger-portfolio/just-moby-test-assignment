namespace BlockTower.Building
{
    public class Builder : IBuilder
    {
        private readonly ITower _tower;
        private readonly ISpawner _spawner;

        public Builder(ITower tower, ISpawner spawner)
        {
            _tower = tower;
            _spawner = spawner;
        }

        public void Start()
        {
            _spawner.Spawned += BlockSpawnedEventHandler;
            _spawner.Start();
        }

        public void Stop()
        {
            _spawner.Spawned -= BlockSpawnedEventHandler;
            _spawner.Stop();
        }

        private void BlockSpawnedEventHandler(BlockBase block)
        {
            block.Dropped += BlockDroppedEventHandler;
        }

        private void BlockDroppedEventHandler(BlockBase block)
        {
            block.Dropped -= BlockDroppedEventHandler;
            if (_tower.CanAdd(block))
            {
                _tower.Add(block);
            }
            else
            {
                block.DestroySelf();
            }
        }
    }
}