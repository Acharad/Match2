using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Board;
using Game.Core.Item;

namespace Game.Items.Components.ExplodeComponent
{
    public class ItemExplodeComponent : IItemExplodeComponent
    {
        protected readonly int X;
        protected readonly int Y;
        protected readonly Board Board;
        protected readonly HashSet<Cell> ExplodingCells = new();

        protected ItemExplodeComponent(Board board, int x, int y)
        {
            Board = board;
            X = x;
            Y = y;
        }
        
        public virtual HashSet<Cell> GetExplodingCells()
        {
             throw new ArgumentNullException(nameof(GetExplodingCells), 
                            "Exploding cells list is null.");
        }
        
        public void TryExplode()
        {
            var explodingCells = GetExplodingCells();

            foreach (var cell in explodingCells.Where(cell => cell.HasItem()))
            {
                cell.ItemBase.TryExecute();
            }
        }
    }
}