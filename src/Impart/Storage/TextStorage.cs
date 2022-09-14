using System.Text;

namespace Impart
{
    public class TextStorage : IStorage
    {
        private string Cache;
        public TextStorage(string cache)
        {
            Cache = cache;
        }

        IElement IStorage.Parse() => Parse();
        public Text Parse()
        {
            Text result;
            StringBuilder token = new StringBuilder();
            switch (Cache[1])
            {
                case 'p':
                    result = new Text("", TextType.Regular);
                    break;
                case 'b':
                    result = new Text("", TextType.Bold);
                    break;
                case 'd':
                    result = new Text("", TextType.Delete);
                    break;
                case 'e':
                    result = new Text("", TextType.Emphasize);
                    break;
                case 's':
                    switch (Cache[2])
                    {
                        case 't':
                            result = new Text("", TextType.Important);
                            break;
                        case 'm':
                            result = new Text("", TextType.Small);
                            break;
                        default:
                            if (Cache[3] == 'b')
                            {
                                result = new Text("", TextType.Subscript);
                            }
                            else
                            {
                                result = new Text("", TextType.Superscript);
                            }
                            break;
                    }
                    result = new Text("", TextType.Emphasize);
                    break;
                case 'i':
                    if (Cache[2] == 'n')
                    {
                        result = new Text("", TextType.Insert);
                    }
                    else
                    {
                        result = new Text("", TextType.Italic);
                    }
                    break;
                default:
                    result = new Text("", TextType.Mark);
                    break;
            }
            for (int i = 0; i < Cache.Length; i++)
            {
                switch (Cache[i])
                {
                    case '':
                        break;
                }
            }
            return result;
        }
    }
}