using UnityEngine;

namespace BlockTower.Building
{
    public class BuildingSceneController : MonoBehaviour
    {
        private IBuilder _builder;

        public void Awake()
        {
            var conditions = new ICondition[]
            {
                new Condition(),
            };
            ITower tower = new Tower(conditions);
            ISpawner spawner = new Spawner();
            _builder = new Builder(tower, spawner);
        }

        private void OnEnable()
        {
            _builder.Start();
        }

        private void OnDisable()
        {
            _builder.Stop();
        }
    }
}