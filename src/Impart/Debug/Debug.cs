using System.IO;
using System;

namespace Impart
{
    public static class Debug
    {
        internal static event Action<Log> ObjectEvent;
        internal static bool FileLogging;
        internal static bool Formatting;
        private static string tempCache = "";
        internal static bool debug = true;
        public static readonly (int, int, int) Version = (1, 1, 3);
        internal static void CallObjectEvent(string log)
        {
            if (!debug)
            {
                return;
            }
            if (FileLogging)
            {
                if (!Common.ValidPath("DEBUG.txt", ""))
                {
                    File.Create("DEBUG.txt");
                }
                if (tempCache == "")
                {
                    tempCache = $"-- DEBUG FILE --{Environment.NewLine}{log} [{Timer.GetTime()}ms]";
                    Common.Write("DEBUG.txt", tempCache);
                    return;
                }
                else
                {
                    tempCache += $"{Environment.NewLine}{log} [{Timer.GetTime()}ms]";
                    Common.Write("DEBUG.txt", tempCache);
                    return;
                }
            }
            ObjectEvent?.Invoke(new Log(log, Timer.GetTime()));
        }
    }
}