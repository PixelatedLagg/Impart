using System.IO;
using System.Text;

namespace Impart.Format
{
    public static class ParseExtensions //TODO actually finish JSON parsing
    {
        public static JsonObject ParseIntoJson(this string source)
        {
            JsonObject result = new JsonObject();
            StringBuilder token = new StringBuilder();
            StringBuilder send = new StringBuilder();
            bool quote = false;
            bool array = false;
            int i = 0;
            while (i < source.Length)
            {
                switch (source[i])
                {
                    case ' ':
                        if (quote)
                        {
                            token.Append(' ');
                        }
                        break;
                    case '{':
                        if (quote)
                        {
                            token.Append('{');
                            break;
                        }
                        break;
                    case '"':
                        quote ^= true;
                        if (!quote)
                        {
                            
                        }
                        break;
                    case '}':
                        if (quote)
                        {
                            token.Append('}');
                            break;
                        }
                        break;
                    case '[':
                        if (quote)
                        {
                            token.Append('[');
                            break;
                        }
                        array ^= true;
                        break;
                    case ']':
                        if (quote)
                        {
                            token.Append(']');
                            break;
                        }
                        array = false;
                        break;
                    default:
                        if (array)
                        {

                        }
                        if (quote)
                        {
                            token.Append(source[i]);
                        }
                        break;
                }
                i++;
            }
            return result;
        }
    }
}