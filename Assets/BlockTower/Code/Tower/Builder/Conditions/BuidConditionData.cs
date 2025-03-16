using UnityEngine;

namespace BlockTower
{
    public readonly struct BuildConditionData
    {
        public readonly Vector3[] CheckingBlockCorners;

        public BuildConditionData(Vector3[] checkingBlockCorners)
        {
            CheckingBlockCorners = checkingBlockCorners;
        }
    }
}