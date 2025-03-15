using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlockTower.Building
{
    public class Block : BlockBase, IPointerMoveHandler, IPointerDownHandler
    {
        private bool _isFollowingMouse;

        public override event Action<BlockBase> Dropped;

        public override Vector3[] GetWorldCorners()
        {
            var corners = new Vector3[4];
            var rectTransform = (RectTransform)transform;
            rectTransform.GetWorldCorners(corners);

            return corners;
        }

        public override void FollowMouse()
        {
            _isFollowingMouse = true;
        }

        public override void DestroySelf()
        {
            Destroy(gameObject);
        }

        void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
        {
            if (_isFollowingMouse == false)
            {
                return;
            }

            transform.position = eventData.position;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (_isFollowingMouse == false)
            {
                return;
            }

            Dropped?.Invoke(this);
            _isFollowingMouse = false;
        }
    }
}