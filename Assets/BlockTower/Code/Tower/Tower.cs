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
        private readonly List<float> _topY;

        public Tower(Canvas canvas)
        {
            _canvas = canvas;
            _blocks = new List<TowerBlockBase>();
            _blockAdded = new Subject<TowerBlockBase>();
        }

        public Observable<TowerBlockBase> BlockAdded => _blockAdded;

        public int Count => _blocks.Count;

        public float TopY => _topY[^1];

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
            AddTopY(block);
            _blocks.Add(block);
            _blockAdded.OnNext(block);
        }

        public void Remove(TowerBlockBase block)
        {
            var blockIndex = _blocks.IndexOf(block);
            RemoveTopYAt(blockIndex);
            _blocks.RemoveAt(blockIndex);
        }

        public TowerBlockBase this[int i] => _blocks[i];

        private bool IsTopYEmpty => _topY.Count == 0;

        private void AddTopY(TowerBlockBase block)
        {
            float baseY;
            if (IsTopYEmpty)
            {
                var blockTransform = block.Transform;
                var blockDistanceToBottom = blockTransform.rect.yMin * _canvas.scaleFactor;
                baseY = blockTransform.position.y + blockDistanceToBottom;
            }
            else
            {
                baseY = TopY;
            }

            var blockHeight = GetBlockHeight(block);
            var blockTopY = baseY + blockHeight;

            _topY.Add(blockTopY);
        }

        private void RemoveTopYAt(int index)
        {
            var block = _blocks[index];
            var blockHeight = GetBlockHeight(block);
            _topY.RemoveAt(index);

            for (var i = index; i < _topY.Count; i++)
            {
                _topY[i] -= blockHeight;
            }
        }

        private float GetBlockHeight(TowerBlockBase block)
        {
            return block.Transform.rect.height * _canvas.scaleFactor;
        }
    }
}