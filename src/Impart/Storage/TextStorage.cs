using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class TextStorage : IStorage
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

        public TextStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();

        public Text ToBuilder()
        {
            int index;
            Text result;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            switch (Cache[1])
            {
                case 'p':
                    result = new Text("", TextType.Regular, 0);
                    index = 2;
                    break;
                case 'b':
                    result = new Text("", TextType.Bold, 0);
                    index = 2;
                    break;
                case 'd':
                    result = new Text("", TextType.Delete, 0);
                    index = 4;
                    break;
                case 'e':
                    result = new Text("", TextType.Emphasize, 0);
                    index = 2;
                    break;
                case 's':
                    switch (Cache[2])
                    {
                        case 't':
                            result = new Text("", TextType.Important, 0);
                            index = 7;
                            break;
                        case 'm':
                            result = new Text("", TextType.Small, 0);
                            index = 6;
                            break;
                        default:
                            if (Cache[3] == 'b')
                            {
                                result = new Text("", TextType.Subscript, 0);
                                index = 4;
                            }
                            else
                            {
                                result = new Text("", TextType.Superscript, 0);
                                index = 4;
                            }
                            break;
                    }
                    break;
                case 'i':
                    if (Cache[2] == 'n')
                    {
                        result = new Text("", TextType.Insert, 0);
                        index = 4;
                    }
                    else
                    {
                        result = new Text("", TextType.Italic, 0);
                        index = 2;
                    }
                    break;
                default:
                    result = new Text("", TextType.Mark, 0);
                    index = 5;
                    break;
            }
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
                        if (idRender == "style")
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
            result.TextValue = content.ToString();
            return result;
        }
    }
}