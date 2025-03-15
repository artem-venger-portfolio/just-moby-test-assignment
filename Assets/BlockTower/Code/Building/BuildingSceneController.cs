using UnityEngine;

namespace BlockTower.Building
{
    public class BuildingSceneController : MonoBehaviour
    {
        public void Awake()
        {
            var conditions = new ICondition[]
            {
                new Condition(),
            };
            ITower tower = new Tower(conditions);
        }
    }
}