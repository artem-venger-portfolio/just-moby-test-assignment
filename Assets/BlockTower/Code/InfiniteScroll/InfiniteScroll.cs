using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
}