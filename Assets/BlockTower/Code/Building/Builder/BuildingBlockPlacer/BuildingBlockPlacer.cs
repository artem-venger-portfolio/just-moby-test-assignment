namespace BlockTower.Building
{
    public class BuildingBlockPlacer : IBuildingBlockPlacer
    {
        private readonly ITower _tower;

        public BuildingBlockPlacer(ITower tower)
        {
            _tower = tower;
        }

        public void Place(BlockBase block)
        {
            _tower.Add(block);
        }
    }
}