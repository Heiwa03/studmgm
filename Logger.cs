using System;
using System.IO;

namespace UniversityManagement
{
    public class Logger
    {
        private readonly string logFilePath;

        public Logger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public void LogError(string message)
        {
            Log("ERROR", message);
        }

        private void Log(string logLevel, string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
    }
}