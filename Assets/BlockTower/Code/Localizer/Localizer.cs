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
                    LocalizationKeys.HELLO_WORLD, "Привет, мир!"
                },
            };
            var enDictionary = new Dictionary<string, string>
            {
                {
                    LocalizationKeys.HELLO_WORLD, "Hello, world!"
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