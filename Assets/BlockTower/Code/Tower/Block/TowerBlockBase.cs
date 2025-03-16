using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    public abstract class TowerBlockBase : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private DraggableObject _draggableObject;

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public abstract Vector3[] GetWorldCorners();
    }
}