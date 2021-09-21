using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Skipy.Infrastructure
{
    public static class AssemblyHelper
    {
        /// <summary>
        /// Loads an assembly with a dll path.
        /// </summary>
        /// <param name="dllPath">Dll path.</param>
        /// <returns></returns>
        public static Assembly LoadAssembly(string dllPath)
        {
            try
            {
                return AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
            }
            catch
            {
                throw new InvalidOperationException($"Assembly cannot be loaded : {dllPath}");
            }
        }
    }
}
