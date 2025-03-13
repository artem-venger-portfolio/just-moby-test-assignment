using TMPro;
using UnityEngine;

namespace BlockTower
{
    public class ScrollElement : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _numberField;

        private RectTransform _transform;
        
        private void Awake()
        {
            _transform = (RectTransform)transform;
        }

        public float AnchoredPositionX
        {
            get => _transform.anchoredPosition.x;
            set => _transform.anchoredPosition = new Vector2(value, _transform.anchoredPosition.y);
        }

        public void SetData(ScrollElementData data)
        {
            SetNumberFieldText(data.Number.ToString());
        }

        public void ResetData()
        {
            SetNumberFieldText(string.Empty);
        }

        private void SetNumberFieldText(string text)
        {
            _numberField.text = text;
        }
    }
}