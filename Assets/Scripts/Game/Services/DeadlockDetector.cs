using System;
using System.Collections.Generic;
using Game.Core.Board;
using UnityEngine;

namespace Game.Services
{
    public class DeadlockDetector : IDisposable
    {
        private Board _board;
        private MatchFinder _matchFinder;
        private BoardStabilityMonitor _boardStabilityMonitor;
        
        private bool[,] _visitedCells;
        private bool _deadlockCheckEnabled;

        
        private bool _hasValidMatch = false;

        private int _minMatchCount;

        public event Action OnDeadlockDetected;

        public void Init(Board board, MatchFinder matchFinder, BoardStabilityMonitor boardStabilityMonitor, int minMatchCount)
        {
            _board = board;
            _matchFinder = matchFinder;

            _visitedCells = new bool[_board.Rows, _board.Cols];
            
            _boardStabilityMonitor = boardStabilityMonitor;

            _boardStabilityMonitor.OnStateChanged += HandleBoardStateChanged;

            _minMatchCount = minMatchCount;
            
            SetEnabled(true);
        }

        public void SetEnabled(bool value)
        {
            _deadlockCheckEnabled = value;
        }
        
        private void HandleBoardStateChanged(BoardState boardState)
        {
            if (boardState != BoardState.Stable) return;
            var hasDeadlock = CheckDeadlock();
            if (hasDeadlock)
                OnDeadlockDetected?.Invoke();

            Debug.Log("DeadlockDetector has deadlock " + hasDeadlock);
        }
        
        private bool CheckDeadlock()
        {
            if (!_deadlockCheckEnabled) return false;
            _hasValidMatch = false;
            
            ResetVisited();
            
            for (var i = 0; i < _board.Rows; i++)
            {
                for (var j = 0; j < _board.Cols; j++)
                {
                    if (!_visitedCells[i, j] && _board.Cells[i, j].HasItem())
                    {
                        var group = _matchFinder.GetMatchedCells(
                            _board.Cells[i, j],
                            _board.Cells[i, j].ItemBase.GetItemColor()
                        );
                        
                        if (group.Count >= _minMatchCount)
                        {
                            _hasValidMatch = true;
                            return false; 
                        }

                        MarkVisited(group);
                    }
                }
            }

            return !_hasValidMatch;
        }
        
        private void MarkVisited(HashSet<Cell> group)
        {
            foreach (var cell in group)
                _visitedCells[cell.X, cell.Y] = true;
        }

        private void ResetVisited()
        {
            for (var i = 0; i < _visitedCells.GetLength(0); i++)
            {
                for (var j = 0; j < _visitedCells.GetLength(1); j++)
                    _visitedCells[i, j] = false;
            }
        }

        public void Dispose()
        {
            _boardStabilityMonitor.OnStateChanged -= HandleBoardStateChanged;
        }
    }
}