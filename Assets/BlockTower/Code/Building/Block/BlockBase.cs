using System;
using UnityEngine;

namespace BlockTower.Building
{
    public abstract class BlockBase : MonoBehaviour
    {
        public abstract event Action<BlockBase> Dropped;
        public abstract void FollowMouse();
        public abstract void DestroySelf();
    }
}