using TMPro;
using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class ActionDisplay : MonoBehaviour, IActionDisplay
    {
        [SerializeField]
        private TMP_Text _textFiled;

        private ILocalizer _localizer;

        public void PrintLocalized(string key)
        {
            _textFiled.text = _localizer.Localize(key);
        }

        [Inject]
        private void InjectDependencies(ILocalizer localizer)
        {
            _localizer = localizer;
        }
    }
}