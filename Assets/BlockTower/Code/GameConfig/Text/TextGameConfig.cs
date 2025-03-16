using System.Collections.Generic;
using UnityEngine;

namespace BlockTower
{
    public class TextGameConfig : IGameConfig
    {
        private readonly GameConfigData _data;

        public TextGameConfig(GameConfigData data)
        {
            _data = data;
        }

        public IList<Color> Colors => _data.Colors;
    }
}