using UnityEngine;

namespace BlockTower
{
    public abstract class TowerBlockBase : MonoBehaviour
    {
        public abstract Color Color { get; set; }
        public abstract Vector3[] GetWorldCorners();
    }
}