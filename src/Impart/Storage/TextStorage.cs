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

        IElement IStorage.ToBuilder() => ToBuilder();
        public Text ToBuilder()
        {
            int index;
            Text result;
            StringBuilder token = new StringBuilder(), value = new StringBuilder();
            switch (Cache[1])
            {
                case 'p':
                    result = new Text("", TextType.Regular);
                    index = 2;
                    break;
                case 'b':
                    result = new Text("", TextType.Bold);
                    index = 2;
                    break;
                case 'd':
                    result = new Text("", TextType.Delete);
                    index = 4;
                    break;
                case 'e':
                    result = new Text("", TextType.Emphasize);
                    index = 2;
                    break;
                case 's':
                    switch (Cache[2])
                    {
                        case 't':
                            result = new Text("", TextType.Important);
                            index = 7;
                            break;
                        case 'm':
                            result = new Text("", TextType.Small);
                            index = 6;
                            break;
                        default:
                            if (Cache[3] == 'b')
                            {
                                result = new Text("", TextType.Subscript);
                                index = 4;
                            }
                            else
                            {
                                result = new Text("", TextType.Superscript);
                                index = 4;
                            }
                            break;
                    }
                    break;
                case 'i':
                    if (Cache[2] == 'n')
                    {
                        result = new Text("", TextType.Insert);
                        index = 4;
                    }
                    else
                    {
                        result = new Text("", TextType.Italic);
                        index = 2;
                    }
                    break;
                default:
                    result = new Text("", TextType.Mark);
                    index = 5;
                    break;
            }
            if (char.IsWhiteSpace(Cache[index]))
            {
                ExtAttrType extAttrType;
                while (Cache[index] != '=')
                {

                }
            }
            return result;
        }
    }
}