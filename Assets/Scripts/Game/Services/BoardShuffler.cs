using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Board;
using Game.Core.Item;
using Game.Core;
using Game.Items.Components;
using Game.Services.Lock;
using Game.Services.StaticExtensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Services
{
    public class BoardShuffler : IDisposable
    {
        private Board _board;
        private DeadlockDetector _deadlockDetector;
        
        private bool _shuffleStarted;
        private int _minMatchCount;

        private List<Direction> _neighboursDirections =
            new() { Direction.Up, Direction.Down, Direction.Left, Direction.Right };


        public void Init(Board board, DeadlockDetector deadlockDetector, int minMatchCount)
        {
            _board = board;

            _minMatchCount = minMatchCount;

            _deadlockDetector = deadlockDetector;

            _deadlockDetector.OnDeadlockDetected += TryShuffle;
        }
        
        
        private void TryShuffle()
        {
            ServiceLock.Add(LockType.Shuffle);
            _shuffleStarted = true;
            
            var cellList = new List<Cell>();
            var cubeDictionary = new Dictionary<ItemColor, List<ItemBase>>();

            for (var y = 0; y < _board.Rows; y++)
            {
                for (var x = 0; x < _board.Cols; x++)
                {
                    if (!_board.Cells[x, y].HasItem()) continue;
                    if (!_board.Cells[x, y].ItemBase.GetItemComponent<IItemShuffleComponent>().CanShuffle()) continue;
                    cellList.Add(_board.Cells[x, y]);
                    AddToCubeDictionary(cubeDictionary, _board.Cells[x, y].ItemBase);
                }
            }
            
            cellList.Shuffle();

            var possibleMatchCellTuple = GetMinMatchCell(cellList);

            if (possibleMatchCellTuple == null)
            {
                Debug.LogError("There are no possible match cells");
                return;
            }

            var colorsForPossibleMatches = new List<ItemColor>();

            foreach (var itemColor in cubeDictionary.Keys)
            {
                if(cubeDictionary[itemColor].Count >= _minMatchCount)
                    colorsForPossibleMatches.Add(itemColor);
            }

            if (colorsForPossibleMatches.Count == 0)
            {
                Debug.LogError("There are no enough item to possible match");
                return;
            }

            AnimateShuffle(cellList, cubeDictionary, colorsForPossibleMatches, possibleMatchCellTuple);

            ServiceLock.Remove(LockType.Shuffle);
            _shuffleStarted = false;
        }

        private void AnimateShuffle(List<Cell> cellList, Dictionary<ItemColor, List<ItemBase>> cubeDictionary, 
                                    List<ItemColor> colorsForPossibleMatches, Tuple<Cell,Cell> possibleMatchCellTuple)
        {
            var randomPossibleMatchColor =
                colorsForPossibleMatches[Random.Range(0, colorsForPossibleMatches.Count)];

            var coloredCubes = cubeDictionary[randomPossibleMatchColor];

            possibleMatchCellTuple.Item1.ItemBase = coloredCubes[0];
            coloredCubes[0].transform.position = possibleMatchCellTuple.Item1.transform.position;

            coloredCubes.RemoveAt(0);
            cellList.Remove(possibleMatchCellTuple.Item1);

            possibleMatchCellTuple.Item2.ItemBase = coloredCubes[0];
            coloredCubes[0].transform.position = possibleMatchCellTuple.Item2.transform.position;
            
            coloredCubes.RemoveAt(0);
            cellList.Remove(possibleMatchCellTuple.Item2);

            var cellIndex = 0;
            foreach (var cubes in cubeDictionary.Values)
            {
                for (var i = 0; i < cubes.Count; i++)
                {
                    var cell = cellList[cellIndex++];
                    cell.ItemBase = cubes[i];
                    
                    cubes[i].transform.position = cell.transform.position;
                }
            }
        }


        private void AddToCubeDictionary(Dictionary<ItemColor, List<ItemBase>> dictionary, ItemBase itemBase)
        {
            if (dictionary.ContainsKey(itemBase.GetItemColor()))
            {
                dictionary[itemBase.GetItemColor()].Add(itemBase);
            }
            else
            {
                dictionary.Add(itemBase.GetItemColor(), new List<ItemBase> { itemBase });
            }
        }
        
        private Tuple<Cell, Cell> GetMinMatchCell(List<Cell> cellList)
        {
            _neighboursDirections.Shuffle();
            for (var i = 0; i < cellList.Count; i++)
            {
                var cell = cellList[i];

                foreach (var direction in _neighboursDirections)
                {
                    if (!cell.Neighbours.TryGetValue(direction, out var neighbour)) continue;
                    
                    if (neighbour.ItemBase.GetItemComponent<IItemShuffleComponent>().CanShuffle())
                    {
                        return new Tuple<Cell, Cell>(cell, neighbour);
                    }
                }
            }

            return null;
        }


        public void Dispose()
        {
            _deadlockDetector.OnDeadlockDetected -= TryShuffle;
        }
    }
}

