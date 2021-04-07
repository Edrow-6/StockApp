using System;
using System.Threading;

namespace StockApp_Console.Settings
{
    public struct ReadLock : IDisposable
    {
        private bool lockHeld;
        private ReaderWriterLockSlim rwLock;

        public ReadLock(ReaderWriterLockSlim rwLock)
        {
            this.rwLock = rwLock;
            rwLock.EnterReadLock();
            lockHeld = true;
        }

        public void Dispose()
        {
            if (lockHeld)
            {
                rwLock.ExitReadLock();
            }
        }
    }
}