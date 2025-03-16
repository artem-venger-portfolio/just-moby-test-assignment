using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class DropZone : MonoBehaviour
    {
        private UIUtility _uiUtility;

        public bool IsInZone(Vector2 screenPoint)
        {
            return _uiUtility.RectangleContainsScreenPoint((RectTransform)transform, screenPoint);
        }

        [Inject]
        private void InjectDependencies(UIUtility uiUtility)
        {
            _uiUtility = uiUtility;
        }
    }
}