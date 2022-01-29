using System;

namespace Impart
{
    public static class Logger
    {
        private static DateTime timer;
        public static event Action<Log> ObjectEvent;
        public static void Start()
        {
            timer = DateTime.Now;
        }
        public static void End(string log = "No Event Provided")
        {
            ObjectEvent?.Invoke(new Log(log, (DateTime.Now - timer).TotalMilliseconds));
        }
    }
}