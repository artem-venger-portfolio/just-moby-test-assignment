using JetBrains.Annotations;

namespace BlockTower
{
    [UsedImplicitly]
    public class TowerDemolisher : ITowerDemolisher
    {
        private readonly ITower _tower;

        public TowerDemolisher(ITower tower)
        {
            _tower = tower;
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}