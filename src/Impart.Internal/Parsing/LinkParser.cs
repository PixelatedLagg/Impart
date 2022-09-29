using System;
using System.Text;

namespace Impart.Internal
{
    public static class LinkParser
    {
        public static Link Parse(string cache, ref int index)
        {
            Link result = new Link("", 0);
            index++;
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
                        else if (idRender == "href")
                        {
                            result.Path = tokenValue.ToString();
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
            index += 2;
            if (cache[index] == 'p')
            {
                index++;
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
                result.LinkType = typeof(Text);
                index += 7;
            }
            else
            {
                index += 3;
                Image innerImage = new Image("", 0);
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
                            if (idRender == "src")
                            {
                                innerImage.Source = tokenValue.ToString();
                            }
                            else if (idRender == "style")
                            {
                                StorageExtensions.GetStyleAttrs(tokenValue.ToString(), innerImage);
                            }
                            else
                            {
                                innerImage.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                result.Image = innerImage;
                result.LinkType = typeof(Image);
                index += 9;
            }
            Console.WriteLine(cache[index]);
            return result;
        } 
    }
}