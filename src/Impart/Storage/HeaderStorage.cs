using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class HeaderStorage : IStorage
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

        public HeaderStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
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
                    result = new Header("", 1, 0);
                    index = 3;
                    break;
                case '2':
                    result = new Header("", 2, 0);
                    index = 3;
                    break;
                case '3':
                    result = new Header("", 3, 0);
                    index = 3;
                    break;
                case '4':
                    result = new Header("", 4, 0);
                    index = 3;
                    break;
                case '5':
                    result = new Header("", 5, 0);
                    index = 3;
                    break;
                default:
                    result = new Header("", 6, 0);
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