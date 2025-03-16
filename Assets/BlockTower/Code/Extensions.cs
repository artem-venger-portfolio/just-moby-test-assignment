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
    }
}