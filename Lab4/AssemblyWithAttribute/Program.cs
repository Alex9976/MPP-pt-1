using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path;
            if (args.Length != 1)
            {
                Console.WriteLine("Patch:");
                path = Console.ReadLine();
            }
            else
            {
                path = args[0];
            }
            Assembly asm = Assembly.LoadFile(path);
            Type[] asmTypes = asm.GetTypes();
            foreach (Type type in asmTypes)
            {
                if (type.GetCustomAttributes(true).Any(x => x.GetType().Name == typeof(ExportClass).Name))
                {
                    Console.WriteLine($"\t{type.FullName}");

                    foreach (MethodBase method in type.GetMethods())
                    {
                        if (method.IsPublic)
                        {
                            Console.WriteLine($"{method}");
                        }
                    }
                    foreach (FieldInfo feild in type.GetFields())
                    {
                        if (feild.IsPublic)
                        {
                            Console.WriteLine($"{feild}");
                        }
                    }
                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        Console.WriteLine($"{property}");
                    }
                }
            }
        }
    }
}
