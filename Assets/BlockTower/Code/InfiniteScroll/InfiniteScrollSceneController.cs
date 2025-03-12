using UnityEngine;

namespace BlockTower
{
    public class InfiniteScrollSceneController : MonoBehaviour
    {
        [SerializeField]
        private InfiniteScroll _infiniteScroll;

        [SerializeField]
        private ScrollElement _scrollElementTemplate;

        private void Start()
        {
            _infiniteScroll.MaxElements = 10;
        }
    }
}