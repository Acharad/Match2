using System.Collections.Generic;
using Game.Core.Board;
using Game.Core.Item;
using Game.Services.Lock;
using UnityEngine;

namespace Game.Services
{
    public class BoardSpawner
    {
        private ItemFactory _itemFactory;
        
        private List<Cell> _generatorCells = new();

        private ItemData[] _generatorData;
        private Transform _itemsParent;

        private float _cellOffset;


        public void Init(Board board, ItemFactory itemFactory, ItemData[] generatorData)
        {
            _itemFactory = itemFactory;

            _generatorData = generatorData;

            _itemsParent = board.ItemsParent;
            
            GetGeneratorCells(board);
        }

        private void GetGeneratorCells(Board board)
        {
            for (var y = 0; y < board.Rows; y++)
            {
                for (var x = 0; x < board.Cols; x++)
                {
                    var cell = board.Cells[x, y];
                    if (cell != null && cell.IsGeneratorCell)
                    {
                        _generatorCells.Add(cell);
                    }
                }
            }
        }
        
        public void SpawnItems()
        {
            if (ServiceLock.Contains(LockType.Shuffle)) return;
            
            for (var i = 0; i < _generatorCells.Count; i++)
            {
                var cell = _generatorCells[i];
                if (cell.HasItem()) continue;
                
                var getRandomItemData = _generatorData[Random.Range(0, _generatorData.Length)];
                var itemBase = _itemFactory.CreateItem(getRandomItemData, _itemsParent);

                var offsetY = 0.0F;
                var targetCellBelow = cell.GetBottomAvailableCell().CellBelow;
                if (targetCellBelow != null)
                {
                    if (targetCellBelow.ItemBase != null)
                    {
                        offsetY = targetCellBelow.ItemBase.transform.position.y + 1;
                    }
                }

                var p = cell.transform.position;
                p.y += cell.CellOffset * 2; // 2 * 0.85 
                p.y = p.y > offsetY ? p.y : offsetY;

                itemBase.SetUp(cell, p);
                cell.ItemBase.TryFall();
            }
        }
    }
}