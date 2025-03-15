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

        public void Awake()
        {
            var applicationEvents = ApplicationEvents.Create();
            var conditions = new ICondition[]
            {
                new Condition(),
            };
            ITower tower = new Tower(conditions);
            ISpawner spawner = new Spawner(applicationEvents, _blockTemplate, _blockContainer);
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