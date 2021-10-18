using System.Net;
using System.IO;
using System;

namespace Csweb
{
    public static class Debug
    {
        public static event Action<Log> ObjectEvent;
        internal static bool FileLogging;
        private static string tempCache = "";
        internal static void CallObjectEvent(Log log)
        {
            if (FileLogging)
            {
                if (!Common.ValidPath("DEBUG.txt", ""))
                {
                    File.Create("DEBUG.txt");
                }
                if (tempCache == "")
                {
                    tempCache = $"-- DEBUG FILE --{Environment.NewLine}{log.log} [{log.ms}ms]";
                    Common.Write("DEBUG.txt", tempCache);
                    return;
                }
                else
                {
                    tempCache = $"{tempCache}{Environment.NewLine}{log.log} [{log.ms}ms]";
                    Common.Write("DEBUG.txt", tempCache);
                    return;
                }
            }
            ObjectEvent?.Invoke(log);
        }
    }
}