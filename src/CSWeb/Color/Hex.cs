namespace CSWeb
{
    public struct Hex : Color
    {
        public string hex;
        public Hex(string hex)
        {
            this.hex = hex;
            if (hex.Length != 6)
            {
                throw new ColorError("Invalid hex value!", this);
            }
            try
            {
                int.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch
            {
                throw new ColorError("Invalid hex value!", this);
            }
        }
        public static bool operator ==(Hex hex1, Hex hex2)
        {
            return (hex1.hex == hex2.hex);
        }
        public static bool operator !=(Hex hex1, Hex hex2)
        {
            return !(hex1.hex == hex2.hex);
        }
        public static bool operator ==(Hex hex1, string hex2)
        {
            return (hex1.hex == hex2);
        }
        public static bool operator !=(Hex hex1, string hex2)
        {
            return !(hex1.hex == hex2);
        }
        public override bool Equals(object obj)
        {
            if (this == obj as Hex? || this.hex == obj as string)
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