using System;
using System.Linq;

namespace Impart
{
    static public class RandomColor
    {
        static private Random rnd = new Random();
        private const string chars = "ABCDEF1234567890";
        static public Rgb Rgb()
        {
            return new Rgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }
        static public Rgb[] Rgbs(int number)
        {
            Rgb[] result = new Rgb[number];
            for (int x = 0; x < number; x++)
            {
                result[x] = new Rgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            }
            return result;
        }
        static public Hsl Hsl()
        {
            return new Hsl(rnd.Next(0, 361), rnd.Next(0, 101), rnd.Next(0, 101));
        }
        static public Hsl[] Hsls(int number)
        {
            Hsl[] result = new Hsl[number];
            for (int x = 0; x < number; x++)
            {
                result[x] = new Hsl(rnd.Next(0, 361), rnd.Next(0, 101), rnd.Next(0, 101));
            }
            return result;
        }
        static public Hex Hex()
        {
            return new Hex(new string(Enumerable.Repeat(chars, 6).Select(s => s[rnd.Next(s.Length)]).ToArray()));
        }
        static public Hex[] Hexes(int number)
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