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
        private static List<bool> inWorkThreads = new List<bool>();
        public ConcurrentQueue<string> taskQueue = new ConcurrentQueue<string>();

        public delegate void TaskDelegate(string param1, string param2, string param3);

        public string dest;
        public static int copiedFiles = 0;

        public TaskQueue(string dest, int threadsCount)
        {
            this.dest = dest;
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

        public void EnqueueTask(string file)
        {
            taskQueue.Enqueue(file);
        }

        public void AbortAllTreads()
        {
            foreach(Thread thread in pool)
            {
                thread.IsBackground = false;
            }
        }

        public bool CheckWorkThreads()
        {
            return inWorkThreads.Contains(true);
        }

        private void ProcessTask()
        {
            inWorkThreads[Convert.ToInt32(Thread.CurrentThread.Name)] = true;
            while (Thread.CurrentThread.IsBackground)
            {
                if (taskQueue.TryDequeue(out string file) && file != null)
                {     
                    Copy(file, dest);     
                }
            }
            inWorkThreads[Convert.ToInt32(Thread.CurrentThread.Name)] = false;
        }

        private void Copy(string file, string dest)
        {
            Console.WriteLine($"Copy file {file} to {dest}");
            FileInfo fileInfo= new FileInfo(file);
            fileInfo.CopyTo(Path.Combine(dest, fileInfo.Name), true);
            copiedFiles++;
        }
    }
}
