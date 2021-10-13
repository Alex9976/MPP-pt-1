using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser
{
    internal class Program
    {
        private static SortedDictionary<string, MemberInfo> members = new SortedDictionary<string, MemberInfo>();

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
                Console.WriteLine($"\t{type.FullName}");

                foreach (MethodBase method in type.GetMethods())
                {
                    if (method.IsPublic)
                    {
                        members.Add(method.Name, method);
                        Console.WriteLine($"{method}");
                    }
                }
                foreach (FieldInfo feild in type.GetFields())
                {
                    if (feild.IsPublic)
                    {
                        members.Add(feild.Name, feild);
                        Console.WriteLine($"{feild}");
                    }
                }
                foreach (PropertyInfo property in type.GetProperties())
                {
                    members.Add(property.Name, property);
                    Console.WriteLine($"{property}");
                }
                
            }

            Console.WriteLine("\n\tSort by name:");

            foreach (KeyValuePair<string, MemberInfo> member in members)
            {
                Console.WriteLine($"{member.Value.DeclaringType.FullName} {member.Value}");
            }

        }
    }
}
