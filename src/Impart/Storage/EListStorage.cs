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
            bool typeSet = false;
            int listType = 0;
            Console.WriteLine($"start: {index}");
            while (Cache[index + 3] != 'l')
            {
                Console.WriteLine($"while check: {index + 3}");
                if (!typeSet)
                {
                    switch (Cache[index + 5])
                    {
                        case 'p':
                            listType = 0;
                            typeSet = true;
                            index += 6;
                            break;
                    }
                }
                else
                {
                    switch (listType)
                    {
                        case 0:
                            Text entry = new Text("", 0);
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
                                            Console.WriteLine($"style: {tokenValue.ToString()}");
                                            StorageExtensions.GetStyleAttrs(tokenValue.ToString(), entry);
                                        }
                                        else if (idRender == "class")
                                        {
                                            Console.WriteLine($"class: {tokenValue.ToString()}");
                                            entry._IOID = Convert.ToInt32(tokenValue.ToString());
                                        }
                                        else
                                        {
                                            Console.WriteLine($"ext attr: {idRender} - {tokenValue.ToString()}");
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
                            Console.WriteLine($"a: {index}");
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
                            index += 4;
                            Console.WriteLine($"b: {index}");
                            Console.WriteLine($"text value: {content.ToString()}");
                            entry.TextValue = content.ToString();
                            result.Add((T)(IElement)(entry));
                            break;
                    }
                }
            }
            return result;
        }
    }
}

/*

< u l > < l i > < p    c  l  a  s  s  =  "  1  "  >  a  i  d  s  <  /  p  >  <  /  l  i  >  <  l  i  >  <  p     c  l  a  s  s  =  "  2  "  >  a  h  h  h  <  /  p  >  <  /  l  i  >  <  l  i  >  <  p     c  l  a  s  s  =  "  3  "  >  e  e  k  <  /  p  >  <  /  l  i  >  <  /  u  l  >
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 72 73 74 75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97

*/