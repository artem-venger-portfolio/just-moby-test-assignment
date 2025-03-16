﻿using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    public class TowerBlock : TowerBlockBase
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private DraggableObject _draggableObject;

        public override Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public override Image Image => _image;

        public override RectTransform Transform => (RectTransform)transform;

        public override Vector3[] GetWorldCorners()
        {
            return Transform.GetWorldCorners();
        }
    }
}