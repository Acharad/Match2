using System;
using Game.Core.Level;
using Game.Services;
using UnityEngine;

namespace Game.Core.Board
{
    public class Board : MonoBehaviour
    {
        public int Rows = 9;
        public int Cols = 9;
        
        public Transform CellsParent;
        public Transform ItemsParent;
        
        [SerializeField] private Cell Cell;
        
        public Cell[,] Cells;

        
        public BoardState State { get; internal set; } = BoardState.Stable;
        public event Action<Board> OnBoardCreated;
        
        public void Init(LevelData levelData, ItemFactory itemFactory)
        {
            Rows = levelData.Rows;
            Cols = levelData.Cols;
			
            CreateCells();
            PrepareCells();
            PopulateItems(levelData, itemFactory);
            
            OnBoardCreated?.Invoke(this);
        }
		
        private void CreateCells()
        {
            Cells = new Cell[Rows, Cols];

            for (var y = 0; y < Rows; y++)
            {
                for (var x = 0; x < Cols; x++)
                {
                    var cell = Instantiate(Cell, Vector3.zero, Quaternion.identity, CellsParent);
                    Cells[x, y] = cell;
                }
            }
        }

        private void PrepareCells()
        {
            for (var y = 0; y < Rows; y++)
            {
                for (var x = 0; x < Cols; x++)
                {
                    Cells[x, y].Prepare(x, y, this);
                }
            }
        }
        
        
        private void PopulateItems(LevelData levelData, ItemFactory itemFactory)
        {
            for (var row = 0; row < levelData.Rows; row++)
            {
                for (var col = 0; col < levelData.Cols; col++)
                {
                    var cell = Cells[col, row];
                    var itemData = levelData.GetItem(row, col); 

                    var item = itemFactory.CreateItem(itemData, ItemsParent);
                    if (item == null) continue;					
					 					
                    cell.ItemBase = item;
                    item.transform.position = cell.transform.position;
                }
            }
        }
        
        public Cell GetNeighbourWithDirection(Cell cell, Direction direction)
        {
            var x = cell.X;
            var y = cell.Y;
            switch (direction)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    y += 1;
                    break;
                case Direction.UpRight:
                    y += 1;
                    x += 1;
                    break;
                case Direction.Right:
                    x += 1;
                    break;
                case Direction.DownRight:
                    y -= 1;
                    x += 1;
                    break;
                case Direction.Down:
                    y -= 1;
                    break;
                case Direction.DownLeft:
                    y -= 1;
                    x -= 1;
                    break;
                case Direction.Left:
                    x -= 1;
                    break;
                case Direction.UpLeft:
                    y += 1;
                    x -= 1;
                    break;
            }

            if (x >= Cols || x < 0 || y >= Rows || y < 0) return null;

            return Cells[x, y];
        }
    }
}
