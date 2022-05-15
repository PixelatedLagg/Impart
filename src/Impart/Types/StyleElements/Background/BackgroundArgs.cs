namespace Impart
{
    public class BackgroundArgs
    {
        public readonly string Image;
        public readonly Background Background;
        public BackgroundArgs(string image, Background background)
        {
            Image = image;
            Background = background;
        }
    }
}