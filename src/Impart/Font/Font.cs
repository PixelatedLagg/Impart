using System.Text;

namespace Impart
{
    /// <summary>A font sourced from file(s).</summary>
    public struct Font
    {
        private string Render;

        /// <summary>Creates a Font instance.</summary>
        /// <param name="name">The name for the font family.</param>
        /// <param name="url">The URL to the source file.</param>
        /// <param name="type">The type of file format.</param>
        public Font(string name, string url, FontType type)
        {
            Render = (type) switch
            {
                FontType.EOT => $"font-family: \"{name}\"; src: url(\"{url}\") format(\"embedded-opentype\");",
                FontType.OTF => $"font-family: \"{name}\"; src: url(\"{url}\") format(\"opentype\");",
                FontType.SVG => $"font-family: \"{name}\"; src: url(\"{url}\") format(\"svg\");",
                FontType.TTF => $"font-family: \"{name}\"; src: url(\"{url}\") format(\"truetype\");",
                FontType.WOFF => $"font-family: \"{name}\"; src: url(\"{url}\") format(\"woff\");",
                _ => $"font-family: \"{name}\"; src: url(\"{url}\") format(\"woff2\");"
            };
        }

        /// <summary>Creates a Font instance.</summary>
        /// <param name="name">The name for the font family.</param>
        /// <param name="args">An array of URLs to source files and those files' format types.</param>
        public Font(string name, params (string, FontType)[] args)
        {
            StringBuilder result = new StringBuilder($"font-family: \"{name}\"; src: ");
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

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return Render;
        }
    }
}