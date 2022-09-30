using System;
using System.Text;

namespace Impart.Internal
{
    public static class FormParser
    {
        public static Form Parse(string cache, ref int index)
        {
            Form result = new Form(0);
            index += 4;
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
            index += 2;
            double otherIOID = 0;
            while (true)
            {
                if (cache[index] == 'i')
                {
                    SubmitField submitField = new SubmitField(0);
                    index += 19;
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
                                tokenId.Append(cache[index]);
                            }
                            index++;
                        }
                    }
                    result.AddSubmitField(submitField);
                    index += 3;
                    continue;
                }
                if (cache[index] == 'l')
                {
                    Text labelText = new Text("", 0);
                    Text temp = new Text("", 0);
                    index += 11;
                    while (cache[index] != '"')
                    {
                        tokenId.Append(cache[index]);
                        index++;
                    }
                    otherIOID = Convert.ToDouble(tokenId.ToString());
                    tokenId.Clear();
                    index++;
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
                                tokenId.Append(cache[index]);
                            }
                            index++;
                        }
                    }
                    index += 3;
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
                                tokenId.Append(cache[index]);
                            }
                            index++;
                        }
                    }
                    index++;
                    StringBuilder content = new StringBuilder();
                    while (true)
                    {
                        if (cache[index] == '<')
                        {
                            break;
                        }
                        content.Append(cache[index]);
                        index++;
                    }
                    labelText.TextValue = content.ToString();
                    index += 25;
                    if (cache[index] == 't')
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
                    continue;
                }
                break;
            }
            index += 4;
            return result;
        }
    }
}