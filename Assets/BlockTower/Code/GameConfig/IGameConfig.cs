﻿using System.Collections.Generic;
using UnityEngine;

namespace BlockTower
{
    public interface IGameConfig
    {
        IList<Color> Colors { get; }
        PlacementAnimationConfig PlacementAnimationConfig { get; }
    }
}