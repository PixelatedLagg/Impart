using System;

namespace Impart
{
    public struct Rgb : Color
    {
        public (int r, int g, int b) rgb;
        public Rgb(int r, int g, int b)
        {
            rgb = (r, g, b);
            if (!(r >= 0 && g >= 0 && b >= 0 && r <= 255 && g <= 255 && b <= 255))
            {
                throw new ImpartError("Invalid rgb values!");
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
        public static bool operator ==(Rgb rgb1, (int r, int g, int b) rgb2)
        {
            return (rgb1.rgb.r == rgb2.r && rgb1.rgb.g == rgb2.g && rgb1.rgb.b == rgb2.b);
        }
        public static bool operator !=(Rgb rgb1, (int r, int g, int b) rgb2)
        {
            return !(rgb1.rgb.r == rgb2.r && rgb1.rgb.g == rgb2.g && rgb1.rgb.b == rgb2.b);
        }
        public static bool operator ==(Rgb rgb1, Tuple<int, int, int> rgb2)
        {
            return (rgb1.rgb.r == rgb2.Item1 && rgb1.rgb.g == rgb2.Item2 && rgb1.rgb.b == rgb2.Item3);
        }
        public static bool operator !=(Rgb rgb1, Tuple<int, int, int> rgb2)
        {
            return !(rgb1.rgb.r == rgb2.Item1 && rgb1.rgb.g == rgb2.Item2 && rgb1.rgb.b == rgb2.Item3);
        }
        public override bool Equals(object obj)
        {
            if (this == obj as Rgb? || this == obj as Tuple<int, int, int>)
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