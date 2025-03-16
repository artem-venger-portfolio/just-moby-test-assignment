using System;
using UnityEngine;

namespace BlockTower
{
    public class TextConfigLoader : IConfigLoader
    {
        private Action<IGameConfig> _completed;

        public void Load(Action<IGameConfig> completed)
        {
            _completed = completed;
            var request = Resources.LoadAsync<TextAsset>(path: "TextGameConfig");
            request.completed += TextAssetLoaded;
        }

        private void TextAssetLoaded(AsyncOperation operation)
        {
            var request = (ResourceRequest)operation;
            var textAsset = (TextAsset)request.asset;
            var configData = JsonUtility.FromJson<GameConfigData>(textAsset.text);
            var config = new TextGameConfig(configData);
            _completed(config);
        }
    }
}