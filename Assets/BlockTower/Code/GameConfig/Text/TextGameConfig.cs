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
        public PlacementAnimationConfig PlacementAnimationConfig => _data.PlacementAnimationConfig;
        public RebuildAnimationConfig RebuildAnimationConfig => _data.RebuildAnimationConfig;
        public float RemoveAnimationDuration => _data.RemoveAnimationDuration;
    }
}