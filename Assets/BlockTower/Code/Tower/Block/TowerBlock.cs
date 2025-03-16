using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    public class TowerBlock : TowerBlockBase
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private DraggableObject _draggableObject;

        public override Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public override Vector3[] GetWorldCorners()
        {
            var corners = new Vector3[4];
            Transform.GetWorldCorners(corners);

            return corners;
        }

        private RectTransform Transform => (RectTransform)transform;
    }
}