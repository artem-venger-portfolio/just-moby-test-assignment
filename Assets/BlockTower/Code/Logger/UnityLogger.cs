using JetBrains.Annotations;
using UnityEngine;

namespace BlockTower
{
    [UsedImplicitly]
    public class UnityLogger : IProjectLogger
    {
        public void LogInfo(string message)
        {
            Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}