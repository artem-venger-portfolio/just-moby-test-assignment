using UnityEngine;

namespace BlockTower
{
    public class TowerBlock : TowerBlockBase
    {
        public override Vector3[] GetWorldCorners()
        {
            var corners = new Vector3[4];
            Transform.GetWorldCorners(corners);

            return corners;
        }

        private RectTransform Transform => (RectTransform)transform;
    }
}