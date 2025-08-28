using System.Collections.Generic;
using Game.Core.Item;

namespace Game.Services
{
    public class MatchFinder
    {
        public HashSet<Cell> GetMatchedCells(Cell cell, ItemColor color)
        {
            var cells = new HashSet<Cell>();
            
            GetMatchedCells(cell, color, cells);

            return cells;
        }
        
        
        private void GetMatchedCells(Cell cell, ItemColor color, HashSet<Cell> resultCells)
        {
            if (cell == null) return;
            if (resultCells.Contains(cell)) return;

            if (!cell.HasItem()
                || cell.ItemBase.GetItemColor() != color
                || cell.ItemBase.GetItemColor() == ItemColor.None
                || !cell.ItemBase.IsAvailableForMatch) return;
            
            resultCells.Add(cell);
			
            foreach (var (direction, neighbour) in cell.Neighbours)
            {
                GetMatchedCells(neighbour, color, resultCells);
            }
        }
    }
}