using System.Collections.Generic;
using UnityEngine;

namespace BlockTower
{
    public static class Extensions
    {
        public static Vector3[] GetWorldCorners(this RectTransform transform)
        {
            var corners = new Vector3[4];
            transform.GetWorldCorners(corners);

            return corners;
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            return list.Count == 0
                    ? default
                    : list[Random.Range(minInclusive: 0, list.Count)];
        }
    }
}