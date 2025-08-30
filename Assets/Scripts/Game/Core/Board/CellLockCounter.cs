namespace Game.Core.Board
{
    public struct CellLockCounter
    {
        private int _lockCount;

        public void AddLock()
        {
            _lockCount++;
        }

        public bool IsLocked()
        {
            return _lockCount > 0;
        }
    }
}