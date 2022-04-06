using System;

namespace Impart
{
    /// <summary>The class for a HSL color.</summary>
    public struct Hsl : Color
    {
        /// <value>The HSL value.</value>
        public (float h, float s, float l) hsl;

        /// <summary>Creates a Hsl instance with <paramref name="h"/>, <paramref name="s"/>, <paramref name="l"/> as the HSL value.</summary>
        /// <returns>A Hsl instance.</returns>
        /// <param name="h">The h value.</param>
        /// <param name="s">The s value.</param>
        /// <param name="l">The l value.</param>
        public Hsl(float h, float s, float l)
        {
            hsl = (h, s, l);
            if (h > 360 || h < 0)
            {
                throw new ImpartError("Invalid hue value!");
            }
            if (s > 100 || s < 0)
            {
                throw new ImpartError("Invalid saturation value!");
            }
            if (l > 100 || l < 0)
            {
                throw new ImpartError("Invalid luminosity value!");
            }
        }

        /// <summary>Compares the equality of two Hsl values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator ==(Hsl hsl1, Hsl hsl2)
        {
            return (hsl1.hsl.h == hsl2.hsl.h && hsl1.hsl.s == hsl2.hsl.s && hsl1.hsl.l == hsl2.hsl.l);
        }

        /// <summary>Compares the inequality of two Hsl values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator !=(Hsl hsl1, Hsl hsl2)
        {
            return !(hsl1.hsl.h == hsl2.hsl.h && hsl1.hsl.s == hsl2.hsl.s && hsl1.hsl.l == hsl2.hsl.l);
        }

        /// <summary>Compares the equality of two Hsl values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator ==(Hsl hsl1, (float h, float s, float l) hsl2)
        {
            return (hsl1.hsl.h == hsl2.h && hsl1.hsl.s == hsl2.s && hsl1.hsl.l == hsl2.l);
        }

        /// <summary>Compares the inequality of two Hsl values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator !=(Hsl hsl1, (float h, float s, float l) hsl2)
        {
            return !(hsl1.hsl.h == hsl2.h && hsl1.hsl.s == hsl2.s && hsl1.hsl.l == hsl2.l);
        }

        /// <summary>Compares the equality of two Hsl values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator ==(Hsl hsl1, Tuple<float, float, float> hsl2)
        {
            return (hsl1.hsl.h == hsl2.Item1 && hsl1.hsl.s == hsl2.Item2 && hsl1.hsl.l == hsl2.Item3);
        }

        /// <summary>Compares the inequality of two Hsl values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator !=(Hsl hsl1, Tuple<float, float, float> hsl2)
        {
            return !(hsl1.hsl.h == hsl2.Item1 && hsl1.hsl.s == hsl2.Item2 && hsl1.hsl.l == hsl2.Item3);
        }

        /// <summary>Compares the equality of this Hsl instance and an Object value.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="obj">The object to compare.</param>
        public override bool Equals(object obj)
        {
            if (this == obj as Hsl? || this == obj as Tuple<float, float, float>)
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

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"hsl({hsl.h}, {hsl.s}%, {hsl.l}%)";
        }
    }
}