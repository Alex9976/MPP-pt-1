using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager
{
    internal class Parallel
    {
        static int runningCount;
        static object sync = new object();

        public static void WaitAll(Action[] delegates)
        {
            runningCount = delegates.Length;
            foreach (Action action in delegates)
                ThreadPool.QueueUserWorkItem(Execute, action);
            lock (sync)
                if (runningCount > 0)
                    Monitor.Wait(sync);
        }

        private static void Execute(object state)
        {
            var action = (Action)state;
            action();
            lock (sync)
            {
                runningCount--;
                if (runningCount == 0)
                    Monitor.Pulse(sync);
            }
        }

    }
}
