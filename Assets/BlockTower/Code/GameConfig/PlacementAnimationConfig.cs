using System;
using UnityEngine;

namespace BlockTower
{
    [Serializable]
    public class PlacementAnimationConfig
    {
        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _spinRelativeDuration;

        [SerializeField]
        private float _startHeight;

        [SerializeField]
        private float _moveUpRelativeDuration;

        [SerializeField]
        private float _moveUpDistance;

        public float Duration => _duration;

        public float SpinRelativeDuration => _spinRelativeDuration;

        public float StartHeight => _startHeight;

        public float MoveUpRelativeDuration => _moveUpRelativeDuration;

        public float MoveUpDistance => _moveUpDistance;
    }
}