namespace Lab1
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Number of threads");
            int NumberOfThreads = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Number of tasks");
            int NumberOfTasks = Convert.ToInt32(Console.ReadLine());

            TaskQueue TaskQueue = new TaskQueue(NumberOfThreads);

            for (int i = 0; i < NumberOfTasks; i++)
            {
                TaskQueue.EnqueueTask(PrintInfo);
            }

            string? Comand = "";
            while (Comand != "q")
            {
                Comand = Console.ReadLine();
            }

            TaskQueue.AbortAllTreads();
        }

        static void PrintInfo()
        {
            Console.WriteLine($"Thread No. {Thread.CurrentThread.Name} execute task");
            Thread.Sleep(100);
        }
    }
}