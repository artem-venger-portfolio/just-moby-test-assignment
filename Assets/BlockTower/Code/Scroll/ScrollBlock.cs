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

        public void SetColor(Color color)
        {
            _image.color = color;
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

        private RectTransform Transform => (RectTransform)transform;

        [Inject]
        private void InjectDependencies(UIUtility uiUtility)
        {
            _uiUtility = uiUtility;
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ScrollBlock>
        {
        }
    }
}