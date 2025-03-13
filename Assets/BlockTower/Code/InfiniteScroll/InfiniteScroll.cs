using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BlockTower
{
    public class InfiniteScroll
    {
        private ScrollElement[] _elements;
        private int _firstElementIndex;

        public ScrollRect ScrollRect { get; set; }
        public int MaxElements { get; set; }
        public ScrollElement ElementTemplate { get; set; }
        public IList<ScrollElementData> DataCollection { get; set; }
        public float SidePadding { get; set; }
        public float Spacing { get; set; }
        public float RecycleThreshold { get; set; }

        public void GenerateElementsWithData()
        {
            GenerateElements();
            SetDataToElements();
            UpdateElementsPosition();
            UpdateContentSize();
        }

        public void StartWatchingScrollRectChanges()
        {
            ScrollRect.onValueChanged.AddListener(ScrollRectValueChangedEventHandler);
        }

        private void GenerateElements()
        {
            var elementsToCreate = Mathf.Min(MaxElements, DataCollection.Count);
            _elements = new ScrollElement[elementsToCreate];
            for (var i = 0; i < elementsToCreate; i++)
            {
                _elements[i] = Object.Instantiate(ElementTemplate, ScrollRect.content, worldPositionStays: false);
            }
        }

        private void SetDataToElements()
        {
            for (var i = 0; i < _elements.Length; i++)
            {
                var dataIndex = _firstElementIndex + i;
                var data = DataCollection[dataIndex];
                _elements[i].SetData(data);
            }
        }

        private void UpdateElementsPosition()
        {
            for (var i = 0; i < _elements.Length; i++)
            {
                var currentElement = _elements[i];
                var dataIndex = _firstElementIndex + i;
                currentElement.AnchoredPositionX = SidePadding + dataIndex * GetWidthPlusSpacing();
            }
        }

        private void UpdateContentSize()
        {
            var contentTransform = (RectTransform)ScrollRect.content.transform;
            var size = contentTransform.sizeDelta;
            var width = SidePadding * 2 + GetWidthPlusSpacing() * DataCollection.Count;
            var height = size.y;
            contentTransform.sizeDelta = new Vector2(width, height);
        }

        private float GetWidthPlusSpacing()
        {
            return ElementTemplate.Width + Spacing;
        }

        private void ScrollRectValueChangedEventHandler(Vector2 value)
        {
        }
    }
}