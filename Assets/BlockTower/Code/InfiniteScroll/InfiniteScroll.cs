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
        private bool _isFirstElementHidden;

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

        public void CalculateMaxVisibleElements()
        {
            var viewport = ScrollRect.viewport;
            var viewportWidth = viewport.rect.width;
            var maxVisibleElements = Mathf.CeilToInt(viewportWidth / GetWidthPlusSpacing()) + 1;
        }

        public void StartWatchingScrollRectChanges()
        {
            ScrollRect.onValueChanged.AddListener(ScrollRectValueChangedEventHandler);
        }

        private RectTransform ScrollRectContent => ScrollRect.content;

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
            var size = ScrollRectContent.sizeDelta;
            var width = SidePadding * 2 + GetWidthPlusSpacing() * DataCollection.Count;
            var height = size.y;
            ScrollRectContent.sizeDelta = new Vector2(width, height);
        }

        private float GetWidthPlusSpacing()
        {
            return ElementTemplate.Width + Spacing;
        }

        private void ScrollRectValueChangedEventHandler(Vector2 value)
        {
            var isFirstElementHidden = ScrollRectContent.anchoredPosition.x + _elements[1].AnchoredPositionX <= 0;
            if (_isFirstElementHidden != isFirstElementHidden)
            {
                _isFirstElementHidden = isFirstElementHidden;
                Debug.Log($"{nameof(_isFirstElementHidden)}: {_isFirstElementHidden}");
            }
        }
    }
}