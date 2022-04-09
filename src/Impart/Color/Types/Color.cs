namespace Impart
{
    /// <summary>Color base class.</summary>
    public class Color
    {
        /// <summary>Creates a Color instance with <paramref name="s"/> as the value.</summary>
        /// <returns>A Color instance.</returns>
        /// <param name="s">The String to convert.</param>
        public static implicit operator Color(string s) => new Hex(s);

        /// <summary>Creates a Color instance with <paramref name="r"/> as the value.</summary>
        /// <returns>A Color instance.</returns>
        /// <param name="r">The (int, int, int) to convert.</param>
        public static implicit operator Color((int, int, int) r) => new Rgb(r);

        /// <summary>Creates a Color instance with <paramref name="h"/> as the value.</summary>
        /// <returns>A Color instance.</returns>
        /// <param name="h">The (float, float, float) to convert.</param>
        public static implicit operator Color((float, float, float) h) => new Hsl(h);

        /// <summary>Converts <paramref name="o"/> into a Measurement instance.</summary>
        /// <returns>A Measurement instance.</returns>
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
            throw new ImpartError("Invalid type to convert!");
        }
    }
}