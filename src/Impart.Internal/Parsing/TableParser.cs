using System;
using System.Text;

namespace Impart.Internal
{
    public static class TableParser
    {
        public static Table Parse(string cache, ref int index)
        {
            Table result = new Table(0);
            index += 5;
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
            index += 3;
            while (true)
            {
                if (cache[index] == 't')
                {
                    break;
                }
                index++;
                TableRow row = new TableRow(0);
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
                            tokenId.Append(cache[index]);
                        }
                        index++;
                    }
                }
                index += 2;
                TableRow newRow;
                if (cache[index + 1] == 'h')
                {
                    newRow = new TableHeader();
                    newRow._IOID = row._IOID;
                    newRow.Attrs = row.Attrs;
                    newRow.ExtAttrs = row.ExtAttrs;
                    newRow._Events = row._Events;
                }
                else
                {
                    newRow = row;
                }
                int type = 1;
                while (cache[index] != '/')
                {
                    index += 4;
                    switch (cache[index])
                    {
                        case 'a':
                            type = 1;
                            break;
                        case 'b':
                            if (cache[index + 1] != 'u')
                            {
                                type = 2;
                            }
                            else
                            {
                                type = 3;
                            }
                            break;
                        case 'd':
                            if (cache[index + 1] == 'e')
                            {
                                type = 4;
                            }
                            else
                            {
                                type = 5;
                            }
                            break;
                        case 'e':
                            type = 6;
                            break;
                        case 'f':
                            type = 7;
                            break;
                        case 'h':
                            type = 7 + cache[index + 1] - '0';
                            break;
                        case 'i':
                            switch (cache[index + 1])
                            {
                                case 'f':
                                    type = 15;
                                    break;
                                case 'm':
                                    type = 16;
                                    break;
                                case 'n':
                                    type = 17;
                                    break;
                                default:
                                    type = 14;
                                    break;
                            }
                            break;
                        case 'm':
                            type = 18;
                            break;
                        case 'o':
                            type = 19;
                            break;
                        case 'p':
                            type = 20;
                            break;
                        case 's':
                            switch (cache[index + 1])
                            {
                                case 'u':
                                    if (cache[index + 2] == 'b')
                                    {
                                        type = 21;
                                    }
                                    else
                                    {
                                        type = 22;
                                    }
                                    break;
                                case 'm':
                                    type = 23;
                                    break;
                                case 't':
                                    type = 24;
                                    break;
                            }
                            break;
                        case 't':
                            type = 25;
                            break;
                        case 'u':
                            type = 26;
                            break;
                        case 'v':
                            type = 27;
                            break;
                    }
                    switch (type)
                    {
                        case 1:
                            newRow.Add(LinkParser.Parse(cache, ref index));
                            break;
                        case 2:
                        case 4:
                        case 6:
                        case 14:
                        case 16:
                        case 18:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                            newRow.Add(TextParser.Parse(cache, ref index));
                            break;
                        case 3:
                            newRow.Add(ButtonParser.Parse(cache, ref index));
                            break;
                        case 5:
                            newRow.Add(DivisionParser.Parse(cache, ref index));
                            break;
                        case 7:
                            newRow.Add(FormParser.Parse(cache, ref index));
                            break;
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                            newRow.Add(HeaderParser.Parse(cache, ref index));
                            break;
                        case 15:
                            newRow.Add(ImageParser.Parse(cache, ref index));
                            break;
                        case 17:
                            newRow.Add(EFrameParser.Parse(cache, ref index));
                            break;
                        case 19:
                        case 26:
                            newRow.Add(EListParser.Parse(cache, ref index));
                            break;
                        case 25:
                            newRow.Add(TableParser.Parse(cache, ref index));
                            break;
                        case 27:
                            newRow.Add(VideoParser.Parse(cache, ref index));
                            break;
                    }
                    index += 7;
                }
                index += 6;
                result.AddRow(newRow);
            }
            index += 5;
            return result;
        }
    }
}


/*

< t a b l e   c l a s  s  =  "  6  "  >  <  t  r     c  l  a  s  s  =  "  5  "  >  <  t  h  >  <  p     c  l  a  s  s  =  "  3   " >  a  <  /  p  >  <  /  t  h  >  <  t  h  >  <  p     c  l  a  s  s  =  "  3   " >  b  <  /  p  >  <  /  
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77 78 79 80 81

t  h  >  <  t  h  >  <  p     c  l  a  s  s  =  "  3  "   >   c   <   /   p   >   <   /   t   h   >   <   /   t   r   >   <   /   t   a   b   l   e   >
82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99 100 101 102 103 104 105 106 107 108 109 110 111 112 113 114 115 116 117 118 119 120 121 123 124 125
*/