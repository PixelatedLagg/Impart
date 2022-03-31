namespace Impart
{
    /// <summary>The class for a HEX color.</summary>
    public struct Hex : Color
    {
        /// <value>The HEX value.</value>
        public string hex;

        /// <summary>Creates a Hex instance with <paramref name="hex"/> as the HEX value.</summary>
        /// <returns>A Hex instance.</returns>
        /// <param name="hex">The HEX value.</param>
        public Hex(string hex)
        {
            this.hex = hex;
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
        }

        /// <summary>Compares the equality of two Hex values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator ==(Hex hex1, Hex hex2)
        {
            return (hex1.hex == hex2.hex);
        }

        /// <summary>Compares the inequality of two Hex values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator !=(Hex hex1, Hex hex2)
        {
            return !(hex1.hex == hex2.hex);
        }

        /// <summary>Compares the equality of two Hex values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator ==(Hex hex1, string hex2)
        {
            return (hex1.hex == hex2);
        }

        /// <summary>Compares the inequality of two Hex values.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator !=(Hex hex1, string hex2)
        {
            return !(hex1.hex == hex2);
        }

        /// <summary>Compares the equality of this Hex instance and an Object value.</summary>
        /// <returns>A Bool instance.</returns>
        /// <param name="obj">The object to compare.</param>
        public override bool Equals(object obj)
        {
            if (this == obj as Hex? || this.hex == obj as string)
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