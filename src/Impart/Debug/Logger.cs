using System;

namespace Impart
{
    /// <summary>The class for logging time in Impart.</summary>
    public static class Logger
    {
        /// <value>The method to be called when Logger ends.</value>
        public static event Action<Log> Event;
        private static DateTime Timer;

        /// <summary>Start tracking time with Logger.</summary>
        public static void Start()
        {
            Timer = DateTime.Now;
        }

        /// <summary>Stop tracking time with <paramref name="message"/> as the log message.</summary>
        /// <param name="message">The log message.</param>
        public static void End(string message = "No Event Provided")
        {
            Event?.Invoke(new Log(message, (DateTime.Now - Timer).TotalMilliseconds));
        }
    }
}