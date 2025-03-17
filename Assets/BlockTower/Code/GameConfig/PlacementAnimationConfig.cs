using System;
using UnityEngine;

namespace BlockTower
{
    [Serializable]
    public class PlacementAnimationConfig
    {
        [SerializeField]
        private float _moveToSpinPositionSpeed;

        [SerializeField]
        private float _placementDuration;

        [SerializeField]
        private float _spinRelativeDuration;

        public float MoveToSpinPositionSpeed => _moveToSpinPositionSpeed;

        public float PlacementDuration => _placementDuration;

        public float SpinRelativeDuration => _spinRelativeDuration;
    }
}