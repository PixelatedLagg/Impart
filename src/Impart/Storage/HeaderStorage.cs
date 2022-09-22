using System.Text;
using Impart.Internal;

namespace Impart
{
    public class HeaderStorage : IStorage
    {
        private string Cache;
        public HeaderStorage(string cache)
        {
            Cache = cache;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public Header ToBuilder()
        {
            int index;
            Header result;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            System.Console.WriteLine(Cache[3]);
            switch (Cache[2])
            {
                case '1':
                    result = new Header("", 1);
                    index = 3;
                    break;
                case '2':
                    result = new Header("", 2);
                    index = 3;
                    break;
                case '3':
                    result = new Header("", 3);
                    index = 3;
                    break;
                case '4':
                    result = new Header("", 4);
                    index = 3;
                    break;
                case '5':
                    result = new Header("", 5);
                    index = 3;
                    break;
                default:
                    result = new Header("", 6);
                    index = 3;
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