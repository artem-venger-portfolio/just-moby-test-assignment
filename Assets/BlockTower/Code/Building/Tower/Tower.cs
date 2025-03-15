﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockTower.Building
{
    public class Tower : ITower
    {
        private readonly IList<ICondition> _conditions;
        private readonly List<BlockBase> _blocks;

        public Tower(IList<ICondition> conditions)
        {
            _conditions = conditions;
            _blocks = new List<BlockBase>();
        }

        public bool IsEmpty()
        {
            return _blocks.Count == 0;
        }

        public BlockBase GetLastBlock()
        {
            return _blocks[^1];
        }

        public bool CanAdd(BlockBase block)
        {
            return _conditions.All(c => c.Check(block));
        }

        public void Add(BlockBase block)
        {
            if (CanAdd(block) == false)
            {
                throw new Exception(message: "Can't add block");
            }

            _blocks.Add(block);
        }

        public void Remove(BlockBase block)
        {
            _blocks.Remove(block);
        }
    }
}