using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class VideoStorage : IStorage
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
        public VideoStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }
        IElement IStorage.ToBuilder() => ToBuilder();
        public Video ToBuilder()
        {
            Video result = new Video(0);
            int index = 6;
            StringBuilder tokenId = new StringBuilder(), tokenValue = new StringBuilder();
            Length temp = null;
            bool controls = false, autoplay = false, muted = false;
            while (Cache[index] == ' ')
            {
                index++;
                while (true)
                {
                    Console.WriteLine(index);
                    if (Cache[index] == ' ')
                    {
                        Console.WriteLine(tokenId.ToString());
                        tokenId.Clear();
                        break;
                    }
                    if (Cache[index] == '=')
                    {
                        index += 2;
                        while (Cache[index] != '"')
                        {
                            tokenValue.Append(Cache[index]);
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
                                if (temp == null)
                                {
                                    temp = StorageExtensions.GetLength(tokenValue.ToString());
                                }
                                else
                                {
                                    result.Size = (StorageExtensions.GetLength(tokenValue.ToString()), temp);
                                }
                                break;
                            case "height":
                                if (temp == null)
                                {
                                    temp = StorageExtensions.GetLength(tokenValue.ToString());
                                }
                                else
                                {
                                    result.Size = (temp, StorageExtensions.GetLength(tokenValue.ToString()));
                                }
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
                        tokenId.Append(Cache[index]);
                    }
                    index++;
                }
            }
            result.Options = new VideoOptions(controls, autoplay, muted);
            return result;
        }
    }
}