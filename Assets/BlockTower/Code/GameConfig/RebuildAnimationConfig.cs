using System;
using UnityEngine;

namespace BlockTower
{
    [Serializable]
    public class RebuildAnimationConfig
    {
        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _interval;

        public float Duration => _duration;

        public float Interval => _interval;
    }
}