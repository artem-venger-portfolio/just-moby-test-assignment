using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private SOGameConfig _config;

        [SerializeField]
        private SceneContext _context;

        private void Awake()
        {
            var gameInstaller = new GameInstaller(_config);
            _context.AddNormalInstaller(gameInstaller);
            _context.Run();
            Destroy(gameObject);
        }
    }
}