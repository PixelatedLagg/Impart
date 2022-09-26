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
                    else
                    {
                        listType = 9;
                    }
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
                        Text entry;
                        switch (listType)
                        {
                            case 0:
                                entry = new Text("", TextType.Regular, 0);
                                index += 6;
                                break;
                            case 1:
                                entry = new Text("", TextType.Bold, 0);
                                index += 6;
                                break;
                            case 2:
                                entry = new Text("", TextType.Delete, 0);
                                index += 8;
                                break;
                            case 3:
                                entry = new Text("", TextType.Emphasize, 0);
                                index += 7;
                                break;
                            case 4:
                                entry = new Text("", TextType.Important, 0);
                                index += 11;
                                break;
                            case 5:
                                entry = new Text("", TextType.Small, 0);
                                index += 10;
                                break;
                            case 6:
                                entry = new Text("", TextType.Subscript, 0);
                                index += 8;
                                break;
                            case 7:
                                entry = new Text("", TextType.Superscript, 0);
                                index += 8;
                                break;
                            case 8:
                                entry = new Text("", TextType.Insert, 0);
                                index += 8;
                                break;
                            default:
                                entry = new Text("", TextType.Italic, 0);
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
                                        StorageExtensions.GetStyleAttrs(tokenValue.ToString(), entry);
                                    }
                                    else if (idRender == "class")
                                    {
                                        entry._IOID = Convert.ToInt32(tokenValue.ToString());
                                    }
                                    else
                                    {
                                        entry.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                        entry.TextValue = content.ToString();
                        result.Add((T)(IElement)(entry));
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

/*
                                                                                            <  /  u  l  >
        |         |                                  |                       |  |      
< u l > < l i > < p    c  l  a  s  s  =  "  1  "  >  a  i  d  s  <  /  p  >  <  /  l  i  >  <  l  i  >  <  p     c  l  a  s  s  =  "  2  "  >  a  h  h  h  <  /  p  >  <  /  l  i  >  <  l  i  >  <  p     c  l  a  s  s  =  "  3  "  >  e  e  k  <  /  p  >  <  /  l  i  >  <  /  u  l  >
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 72 73 74 75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97

*/