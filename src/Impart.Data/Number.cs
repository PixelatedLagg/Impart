using System;

namespace Impart.Data
{
    public static class Number
    {
        private const long m = 4294967296;
        private const long a = 1664525;
        private const long c = 1013904223;
        private static long _last = 0;
        private static long Seed()
        {
            if (_last == 0)
            {
                _last = DateTime.Now.Ticks % m;
            }
            return _last;
        }
        public static long Long()
        {
            _last = ((a * Seed()) + c) % m;
            return _last;
        }
        public static long Range(long min, long max)
        {
            return Long() % (max - min) + min;
        }
    }
}