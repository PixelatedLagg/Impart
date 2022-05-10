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
            if (o is int)
            {
                return new Pixels((int)o);
            }
            if (o is float)
            {
                return new Percent((float)o);
            }
            throw new ImpartError("Invalid type to convert!");
        }
    }
}