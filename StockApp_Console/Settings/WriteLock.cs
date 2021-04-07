using System;
using System.Threading;

namespace StockApp_Console.Settings
{
    public struct WriteLock : IDisposable
    {
        private bool lockHeld;
        private ReaderWriterLockSlim rwLock;

        public WriteLock(ReaderWriterLockSlim rwLock)
        {
            this.rwLock = rwLock;
            rwLock.EnterWriteLock();
            lockHeld = true;
        }

        public void Dispose()
        {
            if (lockHeld)
            {
                rwLock.ExitWriteLock();
            }
        }
    }
}