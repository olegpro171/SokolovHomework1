using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    internal static class Logger
    {
        private const string FilenameExtension = "txt";
        private const string FilenameDirectory = "logs/";

        private static readonly string SessionLogFilename;
        
        private static StringBuilder AllLogsSB;

        //private static 

        static Logger()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            static void OnProcessExit(object? sender, EventArgs e) => WriteFileBuffer();

            AllLogsSB = new();
            SessionLogFilename = GetSessionFilename();
        }

        private static string GetSessionFilename()
        {
            return $"{FilenameDirectory}{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.{FilenameExtension}";
        }


        public static void Log(string message)
        {
            var methodInfo = new StackTrace().GetFrame(1)?.GetMethod();
            var callerClassName = methodInfo?.ReflectedType?.Name;

            SendLogMsg($"[{callerClassName}]: {message}");
        }

        public static void WriteFileBuffer()
        {
            if (!Directory.Exists(FilenameDirectory))
            {
                Directory.CreateDirectory(FilenameDirectory);
            }
            File.AppendAllText(SessionLogFilename, AllLogsSB.ToString());
            AllLogsSB.Clear();
        }

        private static void SendLogMsg(string LogMsg)
        {
            AllLogsSB.AppendLine(LogMsg);
            Console.WriteLine(LogMsg);
        }
    }
}
