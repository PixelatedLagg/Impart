using System;

namespace Impart
{
    /// <summary>Track a difference in time.</summary>
    public static class Timer
    {
        /// <summary>Called when the timer is stopped.</summary>
        public static event Action<Log> Event;

        private static DateTime Timestamp;
        private static DateTime PauseTimestamp;

        /// <summary>Start tracking time.</summary>
        public static void Start()
        {
            Timestamp = DateTime.Now;
        }

        /// <summary>Temporarily pause tracking time.</summary>
        public static void Pause()
        {
            PauseTimestamp = DateTime.Now;
        }

        /// <summary>Resume tracking time.</summary>
        public static void Resume()
        {
            Timestamp = new DateTime(Timestamp.Ticks + (DateTime.Now.Ticks -  PauseTimestamp.Ticks));
        }

        /// <summary>Call the log event with a log message.</summary>
        /// <param name="message">The log message.</param>
        public static void Log(string message = "No Event Provided")
        {
            Event?.Invoke(new Log(message, (DateTime.Now - Timestamp).TotalMilliseconds));
        }
    }
}