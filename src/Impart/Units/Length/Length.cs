namespace Impart
{
    /// <summary>Length base class.</summary>
    public class Length
    {
        /// <summary>Creates a Length instance.</summary>
        /// <param name="f">The float to convert.</param>
        public static implicit operator Length(float f) => new Percent(f);

        /// <summary>Creates a Length instance.</summary>
        /// <param name="i">The integer to convert.</param>
        public static implicit operator Length(int i) => new Pixels(i);

        /// <summary>Converts an Object into a Length instance.</summary>
        /// <param name="o">The Object to convert.</param>
        public static Length Convert(object o)
        {
            return o switch
            {
                int => new Pixels((int)o),
                float => new Percent((float)o),
                double => new ViewWidth((double)o),
                decimal => new ViewHeight((decimal)o),
                _ => throw new ImpartError("Invalid type to convert!")
            };
        }
    }
}