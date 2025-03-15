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
    }
}