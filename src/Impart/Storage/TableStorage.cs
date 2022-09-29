using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class TableStorage : IStorage
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

        public TableStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();

        public Table ToBuilder()
        {
            Table result = new Table(0);
            int index = 6;
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
            index += 3;
            while (true)
            {
                if (Cache[index] == 't')
                {
                    break;
                }
                index++;
                TableRow row = new TableRow(0);
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
                                StorageExtensions.GetStyleAttrs(tokenValue.ToString(), row);
                            }
                            else if (idRender == "class")
                            {
                                row._IOID = Convert.ToInt32(tokenValue.ToString());
                            }
                            else
                            {
                                row.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                index += 3;
                TableRow newRow;
                if (Cache[index] == 'h')
                {
                    newRow = (TableHeader)row;
                }
                else
                {
                    newRow = row;
                }
                index += 3;
                switch (Cache[index])
                {
                    
                }
            }
            return result;
        }
    }
}

/*
< t a b l e > < / t a  b  l  e  >
< t a b l e > < t r >  <  t  d  >  <  p     c  l  a  s  s  =  "  1  "  >  a  <  /  p  >  <  /  t  d  >  <  t  d  >  <  p     c  l  a  s  s  =  "  2  "  >  b  <  /  p  >  <  /  t  d  >  <  /  t  r  >  <  /  t  a  b  l  e  >
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77

*/