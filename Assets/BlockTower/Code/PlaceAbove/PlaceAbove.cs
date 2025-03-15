using TMPro;
using UnityEngine;

namespace BlockTower.PlaceAbove
{
    public class PlaceAbove : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _object;

        [SerializeField]
        private RectTransform _object2;

        [SerializeField]
        private TMP_Text _textFiled;

        private void Start()
        {
            var currentPosition = _object.position;
            var bottomOffset = _object.rect.yMin;

            var targetPosition = _object2.position;
            var targetTopOffset = _object2.rect.yMax;
            var targetTopY = targetPosition.y + targetTopOffset;

            var newPositionY = targetTopY - bottomOffset;

            _object.position = new Vector3(currentPosition.x, newPositionY, currentPosition.z);
        }

        private void Update()
        {
            _textFiled.text = $"Min.x {_object.rect.min.x} xMin {_object.rect.xMin}";
        }
    }
}