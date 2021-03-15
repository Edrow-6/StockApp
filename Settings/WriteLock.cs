using System;
using System.Threading;

namespace StockApp.Settings
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