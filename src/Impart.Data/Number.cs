using System;
using System.Text;

namespace Impart.Data
{
    /// <summary>Randomly generate a number.</summary>
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
        private static long Long()
        {
            _last = ((a * Seed()) + c) % m;
            return _last;
        }

        /// <summary>Generate a number in a specific range.</summary>
        /// <returns>A randomly generated long between a range.</returns>
        /// <param name="min">The minimum number to generate.</param>
        /// <param name="max">The maximum number to generate.</param>
        public static long Range(long min, long max)
        {
            return Long() % (max - min) + min;
        }

        /// <summary>Generate a number with the specified length.</summary>
        /// <returns>A randomly generated long with the specified length.</returns>
        /// <param name="length">The length of the number to generate.</param>
        public static long Length(int length)
        {
            StringBuilder result = new StringBuilder(length);
            while (length > 0)
            {
                length--;
                result.Append(Data.Number.Range(0, 9));
            }
            return Convert.ToInt64(result.ToString());
        }
    }
}