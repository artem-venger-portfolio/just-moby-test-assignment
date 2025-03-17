using JetBrains.Annotations;
using R3;

namespace BlockTower
{
    [UsedImplicitly]
    public class TowerDemolisher : ITowerDemolisher
    {
        private readonly CompositeDisposable _compositeDisposable;
        private readonly ITower _tower;

        public TowerDemolisher(ITower tower)
        {
            _tower = tower;
            _compositeDisposable = new CompositeDisposable();
        }

        public void Start()
        {
            _tower.BlockAdded
                  .Subscribe(block => block.DroppedInHole.Subscribe(BlockDroppedInHoleEventHandler))
                  .AddTo(_compositeDisposable);
        }

        public void Stop()
        {
            _compositeDisposable.Clear();
        }

        private void BlockDroppedInHoleEventHandler(TowerBlockBase blockBase)
        {
            _tower.Remove(blockBase);
            blockBase.DestroySelf();
        }
    }
}