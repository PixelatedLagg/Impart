namespace Impart
{
    /// <summary>The class for a HEX color.</summary>
    public class Hex : Color
    {
        private string Value;

        /// <summary>Creates a Hex instance.</summary>
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
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator ==(Hex hex1, Hex hex2)
        {
            return ((string)hex1 == (string)hex2);
        }

        /// <summary>Compares the inequality of two Hex values.</summary>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        public static bool operator !=(Hex hex1, Hex hex2)
        {
            return !((string)hex1 == (string)hex2);
        }

        /// <summary>Compares the equality of this Hex instance and an Object value.</summary>
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
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"#{Value}";
        }

        /// <summary>Convert the Hex instance to a String.</summary>
        /// <param name="h">The Hex to convert.</param>
        public static explicit operator string(Hex h) => h.Value;

        /// <summary>Convert the String instance to Hex.</summary>
        /// <param name="s">The String to convert.</param>
        public static implicit operator Hex(string s) => new Hex(s);

        /// <summary>Red represented by Hex.</summary>
        public static Hex Red = new Hex("FF0000");

        /// <summary>Orange represented by Hex</summary>
        public static Hex Orange = new Hex("FF8000");

        /// <summary>Yellow represented by Hex</summary>
        public static Hex Yellow = new Hex("FFFF00");

        /// <summary>Lime represented by Hex</summary>
        public static Hex Lime = new Hex("99FF33");

        /// <summary>Dark green represented by Hex</summary>
        public static Hex DarkGreen = new Hex("009900");

        /// <summary>Green represented by Hex</summary>
        public static Hex Green = new Hex("00FF00");

        /// <summary>Light gray represented by Hex</summary>
        public static Hex LightGray = new Hex("E0E0E0");

        /// <summary>Aqua represented by Hex</summary>
        public static Hex Aqua = new Hex("00FFFF");

        /// <summary>Light blue represented by Hex</summary>
        public static Hex LightBlue = new Hex("0080FF");

        /// <summary>Blue represented by Hex</summary>
        public static Hex Blue = new Hex("0000FF");

        /// <summary>Dark blue represented by Hex</summary>
        public static Hex DarkBlue = new Hex("000066");

        /// <summary>Pink represented by Hex</summary>
        public static Hex Pink = new Hex("FF40FF");

        /// <summary>Magenta represented by Hex</summary>
        public static Hex Magenta = new Hex("99004C");

        /// <summary>Purple represented by Hex</summary>
        public static Hex Purple = new Hex("7F00FF");

        /// <summary>White represented by Hex</summary>
        public static Hex White = new Hex("FFFFFF");

        /// <summary>Gray represented by Hex</summary>
        public static Hex Gray = new Hex("808080");

        /// <summary>Dark gray represented by Hex</summary>
        public static Hex DarkGray = new Hex("404040");

        /// <summary>Black represented by Hex</summary>
        public static Hex Black = new Hex("000000");
    }
}