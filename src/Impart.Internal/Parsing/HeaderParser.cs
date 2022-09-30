using System;
using System.Text;

namespace Impart.Internal
{
    public static class HeaderParser
    {
        public static Header Parse(string cache, ref int index)
        {
            Header result;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            result = cache[index + 1] switch 
            {
                '1' => new Header("", 1, 0),
                '2' => new Header("", 2, 0),
                '3' => new Header("", 3, 0),
                '4' => new Header("", 4, 0),
                '5' => new Header("", 5, 0),
                _ => new Header("", 6, 0),
            };
            index += 2;
            while (cache[index] == ' ')
            {
                index++;
                while (true)
                {
                    if (cache[index] == '=')
                    {
                        index += 2;
                        while (cache[index] != '"')
                        {
                            tokenValue.Append(cache[index]);
                            index++;
                        }
                        string idRender = tokenId.ToString();
                        if (idRender == "style")
                        {
                            StorageExtensions.GetStyleAttrs(tokenValue.ToString(), result);
                        }
                        else if (idRender == "class")
                        {
                            result._IOID = Convert.ToInt32(tokenValue.ToString());
                        }
                        else
                        {
                            result.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
                        }
                        tokenId.Clear();
                        tokenValue.Clear();
                        index++;
                        break;
                    }
                    else
                    {
                        tokenId.Append(cache[index]);
                    }
                    index++;
                }
            }
            index++;
            StringBuilder content = new StringBuilder();
            while (true)
            {
                if (cache[index] == '<')
                {
                    break;
                }
                content.Append(cache[index]);
                index++;
            }
            result.TextValue = content.ToString();
            index += 4;
            return result;
        }
    }
}