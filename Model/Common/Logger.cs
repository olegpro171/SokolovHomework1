using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    internal static class Logger
    {
        public static void Log(string message)
        {
            var methodInfo = new StackTrace().GetFrame(1)?.GetMethod();
            var callerClassName = methodInfo?.ReflectedType?.Name;
            Console.WriteLine($"[{callerClassName}]: {message}");
        }
    }
}
