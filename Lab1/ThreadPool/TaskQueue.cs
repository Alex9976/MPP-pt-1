using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class TaskQueue
    {
        private List<Thread> pool = new List<Thread>();
        public ConcurrentQueue<TaskDelegate> taskQueue = new ConcurrentQueue<TaskDelegate>();

        public delegate void TaskDelegate();

        public TaskQueue(int threadsCount)
        {
            for (int i = 0; i < threadsCount; i++)
            {
                Thread thread = new Thread(ProcessTask);
                thread.Name = (i + 1).ToString();
                thread.IsBackground = true;
                pool.Add(thread);
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
                    task();
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }
    }
}
