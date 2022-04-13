using System.IO;
using System.Text;

namespace Impart.Format
{
    public static class JsonParser
    {
        public static void ParseFile(string file) => Parse(File.ReadAllText(file));
        public static void Parse(string source)
        {
            StringBuilder cache = new StringBuilder();
            JsonObject result = new JsonObject();
            int i = 0;
            char c;
            bool reading = false;
            string key = null;

            while (i < source.Length)
            {
                c = source[i];
                if (c == '"')
                {
                    if (reading)
                    {
                        if (key == null)
                        {

                        }
                        reading = false;
                    }
                    else
                    {
                        reading = true;
                    }
                }
                if (c == '{')
                {

                }
                i++;
            }
        }
    }
}