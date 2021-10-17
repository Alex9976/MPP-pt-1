using System;
using System.Collections.Generic;
using System.Reflection;

namespace LogBuffer
{
    internal class Program
    {
        public static LogBuffer logBuffer = new LogBuffer();

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(WriteInfo);
                thread.Name = i.ToString();
                thread.IsBackground = true;
                thread.Start();
            }
            Thread.Sleep(5000);
        }

        public static void WriteInfo()
        {
            while (Thread.CurrentThread.IsBackground)
            {
                String message = $"Thread {Thread.CurrentThread.Name} write string on {DateTime.Now}";
                Console.WriteLine(message);
                logBuffer.Add(message);                
                Thread.Sleep(200);
            }
        }
    }
}
