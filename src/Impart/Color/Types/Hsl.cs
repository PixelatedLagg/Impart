using System;

namespace Impart
{
    /// <summary>The class for a HSL color.</summary>
    public class Hsl : Color
    {
        /// <value>The HSL value.</value>
        private static (float h, float s, float l) Value;

        /// <summary>Creates a Hsl instance.</summary>
        /// <param name="h">The H value.</param>
        /// <param name="s">The S value.</param>
        /// <param name="l">The L value.</param>
        public Hsl(float h, float s, float l)
        {
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
            Value = (h, s, l);
        }

        /// <summary>Creates a Hsl instance.</summary>
        /// <param name="hsl">The HSL value.</param>
        public Hsl((float h, float s, float l) hsl)
        {
            if (hsl.h > 360 || hsl.h < 0)
            {
                throw new ImpartError("Invalid hue value!");
            }
            if (hsl.s > 100 || hsl.s < 0)
            {
                throw new ImpartError("Invalid saturation value!");
            }
            if (hsl.l > 100 || hsl.l < 0)
            {
                throw new ImpartError("Invalid luminosity value!");
            }
            Value = hsl;
        }

        /// <summary>Compares the equality of two Hsl values.</summary>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator ==(Hsl hsl1, Hsl hsl2)
        {
            return (((float, float, float))hsl1 == ((float, float, float))hsl2);
        }

        /// <summary>Compares the inequality of two Hsl values.</summary>
        /// <param name="hsl1">The first Hsl value to compare.</param>
        /// <param name="hsl2">The second Hsl value to compare.</param>
        public static bool operator !=(Hsl hsl1, Hsl hsl2)
        {
            return !(((float, float, float))hsl1 == ((float, float, float))hsl2);
        }

        /// <summary>Compares the equality of this Hsl instance and an Object value.</summary>
        /// <param name="obj">The Object to compare.</param>
        public override bool Equals(object obj)
        {
            if (obj is Hsl)
            {
                if (((float, float, float))((Hsl)obj) == Value)
                {
                    return true;
                }
                return false;
            }
            else if (obj is (float, float, float))
            {
                if (((float, float, float))obj == Value)
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
            return $"hsl({Value.h}, {Value.s}%, {Value.l}%)";
        }

        /// <summary>Convert the Hsl instance to a (float, float, float).</summary>
        /// <param name="h">The Hsl to convert.</param>
        public static explicit operator (float h, float s, float l)(Hsl h) => Value;

        /// <summary>Convert the (float, float, float) instance to Hsl.</summary>
        /// <param name="s">The (float, float, float) to convert.</param>
        public static implicit operator Hsl((float, float, float) h) => new Hsl(h);
    }
}