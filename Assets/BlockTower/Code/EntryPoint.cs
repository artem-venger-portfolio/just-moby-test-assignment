using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
        }

        [Inject]
        private void InjectDependencies()
        {
        }
    }
}