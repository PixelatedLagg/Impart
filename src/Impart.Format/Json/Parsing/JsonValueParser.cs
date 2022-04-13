
using System.Text;

namespace Impart.Format
{
    internal static class JsonValueParser
    {
        internal static JsonValue Parse(string source, string key, ref int i)
        {
            int stack = 0;
            char c;
            while (i < source.Length)
            {
                c = source[i];
                if (c == '{')
                {
                    stack++;
                }
                i++;
            }
            return new JsonValue();
        }
    }
}