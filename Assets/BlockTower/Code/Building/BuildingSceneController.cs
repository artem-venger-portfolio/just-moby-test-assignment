using BlockTower.Building.Update;
using UnityEngine;

namespace BlockTower.Building
{
    public class BuildingSceneController : MonoBehaviour
    {
        [SerializeField]
        private BlockBase _blockTemplate;

        [SerializeField]
        private Transform _blockContainer;

        private IBuilder _builder;

        public void Awake()
        {
            var applicationEvents = ApplicationEvents.Create();
            ITower tower = new Tower();
            tower.AddCondition(new AboveLastBlockCondition(tower));
            IBuildingBlocksProvider buildingBlocksProvider = new BuildingBlocksProvider(applicationEvents,
                     _blockTemplate, _blockContainer, tower);
            _builder = new Builder(tower, buildingBlocksProvider);
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