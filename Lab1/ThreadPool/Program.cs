// See https://aka.ms/new-console-template for more information

using Lab1;

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

void PrintInfo()
{
    Console.WriteLine($"Thread No. {Thread.CurrentThread.Name} execute task");
}