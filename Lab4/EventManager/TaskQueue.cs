using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager
{
    public class TaskQueue
    {
        private List<Thread> pool = new List<Thread>();
        private static List<bool> inWorkThreads = new List<bool>();
        public ConcurrentQueue<TaskDelegate> taskQueue = new ConcurrentQueue<TaskDelegate>();

        public delegate void TaskDelegate();

        public TaskQueue(int threadsCount)
        {
            for (int i = 0; i < threadsCount; i++)
            {
                Thread thread = new Thread(ProcessTask);
                thread.Name = i.ToString();
                thread.IsBackground = true;
                pool.Add(thread);
                inWorkThreads.Add(false);
                thread.Start();
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            taskQueue.Enqueue(task);
        }

        public void AbortAllTreads()
        {
            foreach(Thread thread in pool)
            {
                thread.IsBackground = false;
            }
        }

        private void ProcessTask()
        {
            while (Thread.CurrentThread.IsBackground)
            {
                if (taskQueue.TryDequeue(out TaskDelegate task) && task != null)
                {
                    inWorkThreads[Convert.ToInt32(Thread.CurrentThread.Name)] = true;
                    task();
                    inWorkThreads[Convert.ToInt32(Thread.CurrentThread.Name)] = false;
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }

        public void Wait()
        {
            bool b = true;
            while (b)
            {
                Thread.Sleep(50);
                if (taskQueue.Count == 0)
                {
                    bool workThreads = false;
                    for (int i = 0; i < inWorkThreads.Count; i++)
                    {
                        workThreads |= inWorkThreads[i];
                    }
                    b = workThreads;
                }
            }
        }
    }
}
