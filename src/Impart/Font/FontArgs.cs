using System.Text;

namespace Impart
{
    public struct FontArgs
    {
        private string Render;
        public FontArgs(string url, FontType type)
        {
            Render = (type) switch
            {
                FontType.EOT => $"src: url(\"{url}\") format(\"embedded-opentype\");",
                FontType.OTF => $"src: url(\"{url}\") format(\"opentype\");",
                FontType.SVG => $"src: url(\"{url}\") format(\"svg\");",
                FontType.TTF => $"src: url(\"{url}\") format(\"truetype\");",
                FontType.WOFF => $"src: url(\"{url}\") format(\"woff\");",
                _ => $"src: url(\"{url}\") format(\"woff2\");"
            };
        }
        public FontArgs(params (string, FontType)[] args)
        {
            StringBuilder result = new StringBuilder("src: ");
            foreach ((string url, FontType type) in args)
            {
                result.Append((type) switch
                {
                    FontType.EOT => $"url(\"{url}\") format(\"embedded-opentype\"),",
                    FontType.OTF => $"url(\"{url}\") format(\"opentype\"),",
                    FontType.SVG => $"url(\"{url}\") format(\"svg\"),",
                    FontType.TTF => $"url(\"{url}\") format(\"truetype\"),",
                    FontType.WOFF => $"url(\"{url}\") format(\"woff\"),",
                    _ => $"url(\"{url}\") format(\"woff2\"),"
                });
            }
            result.Length--;
            Render = result.Append(';').ToString();
        }
        public override string ToString()
        {
            return Render;
        }
    }
}