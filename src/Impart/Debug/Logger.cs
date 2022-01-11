using System.IO;
using System;
using System.Configuration;

namespace Impart
{
    public static class Logger
    {
        private static DateTime timer;
        public static event Action<Log> ObjectEvent;
        internal static bool FileLogging;
        private static string tempCache = "";
        public static void StartTimer()
        {
            timer = DateTime.Now;
        }
        public static void EndTimer(string log = null)
        {
            if (FileLogging)
            {
                if (!Common.ValidPath("DEBUG.txt", ""))
                {
                    File.Create("DEBUG.txt");
                }
                if (tempCache == "")
                {
                    tempCache = $"-- DEBUG FILE --{Environment.NewLine}{log ?? "No Event Provided"} [{(DateTime.Now - timer).TotalMilliseconds}ms]";
                    Common.Write("DEBUG.txt", tempCache);
                    return;
                }
                else
                {
                    tempCache += $"{Environment.NewLine}{log ?? "No Event Provided"} [{(DateTime.Now - timer).TotalMilliseconds}ms]";
                    Common.Write("DEBUG.txt", tempCache);
                    return;
                }
            }
            ObjectEvent?.Invoke(new Log(log ?? "No Event Provided", (DateTime.Now - timer).TotalMilliseconds));
        }
        public static void Initialize()
        {
            if (Directory.GetFiles(Environment.CurrentDirectory, "*.config").Length == 0)
            {
                using (StreamWriter writer = new StreamWriter("app.config"))
                {
                    writer.WriteLine(($"<?xml version=\"1.0\" encoding=\"utf-8\" ?>%<configuration>%    <appSettings>%        <add key=\"DebugFile\" value=\"false\" />%    </appSettings>%</configuration>").Replace("%", Environment.NewLine));
                    writer.Close();
                }
                FileLogging = false;
            }
            else 
            {
                try
                {
                    FileLogging = Convert.ToBoolean(ConfigurationManager.AppSettings["DebugFile"]);
                }
                catch
                {
                    throw new ConfigError("All key values must be \"true\" or \"false\"!");
                }
            }
        }
    }
}