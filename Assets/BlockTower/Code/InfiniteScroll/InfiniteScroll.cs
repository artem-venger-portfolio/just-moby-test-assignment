using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    [RequireComponent(typeof(ScrollRect))]
    public class InfiniteScroll : MonoBehaviour
    {
        private ScrollRect _scrollRect;
        
        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }
        
        public int MaxElements { get; set; }
    }
}