using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TaskQueue.TaskDelegate[] delegates = new TaskQueue.TaskDelegate[] { Action, Action, Action, Action, Action, Action, Action };
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