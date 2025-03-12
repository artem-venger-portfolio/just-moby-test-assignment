using System.Collections.Generic;
using UnityEngine.UI;

namespace BlockTower
{
    public class InfiniteScroll
    {
        public ScrollRect ScrollRect { get; set; }
        public int MaxElements { get; set; }
        public ScrollElement ElementTemplate { get; set; }
        public IList<ScrollElementData> DataCollection { get; set; }
    }
}