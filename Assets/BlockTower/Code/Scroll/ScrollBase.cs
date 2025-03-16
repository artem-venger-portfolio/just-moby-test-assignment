using R3;
using UnityEngine;

namespace BlockTower
{
    public abstract class ScrollBase : MonoBehaviour
    {
        public abstract Observable<DropData> BlockDropped { get; }
        public abstract void CreateBlocks();
    }
}