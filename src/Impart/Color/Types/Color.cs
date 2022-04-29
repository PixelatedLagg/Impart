namespace Impart
{
    /// <summary>Color base class.</summary>
    public class Color
    {
        /// <summary>Creates a Color instance.</summary>
        /// <param name="s">The String to convert.</param>
        public static implicit operator Color(string s) => new Hex(s);

        /// <summary>Creates a Color instance.</summary>
        /// <param name="r">The (int, int, int) to convert.</param>
        public static implicit operator Color((int, int, int) r) => new Rgb(r);

        /// <summary>Creates a Color instance.</summary>
        /// <param name="h">The (float, float, float) to convert.</param>
        public static implicit operator Color((float, float, float) h) => new Hsl(h);

        /// <summary>Converts an Object into a Measurement instance.</summary>
        /// <param name="o">The object to convert.</param>
        public static Color Convert(object o)
        {
            if (o is string)
            {
                return new Hex((string)o);
            }
            if (o is (int, int, int))
            {
                return new Rgb(((int, int, int))o);
            }
            if (o is (float, float, float))
            {
                return new Hsl(((float, float, float))o);
            }
            return o switch
            {
                Rgb r => (Rgb)o,
                Hsl h => (Hsl)o,
                Hex he => (Hex)o,
                _ => throw new ImpartError("Invalid type to convert!")
            };
        }
    }
}