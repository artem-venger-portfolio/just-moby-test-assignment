using UnityEngine;

namespace BlockTower.Building
{
    public static class Extensions
    {
        public static bool IsNullRef(this Object unityObject)
        {
            return ReferenceEquals(unityObject, objB: null);
        }
    }
}