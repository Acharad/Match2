using System;
using UnityEngine;

namespace Game.Core.Level
{
    public class LevelLoaderResources : ILevelLoader
    {
        public LevelData LoadLevel(int levelNumber)
        {
            try
            {
                var levelFileName = $"Levels/Level{levelNumber}";
                var jsonFile = Resources.Load<TextAsset>(levelFileName);
                if (jsonFile != null)
                {
                    var levelData = JsonUtility.FromJson<LevelData>(jsonFile.text);
                    Debug.Log(
                        $"Level {levelNumber} yüklendi: Rows={levelData.Rows}, Cols={levelData.Cols}");
                    return levelData;
                }
                Debug.LogError($"Level dosyası bulunamadı: {levelFileName}");
                return null; 
                
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
