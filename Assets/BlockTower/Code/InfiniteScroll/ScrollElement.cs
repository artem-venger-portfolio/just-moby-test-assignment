using TMPro;
using UnityEngine;

namespace BlockTower
{
    public class ScrollElement : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _numberField;

        public float AnchoredPositionX
        {
            get => Transform.anchoredPosition.x;
            set => Transform.anchoredPosition = new Vector2(value, Transform.anchoredPosition.y);
        }

        public float Width => Transform.sizeDelta.x;

        public void SetData(ScrollElementData data)
        {
            SetNumberFieldText(data.Number.ToString());
        }

        public void ResetData()
        {
            SetNumberFieldText(string.Empty);
        }

        private RectTransform Transform => (RectTransform)transform;

        private void SetNumberFieldText(string text)
        {
            _numberField.text = text;
        }
    }
}