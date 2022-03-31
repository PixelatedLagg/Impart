using System;

namespace Impart
{
    /// <summary>The class for a RGB color.</summary>
    public struct Rgb : Color
    {
        /// <value>The RGB value.</value>
        public (int r, int g, int b) rgb;

        /// <summary>Creates a Rgb instance with <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> as the RGB value.</summary>
        /// <returns>A Rgb instance.</returns>
        /// <param name="r">The r value.</param>
        /// <param name="g">The g value.</param>
        /// <param name="b">The b value.</param>
        public Rgb(int r, int g, int b)
        {
            rgb = (r, g, b);
            if (!(r >= 0 && g >= 0 && b >= 0 && r <= 255 && g <= 255 && b <= 255))
            {
                throw new ImpartError("Invalid rgb values!");
            }
        }

        /// <summary>Compares the equality of two Rgb values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator ==(Rgb rgb1, Rgb rgb2)
        {
            return (rgb1.rgb.r == rgb2.rgb.r && rgb1.rgb.g == rgb2.rgb.g && rgb1.rgb.b == rgb2.rgb.b);
        }

        /// <summary>Compares the inequality of two Rgb values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator !=(Rgb rgb1, Rgb rgb2)
        {
            return !(rgb1.rgb.r == rgb2.rgb.r && rgb1.rgb.g == rgb2.rgb.g && rgb1.rgb.b == rgb2.rgb.b);
        }

        /// <summary>Compares the equality of two Rgb values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator ==(Rgb rgb1, (int r, int g, int b) rgb2)
        {
            return (rgb1.rgb.r == rgb2.r && rgb1.rgb.g == rgb2.g && rgb1.rgb.b == rgb2.b);
        }

        /// <summary>Compares the inequality of two Rgb values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator !=(Rgb rgb1, (int r, int g, int b) rgb2)
        {
            return !(rgb1.rgb.r == rgb2.r && rgb1.rgb.g == rgb2.g && rgb1.rgb.b == rgb2.b);
        }

        /// <summary>Compares the equality of two Rgb values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator ==(Rgb rgb1, Tuple<int, int, int> rgb2)
        {
            return (rgb1.rgb.r == rgb2.Item1 && rgb1.rgb.g == rgb2.Item2 && rgb1.rgb.b == rgb2.Item3);
        }

        /// <summary>Compares the inequality of two Rgb values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator !=(Rgb rgb1, Tuple<int, int, int> rgb2)
        {
            return !(rgb1.rgb.r == rgb2.Item1 && rgb1.rgb.g == rgb2.Item2 && rgb1.rgb.b == rgb2.Item3);
        }

        /// <summary>Compares the equality of this Rgb instance and an Object value.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="obj">The object to compare.</param>
        public override bool Equals(object obj)
        {
            if (this == obj as Rgb? || this == obj as Tuple<int, int, int>)
            {
                return true;
            }
            return false;
        }

        /// <summary>Returns a hash code for the current instance.</summary>
        /// <returns>An Int instance.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}