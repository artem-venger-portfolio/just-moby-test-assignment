using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BlockTower
{
    public class ScrollBlock : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private DraggableObject _draggableObject;

        private UIUtility _uiUtility;

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public bool IsAtScreenPoint(Vector2 screenPoint)
        {
            return _uiUtility.RectangleContainsScreenPoint(Transform, screenPoint);
        }

        public void OnBeginDrag()
        {
            _draggableObject.OnBeginDrag();
        }

        public void OnDrag(Vector2 position)
        {
            _draggableObject.OnDrag(position);
        }

        public void OnEndDrag()
        {
            _draggableObject.OnEndDrag();
        }

        public Vector3[] GetWorldCorners()
        {
            return _draggableObject.GetWorldCorners();
        }

        private RectTransform Transform => (RectTransform)transform;

        [Inject]
        private void InjectDependencies(UIUtility uiUtility, Transform draggingObjectContainer)
        {
            _uiUtility = uiUtility;
            _draggableObject.SetDraggingObjectContainer(draggingObjectContainer);
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ScrollBlock>
        {
        }
    }
}