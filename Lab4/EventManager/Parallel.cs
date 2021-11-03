using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager
{
    internal class Parallel
    {     
        private static TaskQueue _queue = new TaskQueue(3);

        ~Parallel()
        {
            _queue.AbortAllTreads();
        }

        public static void WaitAll(TaskQueue.TaskDelegate[] delegates)
        {
            foreach (TaskQueue.TaskDelegate d in delegates)
            {      
                _queue.EnqueueTask(d);
            }
            _queue.Wait();
        }
    }
}
