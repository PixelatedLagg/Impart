using System;
using System.Text;

namespace Impart.Internal
{
    public static class TextParser
    {
        public static Text Parse(string cache, ref int index)
        {
            Text result;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            switch (cache[index])
            {
                case 'p':
                    result = new Text("", TextType.Regular, 0);
                    index++;
                    break;
                case 'b':
                    result = new Text("", TextType.Bold, 0);
                    index++;
                    break;
                case 'd':
                    result = new Text("", TextType.Delete, 0);
                    index += 2;
                    break;
                case 'e':
                    result = new Text("", TextType.Emphasize, 0);
                    index++;
                    break;
                case 's':
                    switch (cache[index + 1])
                    {
                        case 't':
                            result = new Text("", TextType.Important, 0);
                            index += 6;
                            break;
                        case 'm':
                            result = new Text("", TextType.Small, 0);
                            index += 5;
                            break;
                        default:
                            if (cache[3] == 'b')
                            {
                                result = new Text("", TextType.Subscript, 0);
                                index += 3;
                            }
                            else
                            {
                                result = new Text("", TextType.Superscript, 0);
                                index += 3;
                            }
                            break;
                    }
                    break;
                case 'i':
                    if (cache[index + 1] == 'n')
                    {
                        result = new Text("", TextType.Insert, 0);
                        index += 3;
                    }
                    else
                    {
                        result = new Text("", TextType.Italic, 0);
                        index++;
                    }
                    break;
                default:
                    result = new Text("", TextType.Mark, 0);
                    index += 4;
                    break;
            }
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
            index += 3;
            Console.WriteLine(cache[index]);
            return result;
        }
    }
}