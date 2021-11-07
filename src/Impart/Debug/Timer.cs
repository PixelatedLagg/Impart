using System;

namespace Impart
{
    internal static class Timer
    {
        private static DateTime timer;
        internal static void StartTimer()
        {
            if (!Debug.debug)
            {
                return;
            }
            timer = DateTime.Now;
        }
        internal static double GetTime()
        {
            return (DateTime.Now - timer).TotalMilliseconds;
        }
    }
}