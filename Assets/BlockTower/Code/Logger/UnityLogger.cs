using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace BlockTower
{
    [UsedImplicitly]
    public class UnityLogger : IProjectLogger
    {
        private readonly StringBuilder _stringBuilder = new();

        public void LogInfo(string message)
        {
            Debug.Log(message);
        }

        public void LogInfo(string message, params string[] tags)
        {
            var formattedMessage = FormatMessage(message, tags);
            Debug.Log(formattedMessage);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

        private string FormatMessage(string message, string[] tags)
        {
            foreach (var currentTag in tags)
            {
                _stringBuilder.Append($"[{currentTag}]");
            }
            _stringBuilder.Append(" " + message);

            var result = _stringBuilder.ToString();
            _stringBuilder.Clear();

            return result;
        }
    }
}