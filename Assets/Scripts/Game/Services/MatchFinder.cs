using System.Collections.Generic;
using Game.Core.Item;

namespace Game.Services
{
    public class MatchFinder
    {
        public HashSet<Cell> GetMatchedCells(Cell cell, MatchType matchType)
        {
            var cells = new HashSet<Cell>();
            
            GetMatchedCells(cell, matchType, cells);

            return cells;
        }
        
        
        private void GetMatchedCells(Cell cell, MatchType matchType, HashSet<Cell> resultCells)
        {
            if (cell == null) return;
            if (resultCells.Contains(cell)) return;

            if (!cell.HasItem()
                || cell.ItemBase.GetMatchType() != matchType
                || cell.ItemBase.GetMatchType() == MatchType.None
                || !cell.ItemBase.IsAvailableForMatch) return;
            
            resultCells.Add(cell);
			
            foreach (var neighbour in cell.Neighbours.Values)
            {
                GetMatchedCells(neighbour, matchType, resultCells);
            }
        }
    }
}