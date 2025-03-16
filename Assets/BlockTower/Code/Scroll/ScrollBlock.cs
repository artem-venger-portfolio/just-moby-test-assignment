﻿using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BlockTower
{
    public class ScrollBlock : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public void SetColor(Color color)
        {
            _image.color = color;
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ScrollBlock>
        {
        }
    }
}