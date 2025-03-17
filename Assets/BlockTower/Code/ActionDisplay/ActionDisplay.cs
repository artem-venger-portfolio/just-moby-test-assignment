using R3;
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
        private IActionEventBus _bus;

        public void Initialize()
        {
            _bus.EventStream
                .Select(GetKey)
                .Subscribe(PrintLocalized);
        }

        [Inject]
        private void InjectDependencies(ILocalizer localizer, IActionEventBus bus)
        {
            _localizer = localizer;
            _bus = bus;
        }

        private static string GetKey(ActionEvent actionEvent)
        {
            return actionEvent switch
            {
                _ => actionEvent.ToString(),
            };
        }

        private void PrintLocalized(string key)
        {
            _textFiled.text = _localizer.Localize(key);
        }
    }
}