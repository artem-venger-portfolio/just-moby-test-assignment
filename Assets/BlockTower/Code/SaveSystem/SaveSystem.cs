using System.IO;
using UnityEngine;
using Zenject;

namespace BlockTower
{
    public class SaveSystem : MonoBehaviour, ISaveSystem
    {
        private ITower _tower;
        private TowerBlockFactory _blockFactory;

        public void Load()
        {
            var filePath = GetFilePath();
            if (File.Exists(filePath) == false)
            {
                return;
            }

            string json;
            using (var sr = new StreamReader(filePath))
            {
                json = sr.ReadToEnd();
            }

            if (json == string.Empty)
            {
                return;
            }

            var persistentData = JsonUtility.FromJson<PersistentData>(json);

            foreach (var currentBlockData in persistentData.Blocks)
            {
                var block = _blockFactory.Create();
                block.Color = currentBlockData.Color;
                block.Transform.anchoredPosition = currentBlockData.AnchoredPosition;
                _tower.Add(block);
            }
        }

        [Inject]
        private void InjectDependencies(ITower tower, TowerBlockFactory blockFactory)
        {
            _tower = tower;
            _blockFactory = blockFactory;
        }

        private void Save()
        {
            var blocksCount = _tower.Count;
            var blockPersistentData = new BlockPersistentData[blocksCount];
            for (var i = 0; i < blocksCount; i++)
            {
                var currentBlock = _tower[i];
                blockPersistentData[i] = new BlockPersistentData
                {
                    Color = currentBlock.Color,
                    AnchoredPosition = currentBlock.Transform.anchoredPosition,
                };
            }

            var persistentData = new PersistentData
            {
                Blocks = blockPersistentData,
            };
            var json = JsonUtility.ToJson(persistentData);
            var filePath = GetFilePath();
            using (var sw = new StreamWriter(filePath))
            {
                sw.Write(json);
                sw.Flush();
            }
        }

        private void OnDisable()
        {
            Save();
        }

        private static string GetFilePath()
        {
            return Path.Combine(Application.persistentDataPath, path2: "Save.json");
        }
    }
}