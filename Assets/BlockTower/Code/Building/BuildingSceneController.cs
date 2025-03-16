﻿using BlockTower.Building.Update;
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
        private IRemover _remover;

        public void Awake()
        {
            var applicationEvents = ApplicationEvents.Create();
            ITower tower = new Tower();
            tower.AddCondition(new AboveLastBlockCondition(tower));
            tower.AddCondition(new WithinScreenCondition());
            IBuildingBlocksProvider buildingBlocksProvider = new BuildingBlocksProvider(applicationEvents,
                     _blockTemplate, _blockContainer, tower);
            IBuildingBlockPlacer buildingBlockPlacer = new BuildingBlockPlacer(tower);
            _builder = new Builder(buildingBlocksProvider, buildingBlockPlacer);

            _remover = new Remover(applicationEvents, tower);
        }

        private void OnEnable()
        {
            _builder.Start();
            _remover.Start();
        }

        private void OnDisable()
        {
            _builder.Stop();
            _remover.Stop();
        }
    }
}