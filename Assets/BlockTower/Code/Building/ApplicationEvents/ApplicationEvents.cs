using System;
using UnityEngine;

namespace BlockTower.Building.Update
{
    public class ApplicationEvents : MonoBehaviour, IApplicationEvents
    {
        public event Action Updated;

        public static IApplicationEvents Create()
        {
            var updateGO = new GameObject(nameof(ApplicationEvents));
            var update = updateGO.AddComponent<ApplicationEvents>();

            return update;
        }

        public void Update()
        {
            Updated?.Invoke();
        }
    }
}