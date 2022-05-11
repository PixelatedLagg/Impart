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

        /// <value>Red represented by Hex.</value>
        public static Hex Red = new Hex("FF0000");

        /// <value>Orange represented by Hex</value>
        public static Hex Orange = new Hex("FF8000");

        /// <value>Yellow represented by Hex</value>
        public static Hex Yellow = new Hex("FFFF00");

        /// <value>Lime represented by Hex</value>
        public static Hex Lime = new Hex("99FF33");

        /// <value>Dark green represented by Hex</value>
        public static Hex DarkGreen = new Hex("009900");

        /// <value>Green represented by Hex</value>
        public static Hex Green = new Hex("00FF00");

        /// <value>Light gray represented by Hex</value>
        public static Hex LightGray = new Hex("E0E0E0");

        /// <value>Aqua represented by Hex</value>
        public static Hex Aqua = new Hex("00FFFF");

        /// <value>Light blue represented by Hex</value>
        public static Hex LightBlue = new Hex("0080FF");

        /// <value>Blue represented by Hex</value>
        public static Hex Blue = new Hex("0000FF");

        /// <value>Dark blue represented by Hex</value>
        public static Hex DarkBlue = new Hex("000066");

        /// <value>Pink represented by Hex</value>
        public static Hex Pink = new Hex("FF40FF");

        /// <value>Magenta represented by Hex</value>
        public static Hex Magenta = new Hex("99004C");

        /// <value>Purple represented by Hex</value>
        public static Hex Purple = new Hex("7F00FF");

        /// <value>White represented by Hex</value>
        public static Hex White = new Hex("FFFFFF");

        /// <value>Gray represented by Hex</value>
        public static Hex Gray = new Hex("808080");

        /// <value>Dark gray represented by Hex</value>
        public static Hex DarkGray = new Hex("404040");

        /// <value>Black represented by Hex</value>
        public static Hex Black = new Hex("000000");
    }
}