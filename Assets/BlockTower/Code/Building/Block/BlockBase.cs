﻿using System;
using UnityEngine;

namespace BlockTower.Building
{
    public abstract class BlockBase : MonoBehaviour
    {
        public abstract RectTransform Transform { get; }
        public abstract event Action<BlockBase> Dropped;
        public abstract Vector3[] GetWorldCorners();
        public abstract void FollowMouse();
        public abstract void DestroySelf();
    }
}