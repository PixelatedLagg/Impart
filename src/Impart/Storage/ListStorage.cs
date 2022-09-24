using Impart;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class ListStorage<T> : IStorage where T : IElement
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

        public ListStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public EList<T> ToBuilder()
        {
            int index = 3;
            EList<T> result;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            if (Cache[2] == 'u')
            {
                result = new EList<T>(EListType.Unordered);
            }
            else
            {
                result = new EList<T>(EListType.Ordered);
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
            System.Console.WriteLine(Cache[index + 1]);
            switch (Cache[index + 1])
            {

            }
            return result;
        }
    }
}