using System;
namespace CSWeb
{
    public struct Rgb : Color
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
        public static bool operator ==(Rgb rgb1, Rgb rgb2)
        {
            return (rgb1.rgb.r == rgb2.rgb.r && rgb1.rgb.g == rgb2.rgb.g && rgb1.rgb.b == rgb2.rgb.b);
        }
        public static bool operator !=(Rgb rgb1, Rgb rgb2)
        {
            return !(rgb1.rgb.r == rgb2.rgb.r && rgb1.rgb.g == rgb2.rgb.g && rgb1.rgb.b == rgb2.rgb.b);
        }
        public override bool Equals(object obj)
        {
            if (this == obj as Rgb?)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public struct Hsl : Color
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
        public static bool operator ==(Hsl hsl1, Hsl hsl2)
        {
            return (hsl1.hsl.h == hsl2.hsl.h && hsl1.hsl.s == hsl2.hsl.s && hsl1.hsl.l == hsl2.hsl.l);
        }
        public static bool operator !=(Hsl hsl1, Hsl hsl2)
        {
            return !(hsl1.hsl.h == hsl2.hsl.h && hsl1.hsl.s == hsl2.hsl.s && hsl1.hsl.l == hsl2.hsl.l);
        }
        public static bool operator ==(Hsl hsl1, (float h, float s, float l) hsl)
        {
            return (hsl1.hsl.h == hsl.h && hsl1.hsl.s == hsl.s && hsl1.hsl.l == hsl.l);
        }
        public static bool operator !=(Hsl hsl1, (float h, float s, float l) hsl)
        {
            return !(hsl1.hsl.h == hsl.h && hsl1.hsl.s == hsl.s && hsl1.hsl.l == hsl.l);
        }
        public static bool operator ==(Hsl hsl1, Tuple<float, float, float> hsl)
        {
            return (hsl1.hsl.h == hsl.Item1 && hsl1.hsl.s == hsl.Item2 && hsl1.hsl.l == hsl.Item3);
        }
        public static bool operator !=(Hsl hsl1, Tuple<float, float, float> hsl)
        {
            return !(hsl1.hsl.h == hsl.Item1 && hsl1.hsl.s == hsl.Item2 && hsl1.hsl.l == hsl.Item3);
        }
        public override bool Equals(object obj)
        {
            if (this == obj as Hsl? || this == obj as Tuple<float, float, float>)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public struct Hex : Color
    {
        public string hex;
        public Hex(string hex)
        {
            this.hex = hex;
            if (hex.Length != 6)
            {
                throw new ConversionError("Invalid hex value!");
            }
            try
            {
                int.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch
            {
                throw new ConversionError("Invalid hex value!");
            }
        }
        public static bool operator ==(Hex hex1, Hex hex2)
        {
            return (hex1.hex == hex2.hex);
        }
        public static bool operator !=(Hex hex1, Hex hex2)
        {
            return !(hex1.hex == hex2.hex);
        }
        public override bool Equals(object obj)
        {
            if (this == obj as Hex? || this.hex == obj as string)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}