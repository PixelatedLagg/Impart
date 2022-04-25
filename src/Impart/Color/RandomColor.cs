using System;
using System.Linq;

namespace Impart
{
    /// <summary>The class for generating random colors.</summary>
    public static class RandomColor
    {
        private static Random rnd = new Random();
        private const string chars = "ABCDEF1234567890";

        /// <summary>Generates a random Rgb value.</summary>
        public static Rgb Rgb()
        {
            return new Rgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }

        /// <summary>Generates a random Rgb value array with <paramref name="number"/> as the number of elements in the array.</summary>
        /// <param name="number">The number of elements in the array.</param>
        public static Rgb[] Rgbs(int number)
        {
            Rgb[] result = new Rgb[number];
            for (int x = 0; x < number; x++)
            {
                result[x] = new Rgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            }
            return result;
        }

        /// <summary>Generates a random Hsl value.</summary>
        public static Hsl Hsl()
        {
            return new Hsl(rnd.Next(0, 361), rnd.Next(0, 101), rnd.Next(0, 101));
        }

        /// <summary>Generates a random Hsl value array with <paramref name="number"/> as the number of elements in the array.</summary>
        /// <param name="number">The number of elements in the array.</param>
        public static Hsl[] Hsls(int number)
        {
            Hsl[] result = new Hsl[number];
            for (int x = 0; x < number; x++)
            {
                result[x] = new Hsl(rnd.Next(0, 361), rnd.Next(0, 101), rnd.Next(0, 101));
            }
            return result;
        }

        /// <summary>Generates a random Hex value.</summary>
        public static Hex Hex()
        {
            return new Hex(new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray()));
        }

        /// <summary>Generates a random Hex value array with <paramref name="number"/> as the number of elements in the array.</summary>
        /// <param name="number">The number of elements in the array.</param>
        public static Hex[] Hexes(int number)
        {
            Hex[] result = new Hex[number];
            for (int x = 0; x < number; x++)
            {
                result[x] = new Hex(new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray()));
            }
            return result;
        }
    }
}