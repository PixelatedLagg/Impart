namespace Csweb
{
    public struct Rgb
    {
        public (int r, int g, int b) rgb;
        public Rgb(int r, int g, int b)
        {
            rgb = (r, g, b);
            if (!(r >= 0 && g >= 0 && b >= 0 && r <= 255 && g <= 255 && b <= 255))
            {
                throw new ColorError("Invalid rgb values!", this);
            }
        }
    }
    public struct Hsl
    {
        public (float h, float s, float l) hsl;
        public Hsl(float h, float s, float l)
        {
            hsl = (h, s, l);
            if (h > 360 || h < 0)
            {
                throw new ColorError("Invalid hue value!", this);
            }
            if (s > 100 || s < 0)
            {
                throw new ColorError("Invalid saturation value!", this);
            }
            if (l > 100 || l < 0)
            {
                throw new ColorError("Invalid luminosity value!", this);
            }
        }
    }
}