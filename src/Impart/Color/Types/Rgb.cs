using System;

namespace Impart
{
    /// <summary>The class for a RGB color.</summary>
    public struct Rgb : Color
    {
        /// <value>The RGB value.</value>
        private static (int r, int g, int b) Value;

        /// <summary>Creates a Rgb instance with <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> as the RGB value.</summary>
        /// <returns>A Rgb instance.</returns>
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

        /// <summary>Creates a Rgb instance with <paramref name="rgb"/> as the RGB value.</summary>
        /// <returns>A Rgb instance.</returns>
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
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator ==(Rgb rgb1, Rgb rgb2)
        {
            return (((int, int, int))rgb1 == ((int, int, int))rgb2);
        }

        /// <summary>Compares the inequality of two Rgb values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="rgb1">The first Rgb value to compare.</param>
        /// <param name="rgb2">The second Rgb value to compare.</param>
        public static bool operator !=(Rgb rgb1, Rgb rgb2)
        {
            return !(((int, int, int))rgb1 == ((int, int, int))rgb2);
        }

        /// <summary>Compares the equality of this Rgb instance and an Object value.</summary>
        /// <returns>A Bool instance.</returns>
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
        /// <returns>An Int instance.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"rgb({Value.r}, {Value.g}, {Value.b})";
        }

        /// <summary>Convert the Rgb instance to a (int, int, int).</summary>
        /// <returns>A (int, int, int) instance.</returns>
        /// <param name="r">The Rgb to convert.</param>
        public static explicit operator (int r, int g, int b)(Rgb r) => Value;

        /// <summary>Convert the (int, int, int) instance to Rgb.</summary>
        /// <returns>A Rgb instance.</returns>
        /// <param name="s">The (int, int, int) to convert.</param>
        public static implicit operator Rgb((int, int, int) r) => new Rgb(r);
    }
}