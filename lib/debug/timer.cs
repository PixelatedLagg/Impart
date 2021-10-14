using System.Diagnostics;

namespace Csweb
{
    internal static class Timer
    {
        private static Stopwatch time;
        internal static void StartTimer()
        {
            time = new Stopwatch();
            time.Start();
        }
        internal static double GetTime()
        {
            double result = (double)time.Elapsed.TotalMilliseconds;
            time.Stop();
            time.Reset();
            return result;
        }
    }
}