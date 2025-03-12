using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    public class InfiniteScrollSceneController : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField]
        private ScrollElement _scrollElementTemplate;

        private void Start()
        {
            var dataArray = new ScrollElementData[100];
            for (var i = 0; i < 100; i++)
            {
                dataArray[i] = new ScrollElementData();
            }
            var infiniteScroll = new InfiniteScroll
            {
                ScrollRect = _scrollRect,
                MaxElements = 10,
                ElementTemplate = _scrollElementTemplate,
                DataCollection = dataArray,
            };
            infiniteScroll.GenerateElementsWithData();
        }
    }
}