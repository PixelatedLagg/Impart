namespace Impart
{
    /// <summary>Time base class.</summary>
    public class Time
    {
        /// <summary>Creates a Time instance.</summary>
        /// <param name="f">The float to convert.</param>
        public static implicit operator Time(float f) => new Milliseconds(f);

        /// <summary>Creates a Time instance.</summary>
        /// <param name="i">The integer to convert.</param>
        public static implicit operator Time(int i) => new Seconds(i);
        
        /// <summary>Converts an Object into a Time instance.</summary>
        /// <param name="o">The Object to convert.</param>
        public static Time Convert(object o)
        {
            if (o is int)
            {
                return new Seconds((int)o);
            }
            if (o is float)
            {
                return new Milliseconds((float)o);
            }
            throw new ImpartError("Invalid type to convert!");
        }
    }
}