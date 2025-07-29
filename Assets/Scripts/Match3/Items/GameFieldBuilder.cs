using System;
using UnityEngine;

namespace Match3.Items
{
    public class GameFieldBuilder : MonoBehaviour
    {
        public LevelData _levelData;
        private void Start()
        {
            var loader = new LevelLoader();
            _levelData = loader.LoadLevelData(1);

            PrintGrid(_levelData);
        }
        
        public static void PrintGrid(LevelData data)
        {
            for (int row = 0; row < data.Rows; row++)
            {
                string line = "";
                for (int col = 0; col < data.Cols; col++)
                {
                    line += data.GetItem(row, col) + " ";
                }
                Debug.Log(line);
            }
        }

    }
}