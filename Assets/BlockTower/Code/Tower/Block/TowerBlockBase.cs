using R3;
using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    public abstract class TowerBlockBase : MonoBehaviour
    {
        public abstract Image Image { get; }
        public abstract RectTransform Transform { get; }
        public abstract Color Color { get; set; }
        public abstract Observable<TowerBlockBase> DroppedStream { get; }
        public abstract Vector3[] GetWorldCorners();
        public abstract float GetHeight();
        public abstract void ReturnToBeginDragPosition();
    }
}