using System;
using System.Text;

namespace Impart.Internal
{
    public static class VideoParser
    {
        public static Video Parse(string cache, ref int index)
        {
            Video result = new Video(0);
            index += 5;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            bool controls = false, autoplay = false, muted = false;
            while (cache[index] == ' ')
            {
                index++;
                while (true)
                {
                    if (cache[index] == ' ')
                    {
                        switch (tokenId.ToString())
                        {
                            case "controls":
                                controls = true;
                                break;
                            case "autoplay":
                                autoplay = true;
                                break;
                            case "muted":
                                muted = true;
                                break;
                        }
                        tokenId.Clear();
                        index++;
                        while (cache[index] != '>')
                        {
                            if (cache[index] == ' ')
                            {
                                switch (tokenId.ToString())
                                {
                                    case "controls":
                                        controls = true;
                                        break;
                                    case "autoplay":
                                        autoplay = true;
                                        break;
                                    case "muted":
                                        muted = true;
                                        break;
                                }
                                tokenId.Clear();
                            }
                            else
                            {
                                tokenId.Append(cache[index]);
                            }
                            index++;
                        }
                        switch (tokenId.ToString())
                        {
                            case "controls":
                                controls = true;
                                break;
                            case "autoplay":
                                autoplay = true;
                                break;
                            case "muted":
                                muted = true;
                                break;
                        }
                        tokenId.Clear();
                        break;
                    }
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
                            case "src":
                                result.Source = tokenValue.ToString();
                                break;
                            case "width":
                                result.Width = Convert.ToInt32(tokenValue.ToString());
                                break;
                            case "height":
                                result.Height = Convert.ToInt32(tokenValue.ToString());
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
            result.Options = new VideoOptions(controls, autoplay, muted);
            index += 8;
            Console.WriteLine(cache[index]);
            return result;
        }
    }
}