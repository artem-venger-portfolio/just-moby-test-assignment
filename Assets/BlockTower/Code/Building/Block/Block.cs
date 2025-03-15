using System;

namespace BlockTower.Building
{
    public class Block : BlockBase
    {
        public override event Action<BlockBase> Dropped;

        public override void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}