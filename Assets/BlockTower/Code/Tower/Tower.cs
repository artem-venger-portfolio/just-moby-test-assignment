using System.Collections.Generic;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace BlockTower
{
    [UsedImplicitly]
    public class Tower : ITower
    {
        private readonly Canvas _canvas;
        private readonly List<TowerBlockBase> _blocks;
        private readonly Subject<TowerBlockBase> _blockAdded;

        public Tower(Canvas canvas)
        {
            _canvas = canvas;
            _blocks = new List<TowerBlockBase>();
            _blockAdded = new Subject<TowerBlockBase>();
        }

        public Observable<TowerBlockBase> BlockAdded => _blockAdded;

        public int Count => _blocks.Count;

        public int IndexOf(TowerBlockBase block)
        {
            return _blocks.IndexOf(block);
        }

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
            _blockAdded.OnNext(block);
        }

        public void Remove(TowerBlockBase block)
        {
            var blockIndex = _blocks.IndexOf(block);
            _blocks.RemoveAt(blockIndex);
        }

        public TowerBlockBase this[int i] => _blocks[i];
    }
}