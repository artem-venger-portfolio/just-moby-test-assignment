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
                ActionEvent.BlockDraggedFromScrollBar        => LocalizationKeys.BLOCK_DRAGGED_FROM_SCROLL_BAR,
                ActionEvent.BlockFromScrollBarDropped        => LocalizationKeys.BLOCK_FROM_SCROLL_BAR_DROPPED,
                ActionEvent.BlockDroppedInAppropriatePlace   => LocalizationKeys.BLOCK_DROPPED_IN_APPROPRIATE_PLACE,
                ActionEvent.BlockDroppedInInappropriatePlace => LocalizationKeys.BLOCK_DROPPED_IN_INAPPROPRIATE_PLACE,
                ActionEvent.TowerBlockDestroyed              => LocalizationKeys.TOWER_BLOCK_DESTROYED,
                ActionEvent.TowerBlockDragged                => LocalizationKeys.TOWER_BLOCK_DRAGGED,
                ActionEvent.TowerBlockDropped                => LocalizationKeys.TOWER_BLOCK_DROPPED,
                ActionEvent.TowerBlockDroppedInHole          => LocalizationKeys.TOWER_BLOCK_DROPPED_IN_HOLE,
                _                                            => actionEvent.ToString(),
            };
        }

        private void PrintLocalized(string key)
        {
            _textFiled.text = _localizer.Localize(key);
        }
    }
}