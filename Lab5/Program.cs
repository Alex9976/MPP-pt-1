using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    public class Program
    {
        public static DynamicList<int> list = new DynamicList<int>();
        private static DynamicList<string> dynamicList = new DynamicList<string>(4) { "This ", "is ", "dynamic ", "list"};
        public static void Main(string[] args)
        {
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                list.Add(random.Next(100));
            }
            Console.WriteLine($"Count = {list.Count}");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();
            list.Remove(list[19]);
            list.RemoveAt(0);
            Console.WriteLine($"New count = {list.Count}");
            Console.WriteLine(list.ToString());

            foreach (string item in dynamicList)
            {
                Console.Write(item);
            }

        }
    }
}