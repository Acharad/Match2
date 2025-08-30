using System.Collections.Generic;
using Game.Core.Board;
using Game.Core.Item;

namespace Game.Items.Components.ExplodeComponent
{
    public class ItemSquareExplodeComponent : ItemExplodeComponent
    {
        private readonly int _explosionRange = 0;

        public ItemSquareExplodeComponent(int explosionRange, Board board, int x, int y) : base(board, x, y)
        {
            _explosionRange = explosionRange;
        }
        
        public override HashSet<Cell> GetExplodingCells()
        {

            for (int i = -_explosionRange; i <= _explosionRange; i++)
            {
                for (int j = -_explosionRange; j <= _explosionRange; j++)
                {
                    if (!(X + i >= Board.Rows || X + i < 0 || Y + j >= Board.Cols || Y + j < 0))
                    {
                        if (i == 0 && j == 0) 
                            continue;
                        ExplodingCells.Add(Board.Cells[X + i, Y + j]);
                    }
                }
            }

            return ExplodingCells;
        }

        
    }
}