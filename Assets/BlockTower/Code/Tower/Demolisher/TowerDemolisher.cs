using JetBrains.Annotations;
using R3;
using UnityEngine;

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
            var blockIndex = _tower.IndexOf(blockBase);
            var blockHeight = blockBase.GetHeight();
            RemoveBlock(blockBase);
            if (_tower.IsEmpty() == false)
            {
                RebuildTower(blockIndex, blockHeight);
            }
        }

        private void RemoveBlock(TowerBlockBase blockBase)
        {
            _tower.Remove(blockBase);
            blockBase.DestroySelf();
        }

        private void RebuildTower(int startIndex, float height)
        {
            for (var i = startIndex; i < _tower.Count; i++)
            {
                var currentBlock = _tower[i];
                var currentBlockTransform = currentBlock.Transform;
                currentBlockTransform.position -= new Vector3(x: 0, height, z: 0);
            }
        }
    }
}