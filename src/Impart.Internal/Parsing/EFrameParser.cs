using System;
using System.Text;

namespace Impart.Internal
{
    public static class EFrameParser
    {
        public static EFrame Parse(string cache, ref int index)
        {
            EFrame result = new EFrame(0);
            index += 6;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
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
                        switch (idRender)
                        {
                            case "style":
                                StorageExtensions.GetStyleAttrs(tokenValue.ToString(), result);
                                break;
                            case "class":
                                result._IOID = Convert.ToInt32(tokenValue.ToString());
                                break;
                            case "src":
                                result.Source = tokenValue.ToString();
                                break;
                            case "width":
                                result.Width = Convert.ToInt32(tokenValue.ToString());
                                break;
                            case "height":
                                result.Height = Convert.ToInt32(tokenValue.ToString());
                                break;
                            default:
                                result.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
                                break;
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
            index += 9;
            return result;
        }
    }
}