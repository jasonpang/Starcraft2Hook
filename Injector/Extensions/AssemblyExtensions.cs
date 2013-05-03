using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Injector.Extensions
{
    public static class AssemblyExtensions
    {
        public static List <string> GetAllAssembliesInCallingAssemblyPath (this Assembly assembly)
        {
            return Directory.GetFiles (Assembly.GetExecutingAssembly().Location, "*.dll")
                            .Select (Path.GetFileName)
                            .ToList();
        }
    }
}
