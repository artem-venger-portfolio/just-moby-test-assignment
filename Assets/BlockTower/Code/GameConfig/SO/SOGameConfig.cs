using System.Collections.Generic;
using UnityEngine;

namespace BlockTower
{
    [CreateAssetMenu(menuName = "BlockTower/" + TYPE_NAME, fileName = TYPE_NAME)]
    public class SOGameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField]
        private GameConfigData _config;

        private const string TYPE_NAME = nameof(SOGameConfig);

        public IList<Color> Colors => _config.Colors;
        public PlacementAnimationConfig PlacementAnimationConfig => _config.PlacementAnimationConfig;
    }
}