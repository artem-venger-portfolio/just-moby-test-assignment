using System.Collections.Generic;

namespace BlockTower
{
    public class Localizer : ILocalizer
    {
        private readonly Dictionary<Language, Dictionary<string, string>> _dictionaries;

        public Localizer()
        {
            var ruDictionary = new Dictionary<string, string>
            {
                {
                    LocalizationKeys.BLOCK_DRAGGED_FROM_SCROLL_BAR, "Началось перемещение блока из списка блоков"
                },
                {
                    LocalizationKeys.BLOCK_FROM_SCROLL_BAR_DROPPED, "Прекращено перемещение блока из списка блоков"
                },
                {
                    LocalizationKeys.BLOCK_DROPPED_IN_APPROPRIATE_PLACE, "Блок отпустили в подходящем месте"
                },
                {
                    LocalizationKeys.BLOCK_DROPPED_IN_INAPPROPRIATE_PLACE, "Блок отпустили в неподходящем месте"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DESTROYED, "Блок башни разрушен"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DRAGGED, "Началось перемещение блока башни"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DROPPED, "Прекращено перемещение блока башни"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DROPPED_IN_HOLE, "Блок башни брошен в дыру"
                },
            };
            var enDictionary = new Dictionary<string, string>
            {
                {
                    LocalizationKeys.BLOCK_DRAGGED_FROM_SCROLL_BAR, "BLOCK DRAGGED FROM SCROLL BAR"
                },
                {
                    LocalizationKeys.BLOCK_FROM_SCROLL_BAR_DROPPED, "BLOCK FROM SCROLL BAR DROPPED"
                },
                {
                    LocalizationKeys.BLOCK_DROPPED_IN_APPROPRIATE_PLACE, "BLOCK DROPPED IN APPROPRIATE PLACE"
                },
                {
                    LocalizationKeys.BLOCK_DROPPED_IN_INAPPROPRIATE_PLACE, "BLOCK DROPPED IN INAPPROPRIATE PLACE"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DESTROYED, "TOWER BLOCK DESTROYED"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DRAGGED, "TOWER BLOCK DRAGGED"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DROPPED, "TOWER BLOCK DROPPED"
                },
                {
                    LocalizationKeys.TOWER_BLOCK_DROPPED_IN_HOLE, "TOWER BLOCK DROPPED IN HOLE"
                },
            };
            _dictionaries = new Dictionary<Language, Dictionary<string, string>>
            {
                {
                    Language.RU, ruDictionary
                },
                {
                    Language.EN, enDictionary
                },
            };
        }

        public Language Language { get; set; }

        public string Localize(string key)
        {
            if (_dictionaries.TryGetValue(Language, out var dictionary))
            {
                if (dictionary.TryGetValue(key, out var message))
                {
                    return message;
                }
            }

            return key;
        }
    }
}