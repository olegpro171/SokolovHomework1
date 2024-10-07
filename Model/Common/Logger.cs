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
        private const string FilenameExtension = "csv";
        private const string FilenameDirectory = "logs/";

        private static readonly string SessionLogFilename;
        
        private static StringBuilder AllLogsSB = new();
        private static StringBuilder CSVLogsSB = new();

        //private static 

        static Logger()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            static void OnProcessExit(object? sender, EventArgs e) => WriteFileBuffer();

            SessionLogFilename = "SessionLogFile.csv";
            File.Delete(SessionLogFilename);
            // SessionLogFilename = GetSessionFilename();
        }

        private static string GetSessionFilename()
        {
            return $"{FilenameDirectory}{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.{FilenameExtension}";
        }


        public static void Log(string message, bool noPrefix = false)
        {
            var methodInfo = new StackTrace().GetFrame(1)?.GetMethod();
            var callerClassName = methodInfo?.ReflectedType?.Name;

            if (noPrefix)
                SendLogMsg(message);
            else    
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

        private static void SendLogMsg(string LogMsg, bool writeOnConsole = false)
        {
            AllLogsSB.AppendLine(LogMsg);
            if (writeOnConsole)
                Console.WriteLine(LogMsg);
        }
    }
}
