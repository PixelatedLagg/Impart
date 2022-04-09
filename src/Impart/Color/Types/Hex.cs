using System;

namespace Impart
{
    /// <summary>The class for a HEX color.</summary>
    public class Hex : Color
    {
        /// <value>The HEX value.</value>
        private static string Value;

        /// <summary>Creates a Hex instance with <paramref name="hex"/> as the HEX value.</summary>
        /// <returns>A Hex instance.</returns>
        /// <param name="hex">The HEX value.</param>
        public Hex(string hex)
        {
            if (hex.Length != 6)
            {
                throw new ImpartError("Invalid hex value!");
            }
            try
            {
                int.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch
            {
                throw new ImpartError("Invalid hex value!");
            }
            Value = hex;
        }

        /// <summary>Compares the equality of two Hex values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator ==(Hex hex1, Hex hex2)
        {
            return ((string)hex1 == (string)hex2);
        }

        /// <summary>Compares the inequality of two Hex values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator !=(Hex hex1, Hex hex2)
        {
            return !((string)hex1 == (string)hex2);
        }

        /// <summary>Compares the equality of this Hex instance and an Object value.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="obj">The Object to compare.</param>
        public override bool Equals(object obj)
        {
            if (obj is Hex)
            {
                if ((string)((Hex)obj) == Value)
                {
                    return true;
                }
                return false;
            }
            else if (obj is string)
            {
                if ((string)obj == Value)
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
            return $"#{Value}";
        }

        /// <summary>Convert the Hex instance to a String.</summary>
        /// <returns>A String instance.</returns>
        /// <param name="h">The Hex to convert.</param>
        public static explicit operator string(Hex h) => Value;

        /// <summary>Convert the String instance to Hex.</summary>
        /// <returns>A Hex instance.</returns>
        /// <param name="s">The String to convert.</param>
        public static implicit operator Hex(string s) => new Hex(s);
    }
}