using System;
using UnityEngine;

namespace BlockTower
{
    public class SOConfigLoader : IConfigLoader
    {
        private Action<IGameConfig> _completed;

        public void Load(Action<IGameConfig> completed)
        {
            _completed = completed;
            var request = Resources.LoadAsync<SOGameConfig>(path: "SOGameConfig");
            request.completed += TextAssetLoaded;
        }

        private void TextAssetLoaded(AsyncOperation operation)
        {
            var request = (ResourceRequest)operation;
            var config = (SOGameConfig)request.asset;
            _completed(config);
        }
    }
}