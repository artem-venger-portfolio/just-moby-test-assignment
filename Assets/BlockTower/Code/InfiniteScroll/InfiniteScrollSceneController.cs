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
            var infiniteScroll = new InfiniteScroll
            {
                ScrollRect = _scrollRect,
                MaxElements = 10,
                ElementTemplate = _scrollElementTemplate,
            };
        }
    }
}