using System;
using System.Text;

namespace Impart.Internal
{
    public static class DivisionParser
    {
        public static Division Parse(string cache, ref int index)
        {
            Division result = new Division();
            index += 3;
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
                        switch (idRender)
                        {
                            case "style":
                                StorageExtensions.GetStyleAttrs(tokenValue.ToString(), result);
                                break;
                            case "class":
                                result._IOID = Convert.ToInt32(tokenValue.ToString());
                                break;
                            default:
                                result.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
                                break;
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
            while (cache[index] != '/')
            {
                if (NestParser.IsNest(cache, index))
                {
                    result.Add(NestParser.Parse(cache, ref index));
                    break;
                }
                int type = 1;
                switch (cache[index])
                {
                    case 'a':
                        type = 1;
                        break;
                    case 'b':
                        if (cache[index + 1] == 'u')
                        {
                            type = 3;
                        }
                        else
                        {
                            type = 2;
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
                                type = 17;
                                break;
                            case 'm':
                                type = 15;
                                break;
                            case 'n':
                                type = 16;
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
                        result.Add(LinkParser.Parse(cache, ref index));
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
                        result.Add(TextParser.Parse(cache, ref index));
                        break;
                    case 3:
                        result.Add(ButtonParser.Parse(cache, ref index));
                        break;
                    case 5:
                        result.Add(DivisionParser.Parse(cache, ref index));
                        break;
                    case 7:
                        result.Add(FormParser.Parse(cache, ref index));
                        break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        result.Add(HeaderParser.Parse(cache, ref index));
                        break;
                    case 15:
                        result.Add(ImageParser.Parse(cache, ref index));
                        break;
                    case 17:
                        result.Add(EFrameParser.Parse(cache, ref index));
                        break;
                    case 19:
                    case 26:
                        result.Add(EListParser.Parse(cache, ref index));
                        break;
                    case 25:
                        result.Add(TableParser.Parse(cache, ref index));
                        break;
                    case 27:
                        result.Add(VideoParser.Parse(cache, ref index));
                        break;
                }
                index += 2;
            }
            index += 4;
            return result;
        }
    }
}

/*

< d i v   c l a s s =  "  1  "  >  <  p     c  l  a  s  s  =  "  2  "  >  a  i  d  s  !  <  /  p  >  <  p     c  l  a  s  s  =  "  3  "  >  h  e  p  a  t  i  t  i  s  <  /  p  >  <  /  d  i  v  > 
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69
*/