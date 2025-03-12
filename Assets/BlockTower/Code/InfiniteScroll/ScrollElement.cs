using TMPro;
using UnityEngine;

namespace BlockTower
{
    public class ScrollElement : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _numberField;

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