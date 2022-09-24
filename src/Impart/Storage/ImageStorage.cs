using System.Text;
using Impart.Internal;
using System;

namespace Impart
{
    public class ImageStorage : IStorage
    {
        private string Cache;
        private int _IOID;
        int IStorage.IOID
        {
            get
            {
                return _IOID;
            }
        }

        public ImageStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public Image ToBuilder()
        {
            int index = 4;
            Image result = new Image();
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            while (Cache[index] == ' ')
            {
                index++;
                while (true)
                {
                    if (Cache[index] == '=')
                    {
                        index += 2;
                        while (Cache[index] != '"')
                        {
                            tokenValue.Append(Cache[index]);
                            index++;
                        }
                        string idRender = tokenId.ToString();
                        if (idRender == "src")
                        {
                            result.Source = tokenValue.ToString();
                        }
                        else if (idRender == "style")
                        {
                            int styleIndex = 0;
                            string style = tokenValue.ToString();
                            StringBuilder styleId = new StringBuilder(), styleValue = new StringBuilder();
                            bool readingId = true;
                            while (styleIndex < style.Length)
                            {
                                switch (style[styleIndex])
                                {
                                    case ';':
                                        readingId = true;
                                        Console.WriteLine($"added {styleValue.ToString()}");
                                        result.Attrs.Add(StorageExtensions.GetAttr(styleId.ToString(), styleValue.ToString()));
                                        styleId.Clear();
                                        styleValue.Clear();
                                        if (styleIndex + 2 < style.Length)
                                        {
                                            styleIndex++;
                                        }
                                        break;
                                    case ':':
                                        readingId = false;
                                        styleIndex++;
                                        break;
                                    default:
                                        if (readingId)
                                        {
                                            styleId.Append(style[styleIndex]);
                                        }
                                        else
                                        {
                                            styleValue.Append(style[styleIndex]);
                                        }
                                        break;
                                }
                                styleIndex++;
                            }
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
                        tokenId.Append(Cache[index]);
                    }
                    index++;
                }
            }
            index++;
            StringBuilder content = new StringBuilder();
            while (true)
            {
                if (Cache[index] == '<')
                {
                    break;
                }
                content.Append(Cache[index]);
                index++;
            }
            return result;
        }
    }
}