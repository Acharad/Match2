using System.Collections.Generic;

namespace Game.Services.Lock
{
    public class ServiceLock
    {
        private static readonly HashSet<LockType> _locks = new();

        public static void Add(LockType gameLockType)
        {
            _locks.Add(gameLockType);
        }

        public static void Remove(LockType gameLockType)
        {
            _locks.Remove(gameLockType);
        }

        public static bool Contains(LockType gameLockType)
        {
            return _locks.Contains(gameLockType);
        }
    }
}