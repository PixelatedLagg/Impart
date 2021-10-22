namespace Csweb
{
    public class ICSWeb
    {
        public static Text Text(string text, string id = null)
        {
            return new Text(text, id);
        }
        public static Image Image(string path, string id = null)
        {
            return new Image(path, id);
        }
        public static Header Header(int num, string text, string id = null)
        {
            return new Header(num, text, id);
        }
        public static Link Link(Text text, string path, string id = null)
        {
            return new Link(text, path, id);
        }
        public static Link Link(Image image, string path, string id = null)
        {
            return new Link(image, path, id);
        }
    }
}