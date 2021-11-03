using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportClass : Attribute
    {
    }
}
