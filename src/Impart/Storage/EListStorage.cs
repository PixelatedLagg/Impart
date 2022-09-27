using System;
using Impart;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class EListStorage<T> : IStorage where T : IElement
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

        public EListStorage(string cache, int ioid)
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
                result = new EList<T>(EListType.Unordered, 0);
            }
            else
            {
                result = new EList<T>(EListType.Ordered, 0);
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
            int listType = 0;
            switch (Cache[index + 5])
            {
                case 'p':
                    listType = 0;
                    break;
                case 'b':
                    listType = 1;
                    break;
                case 'd':
                    listType = 2;
                    break;
                case 'e':
                    listType = 3;
                    break;
                case 's':
                    switch (Cache[2])
                    {
                        case 't':
                            listType = 4;
                            break;
                        case 'm':
                            listType = 5;
                            break;
                        default:
                            if (Cache[index + 6] == 'b')
                            {
                                listType = 6;
                            }
                            else
                            {
                                listType = 7;
                            }
                            break;
                    }
                    break;
                case 'i':
                    if (Cache[index + 6] == 'n')
                    {
                        listType = 8;
                    }
                    else if (Cache[index + 6] == 'm')
                    {
                        listType = 10;
                    }
                    else
                    {
                        listType = 9;
                    }
                    break;
                case 'h':
                    listType = Cache[index + 6] - '0' + 10;
                    break;
                case 'o':
                    listType = 17;
                    break;
                case 'u':
                    listType = 18;
                    break;
                case 'a':
                    listType = 19;
                    break;
                default:
                    listType = 0;
                    break;
            }
            while (true)
            {
                switch (listType)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        Text entryText;
                        switch (listType)
                        {
                            case 0:
                                entryText = new Text("", TextType.Regular, 0);
                                index += 6;
                                break;
                            case 1:
                                entryText = new Text("", TextType.Bold, 0);
                                index += 6;
                                break;
                            case 2:
                                entryText = new Text("", TextType.Delete, 0);
                                index += 8;
                                break;
                            case 3:
                                entryText = new Text("", TextType.Emphasize, 0);
                                index += 7;
                                break;
                            case 4:
                                entryText = new Text("", TextType.Important, 0);
                                index += 11;
                                break;
                            case 5:
                                entryText = new Text("", TextType.Small, 0);
                                index += 10;
                                break;
                            case 6:
                                entryText = new Text("", TextType.Subscript, 0);
                                index += 8;
                                break;
                            case 7:
                                entryText = new Text("", TextType.Superscript, 0);
                                index += 8;
                                break;
                            case 8:
                                entryText = new Text("", TextType.Insert, 0);
                                index += 8;
                                break;
                            default:
                                entryText = new Text("", TextType.Italic, 0);
                                index += 6;
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
                                        StorageExtensions.GetStyleAttrs(tokenValue.ToString(), entryText);
                                    }
                                    else if (idRender == "class")
                                    {
                                        entryText._IOID = Convert.ToInt32(tokenValue.ToString());
                                    }
                                    else
                                    {
                                        entryText.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                        StringBuilder contentText = new StringBuilder();
                        while (true)
                        {
                            if (Cache[index] == '<')
                            {
                                break;
                            }
                            contentText.Append(Cache[index]);
                            index++;
                        }
                        entryText.TextValue = contentText.ToString();
                        result.Add((T)(IElement)(entryText));
                        index += listType switch {
                            0 => 9,
                            1 => 9,
                            2 => 11,
                            3 => 10,
                            4 => 14,
                            5 => 13,
                            6 => 11,
                            7 => 11,
                            8 => 11,
                            _ => 9
                        };
                        break;
                    case 10:
                        Image entryImage = new Image("/", 0);
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
                                entryImage.Source = tokenValue.ToString();
                            }
                            else if (idRender == "style")
                            {
                                StorageExtensions.GetStyleAttrs(tokenValue.ToString(), entryImage);
                            }
                            else if (idRender == "class")
                            {
                                entryImage._IOID = Convert.ToInt32(tokenValue.ToString());
                            }
                            else
                            {
                                entryImage.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                        result.Add((T)(IElement)(entryImage));
                        index += 12;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        Header entryHeader = new Header("", listType - 10, 0);
                        index += 7;
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
                                        StorageExtensions.GetStyleAttrs(tokenValue.ToString(), entryHeader);
                                    }
                                    else if (idRender == "class")
                                    {
                                        entryHeader._IOID = Convert.ToInt32(tokenValue.ToString());
                                    }
                                    else
                                    {
                                        entryHeader.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                        StringBuilder contentHeader = new StringBuilder();
                        while (true)
                        {
                            if (Cache[index] == '<')
                            {
                                break;
                            }
                            contentHeader.Append(Cache[index]);
                            index++;
                        }
                        entryHeader.TextValue = contentHeader.ToString();
                        index += 10;
                        break;
                    case 17:
                    case 18:
                        //17 is ol
                        //18 is ul
                        break;
                    case 19:
                    
                        break;
                }
                if (Cache[index + 3] == 'l')
                {
                    break;
                }
            }
            return result;
        }
    }
}