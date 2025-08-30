using System.Collections.Generic;
using Game.Items;

namespace Game.Services
{
    public class MatchHandler
    {
        private MatchFinder _matchFinder;
        private int _minMatchCount;

        public void Init(MatchFinder matchFinder, int minMatchCount)
        {
            _matchFinder = matchFinder;
            
            _minMatchCount = minMatchCount;
        }
        
        public void CellTapped(Cell cell)
        {
            if (cell == null) return;

            if (!cell.HasItem() || !cell.ItemBase.CanExplodeWithTab) return;

            if (cell.ItemBase is BoosterItem)
                HitSpecialMatchingCells(cell);
            else
                HitMatchingCells(cell);
        }
        
        private void HitMatchingCells(Cell cell)
        {
            var matchType = cell.ItemBase.GetMatchType();

            var hitGroup = _matchFinder.GetMatchedCells(cell, matchType);
            
            if (hitGroup.Count < _minMatchCount) return;
            
            foreach (var c in hitGroup)
            {
                c.ItemBase.TryExecute();
            }

            HitNeighboursOfCellGroup(hitGroup);
        }

        private void HitSpecialMatchingCells(Cell cell)
        {
            var matchType = cell.ItemBase.GetMatchType();

            var hitGroup = _matchFinder.GetMatchedCells(cell, matchType);
            
            if (hitGroup.Count < 2)
            {
                cell.ItemBase.TryExecute();
            }
        }

        private void HitNeighboursOfCellGroup(HashSet<Cell> hitGroup)
        {
            GetNeighbours(hitGroup, out var neighbours);

            foreach (var cell in neighbours)
            {
                if (cell.HasItem())
                {
                    cell.ItemBase.TryExecuteByNearMatch();
                }
            }
        }

        private void GetNeighbours(HashSet<Cell> hitGroup, out HashSet<Cell> neighbours)
        {
            neighbours = new HashSet<Cell>(); 

            foreach (var cell in hitGroup)
            {
                foreach (var (direction, neighbour) in cell.Neighbours)
                {
                    if (!hitGroup.Contains(neighbour))
                    {
                        neighbours.Add(neighbour);
                    }
                }
            }
        }
    }
}