using Game.Core.Board;
using Game.Services.Lock;

namespace Game.Services
{
    public class GravityHandler
    {
        private Board _board;
        private bool _isFallEnabled = false;


        public void Init(Board board)
        {
            _board = board;
            SetEnabled(true);
        }

        public void SetEnabled(bool value)
        {
            _isFallEnabled = value;
        }

        public void ApplyGravity()
        {
            if (!_isFallEnabled) return;
            if (ServiceLock.Contains(LockType.Shuffle)) return;
            
            for (var y = 0; y < _board.Rows; y++)
            {
                for (var x = 0; x < _board.Cols; x++)
                {
                    var cell = _board.Cells[x, y];
                    if (cell.ItemBase != null && cell.CellBelow != null && cell.CellBelow.ItemBase == null)
                    {
                        cell.ItemBase.TryFall();
                    }
                }
            }
        }
    }
}