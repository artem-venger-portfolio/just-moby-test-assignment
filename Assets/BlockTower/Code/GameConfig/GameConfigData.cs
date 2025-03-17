using System;
using UnityEngine;

namespace BlockTower
{
    [Serializable]
    public class GameConfigData
    {
        public Color[] Colors;
        public PlacementAnimationConfig PlacementAnimationConfig;
        public RebuildAnimationConfig RebuildAnimationConfig;
        public float RemoveAnimationDuration;
        public float DestructionAnimationDuration;
    }
}