using System;
using System.Text;

namespace Impart.Internal
{
    public static class NestParser
    {
        public static Nest Parse(string cache, ref int index)
        {
            return new Nest();
        }
        public static bool IsNest(string cache, int index)
        {
            do
            {
                index++;
            }
            while (cache[index] != '<');
            return cache[index + 1] != '/';
        }
    }
}