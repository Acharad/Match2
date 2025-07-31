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

            PrepareItems();
        }
        
        private void PrintGrid(LevelData data)
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
        
        private GameplayDataHolder _gameplayDataHolder;

        [Inject]
        public void Construct(GameplayDataHolder gameplayDataHolder)
        {
            _gameplayDataHolder = gameplayDataHolder;
        }

        private void PrepareItems()
        {
            //todo imran move to ItemFactory
            int cols = _levelData.Cols;

            for (int i = 0; i < _levelData.GridData.Length; i++)
            {
                int row = i / cols;
                int col = i % cols;

                var cell = board.Cells[col, row]; // [x, y] = [col, row]

                var itemType = _levelData.GridData[i];
                
                if (!_gameplayDataHolder.TryGetData(itemType, out ItemData data) || data == null) 
                    continue;
            }
        }
    }
}