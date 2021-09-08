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
        public ConcurrentQueue<string> taskQueue = new ConcurrentQueue<string>();

        public delegate void TaskDelegate(string param1, string param2, string param3);

        public string source, dest;
        public int copiedFiles = 0;

        public TaskQueue(string dource, string dest, int threadsCount)
        {
            this.source = source;
            this.dest = dest;
            for (int i = 0; i < threadsCount; i++)
            {
                Thread thread = new Thread(ProcessTask);
                thread.Name = i.ToString();
                thread.IsBackground = true;
                pool.Add(thread);
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

        private void ProcessTask()
        {
            while (Thread.CurrentThread.IsBackground)
            {
                if (taskQueue.TryDequeue(out string file) && file != null)
                {
                    Copy(file, dest);     
                }
            }
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
