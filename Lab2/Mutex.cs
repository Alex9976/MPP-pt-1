using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab2
{
    internal class Mutex
    {
        private const int UnlockedThreadID = -1;
        public int lockedThreadID = -1;
        private static int currentThreadID => Thread.CurrentThread.ManagedThreadId;

        public void Lock()
        {
            SpinWait spinWait = new SpinWait();
            while (Interlocked.CompareExchange(ref lockedThreadID, currentThreadID, UnlockedThreadID) != UnlockedThreadID)
            {
                spinWait.SpinOnce();
            }
        }

        public void Unlock()
        {
            if (Interlocked.CompareExchange(ref lockedThreadID, UnlockedThreadID, currentThreadID) != currentThreadID)
            {
                throw new Exception($"Thread {currentThreadID} can't unlock mutex");
            }
        }
    }
}
