using System.Collections.Generic;
using Game.Core.Board;
using Game.Services.Lock;

namespace Game.Services
{
    public class HintHighlighter
    {
        private Board _board;
        private MatchFinder _matchFinder;

        private bool[,] _visitedCells;

        private bool _hintEnable;

        private List<int> _hintThresholds;


        public void Init(Board board, MatchFinder matchFinder, List<int> hintThresholds)
        {
            _board = board;
            _matchFinder = matchFinder;

            _visitedCells = new bool[_board.Rows, _board.Cols];
            _hintThresholds = hintThresholds;
            SetEnabled(true);
        }

        public void SetEnabled(bool value)
        {
            _hintEnable = value;
        }

        public void ShowHints()
        {
            if (ServiceLock.Contains(LockType.Shuffle)) return;
            if (!_hintEnable) return;
            ResetVisited();
            for (var i = 0; i < _board.Rows; i++)
            {
                for (var j = 0; j < _board.Cols; j++)
                {
                    if (!_visitedCells[i, j] && _board.Cells[i, j].HasItem())
                    {
                        var group = _matchFinder.GetMatchedCells(_board.Cells[i, j],
                            _board.Cells[i, j].ItemBase.GetMatchType());

                        ShowGroupHint(group);
                        MarkVisited(group);
                    }
                }
            }
        }

        private void MarkVisited(HashSet<Cell> group)
        {
            foreach (var cell in group)
            {
                _visitedCells[cell.X, cell.Y] = true;
            }
        }

        private void ResetVisited()
        {
            for (var i = 0; i < _visitedCells.GetLength(0); i++)
            for (var j = 0; j < _visitedCells.GetLength(1); j++)
                _visitedCells[i, j] = false;
        }

        private void ShowGroupHint(HashSet<Cell> group)
        {
            foreach (var cell in group)
            {
                cell.ItemBase.ShowHint(group.Count, _hintThresholds);
            }
        }
    }
}