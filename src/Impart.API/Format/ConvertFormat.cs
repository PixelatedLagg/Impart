using System.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impart.API
{
    public static class ConvertFormat
    {
        public static Json ToJson(string file)
        {
            string contents = RemoveWhitespace(File.ReadAllText(file));
            Console.WriteLine(contents);
            StringBuilder title = new StringBuilder();
            int i = 0;
            bool read = false;
            while (i < contents.Length)
            {
                if (read)
                {
                    
                }
                switch (contents[i])
                {
                    case '"':
                        break;
                    case ':':
                        break;
                    case '{':
                        break;
                    case '}':
                        break;
                }
                i++;
            }
            Json json = new Json(title.ToString());
            return json;
        }
        public static Json ToJson(Xml xml)
        {
            return new Json();
        }
        public static Xml ToXml(string file)
        {
            return new Xml();
        }
        public static Xml ToXml(Json json)
        {
            return new Xml();
        }
        private static string RemoveWhitespace(string json)
        {
            var len = json.Length;
            char[] src = json.ToCharArray();
            int dstIdx = 0;
            for (int i = 0; i < len; i++) 
            {
                char ch = src[i];
                switch (ch) 
                {
                        case '\u0020': case '\u00A0': case '\u1680': case '\u2000': case '\u2001':
                        case '\u2002': case '\u2003': case '\u2004': case '\u2005': case '\u2006':
                        case '\u2007': case '\u2008': case '\u2009': case '\u200A': case '\u202F':
                        case '\u205F': case '\u3000': case '\u2028': case '\u2029': case '\u0009':
                        case '\u000A': case '\u000B': case '\u000C': case '\u000D': case '\u0085':
                            continue;
                        default:
                            src[dstIdx++] = ch;
                            break;
                }
            }
            return new string(src, 0, dstIdx);
        }
    }
}