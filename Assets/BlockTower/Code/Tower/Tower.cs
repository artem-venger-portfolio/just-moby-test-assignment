using System.Collections.Generic;
using JetBrains.Annotations;

namespace BlockTower
{
    [UsedImplicitly]
    public class Tower : ITower
    {
        private readonly List<TowerBlockBase> _blocks = new();

        public bool IsEmpty()
        {
            return _blocks.Count == 0;
        }

        public TowerBlockBase GetLastBlock()
        {
            return _blocks[^1];
        }

        public void Add(TowerBlockBase block)
        {
            _blocks.Add(block);
        }

        public void Remove(TowerBlockBase block)
        {
            _blocks.Remove(block);
        }

        public TowerBlockBase this[int i] => _blocks[i];
    }
}