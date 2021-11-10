using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EventManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Action[] delegates = new Action[] { Action, Action, Action, Action, Action, Action, Action };
            Parallel.WaitAll(delegates);
            Console.WriteLine("Done");
        }

        public static void Action()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"The thread is {threadId} running now");
            Thread.Sleep(1000);
        }
    }
}