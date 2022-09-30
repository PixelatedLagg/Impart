using System;
using System.Text;

namespace Impart.Internal
{
    public static class ButtonParser
    {
        public static Button Parse(string cache, ref int index)
        {
            Button result = new Button(0);
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
            index += 3;
            Text innerText = new Text("", 0);
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
                            StorageExtensions.GetStyleAttrs(tokenValue.ToString(), innerText);
                        }
                        else if (idRender == "class")
                        {
                            innerText._IOID = Convert.ToInt32(tokenValue.ToString());
                        }
                        else
                        {
                            innerText.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
            innerText.TextValue = content.ToString();
            result.Text = innerText;
            index += 12;
            return result;
        }
    }
}