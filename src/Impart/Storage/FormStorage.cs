using System;
using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    public class FormStorage : IStorage 
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
        public FormStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }
        IElement IStorage.ToBuilder() => ToBuilder();
        public Form ToBuilder()
        {
            Form result = new Form(0);
            int index = 5;
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
            index += 2;
            double otherIOID = 0;
            while (true)
            {
                if (Cache[index] == 'i')
                {
                    SubmitField submitField = new SubmitField(0);
                    index += 19;
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
                                    StorageExtensions.GetStyleAttrs(tokenValue.ToString(), submitField);
                                }
                                else if (idRender == "class")
                                {
                                    submitField._IOID = Convert.ToInt32(tokenValue.ToString());
                                }
                                else
                                {
                                    submitField.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                    result.AddSubmitField(submitField);
                    index += 3;
                    Console.WriteLine(Cache[index - 1] + "" + Cache[index] + "" + Cache[index + 1]);
                    continue;
                }
                if (Cache[index] == 'l')
                {
                    Text labelText = new Text("", 0);
                    Text temp = new Text("", 0);
                    index += 11;
                    while (Cache[index] != '"')
                    {
                        tokenId.Append(Cache[index]);
                        index++;
                    }
                    otherIOID = Convert.ToDouble(tokenId.ToString());
                    tokenId.Clear();
                    index++;
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
                                    StorageExtensions.GetStyleAttrs(tokenValue.ToString(), temp);
                                }
                                else if (idRender == "class")
                                {
                                    temp._IOID = Convert.ToInt32(tokenValue.ToString());
                                }
                                else
                                {
                                    temp.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                                    StorageExtensions.GetStyleAttrs(tokenValue.ToString(), labelText);
                                }
                                else if (idRender == "class")
                                {
                                    labelText._IOID = Convert.ToInt32(tokenValue.ToString());
                                }
                                else
                                {
                                    labelText.ExtAttrs.Add(StorageExtensions.GetExtAttr(idRender, tokenValue.ToString()));
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
                    labelText.TextValue = content.ToString();
                    index += 25;
                    if (Cache[index] == 't')
                    {
                        TextField textField = new TextField(0);
                        textField._IOID = temp._IOID;
                        textField._OtherIOID = otherIOID;
                        textField.ExtAttrs = temp.ExtAttrs;
                        textField.Attrs = temp.Attrs;
                        textField.Text = labelText;
                        result.AddTextField(textField);
                        index += 13 + otherIOID.ToString().Length;
                    }
                    else
                    {
                        CheckField checkField = new CheckField(0);
                        checkField._IOID = temp._IOID;
                        checkField._OtherIOID = otherIOID;
                        checkField.ExtAttrs = temp.ExtAttrs;
                        checkField.Attrs = temp.Attrs;
                        checkField.Text = labelText;
                        result.AddCheckField(checkField);
                        index += 17 + otherIOID.ToString().Length;
                    }
                    index += 2;
                    Console.WriteLine(Cache[index - 1] + "" + Cache[index] + "" + Cache[index + 1]);
                    continue;
                }
                break;
            }
            return result;
        }
    }
}
/*
< f o r m > < i n p  u  t     t  y  p  e  =  "  t  e  x  t  "  >  <  /  f  o  r  m  >
1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69
*/