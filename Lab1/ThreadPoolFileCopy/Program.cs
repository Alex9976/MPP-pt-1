// See https://aka.ms/new-console-template for more information

using Lab1;

class Program
{
    static void Main(string[] args)
    {
        string Source = "", Dest = "";
        int NumberOfThreads = 1;

        if (args.Length != 3)
        {
            Console.WriteLine("Source path");
            Source = Console.ReadLine();
            Console.WriteLine("Dest. path");
            Dest = Console.ReadLine();
            Console.WriteLine("Number of threads");
            NumberOfThreads = Convert.ToInt32(Console.ReadLine());
        }
        else
        {
            Source = args[0];
            Dest = args[1];
            NumberOfThreads = Convert.ToInt32(args[2]);
        }

        TaskQueue TaskQueue = new TaskQueue(Dest, NumberOfThreads);

        string[] files;
        files = Directory.GetFiles(Source);

        foreach (var file in files)
        {
            TaskQueue.EnqueueTask(file);
        }

        while (TaskQueue.taskQueue.Count > 0) { }

        TaskQueue.AbortAllTreads();

        while (TaskQueue.CheckWorkThreads()) { }
        
        Console.WriteLine($"{TaskQueue.copiedFiles} files copied");
    }
}