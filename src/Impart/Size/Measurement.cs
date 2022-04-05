namespace Impart
{
    /// <summary>Measurement base class.</summary>
    public class Measurement
    {
        /// <summary>Creates a Measurement instance with <paramref name="f"/> as the value.</summary>
        /// <returns>A Measurement instance.</returns>
        /// <param name="f">The float to convert.</param>
        public static implicit operator Measurement(float f) => new Percent(f);

        /// <summary>Creates a Measurement instance with <paramref name="i"/> as the value.</summary>
        /// <returns>A Measurement instance.</returns>
        /// <param name="i">The integer to convert.</param>
        public static implicit operator Measurement(int i) => new Pixels(i);
    }
}