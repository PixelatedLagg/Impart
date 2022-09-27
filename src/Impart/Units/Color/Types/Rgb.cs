using System;

namespace Impart
{
    /// <summary>The class for a RGB color.</summary>
    public class Rgb : Color
    {
        private (int r, int g, int b) Value;

        /// <summary>Creates a Rgb instance.</summary>
        /// <param name="r">The R value.</param>
        /// <param name="g">The G value.</param>
        /// <param name="b">The B value.</param>
        public Rgb(int r, int g, int b)
        {
            if (!(r >= 0 && g >= 0 && b >= 0 && r <= 255 && g <= 255 && b <= 255))
            {
                throw new ImpartError("Invalid rgb values!");
            }
            Value = (r, g, b);
        }

        /// <summary>Creates a Rgb instance.</summary>
        /// <param name="rgb">The RGB value.</param>
        public Rgb((int r, int g, int b) rgb)
        {
            if (!(rgb.r >= 0 && rgb.g >= 0 && rgb.b >= 0 && rgb.r <= 255 && rgb.g <= 255 && rgb.b <= 255))
            {
                throw new ImpartError("Invalid rgb values!");
            }
            Value = rgb;
        }

        /// <summary>Compares the equality of two Rgb values.</summary>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator ==(Rgb rgb1, Rgb rgb2)
        {
            return (((int, int, int))rgb1 == ((int, int, int))rgb2);
        }

        /// <summary>Compares the inequality of two Rgb values.</summary>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator !=(Rgb rgb1, Rgb rgb2)
        {
            return !(((int, int, int))rgb1 == ((int, int, int))rgb2);
        }

        /// <summary>Compares the equality of this Rgb instance and an Object value.</summary>
        /// <param name="obj">The object to compare.</param>
        public override bool Equals(object obj)
        {
            if (obj is Rgb)
            {
                if (((int, int, int))((Rgb)obj) == Value)
                {
                    return true;
                }
                return false;
            }
            else if (obj is (int, int, int))
            {
                if (((int, int, int))obj == Value)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>Returns a hash code for the current instance.</summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"rgb({Value.r},{Value.g},{Value.b})";
        }

        /// <summary>Convert the Rgb instance to a (int, int, int).</summary>
        /// <param name="r">The Rgb to convert.</param>
        public static explicit operator (int r, int g, int b)(Rgb r) => r.Value;

        /// <summary>Convert the (int, int, int) instance to Rgb.</summary>
        /// <param name="r">The (int, int, int) to convert.</param>
        public static implicit operator Rgb((int, int, int) r) => new Rgb(r);

        /// <summary>Red represented by Rgb.</summary>
        public static Rgb Red = new Rgb(255, 0, 0);

        /// <summary>Orange represented by Rgb</summary>
        public static Rgb Orange = new Rgb(255, 128, 0);

        /// <summary>Yellow represented by Rgb</summary>
        public static Rgb Yellow = new Rgb(255, 255, 0);

        /// <summary>Lime represented by Rgb</summary>
        public static Rgb Lime = new Rgb(153, 255, 51);

        /// <summary>Dark green represented by Rgb</summary>
        public static Rgb DarkGreen = new Rgb(0, 153, 0);

        /// <summary>Green represented by Rgb</summary>
        public static Rgb Green = new Rgb(0, 255, 0);

        /// <summary>Light gray represented by Rgb</summary>
        public static Rgb LightGray = new Rgb(224, 224, 224);

        /// <summary>Aqua represented by Rgb</summary>
        public static Rgb Aqua = new Rgb(0, 255, 255);

        /// <summary>Light blue represented by Rgb</summary>
        public static Rgb LightBlue = new Rgb(0, 128, 255);

        /// <summary>Blue represented by Rgb</summary>
        public static Rgb Blue = new Rgb(0, 0, 255);

        /// <summary>Dark blue represented by Rgb</summary>
        public static Rgb DarkBlue = new Rgb(0, 0, 102);

        /// <summary>Pink represented by Rgb</summary>
        public static Rgb Pink = new Rgb(255, 64, 255);

        /// <summary>Magenta represented by Rgb</summary>
        public static Rgb Magenta = new Rgb(153, 0, 76);

        /// <summary>Purple represented by Rgb</summary>
        public static Rgb Purple = new Rgb(127, 0, 255);

        /// <summary>White represented by Rgb</summary>
        public static Rgb White = new Rgb(255, 255, 255);

        /// <summary>Gray represented by Rgb</summary>
        public static Rgb Gray = new Rgb(128, 128, 128);

        /// <summary>Dark gray represented by Rgb</summary>
        public static Rgb DarkGray = new Rgb(64, 64, 64);

        /// <summary>Black represented by Rgb</summary>
        public static Rgb Black = new Rgb(0, 0, 0);
    }
}