using System;
using MInject.Runtime;
using UnityEngine;

namespace Match3.Items
{
    public class GameFieldBuilder : MonoBehaviour
    {
        public LevelData _levelData;
        [SerializeField] private Board board;
        private void Start()
        {
            var loader = new LevelLoader();
            _levelData = loader.LoadLevelData(1);

            PrintGrid(_levelData);
            
            board.Prepare(_levelData);
        }
        
        private static void PrintGrid(LevelData data)
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

        [Inject]
        public void Construct()
        {
            Debug.Log("ahmet deneme");
        }

    }
}