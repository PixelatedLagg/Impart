using System;

namespace Impart
{
    /// <summary>The measurement class for a percent value.</summary>
    public class Percent : Measurement
    {
        private static float Value;

        /// <summary>Creates a Percent instance with <paramref name="percent"/> as the value.</summary>
        /// <returns>A Percent instance.</returns>
        /// <param name="percent">The percent value.</param>
        public Percent(float percent)
        {
            if (percent > 100 || percent < 0)
            {
                throw new ImpartError("Percent must be between 0-100!");
            }
            Value = percent;
        }

        /// <summary>Convert the Percent instance to a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"{Value}%";
        }

        /// <summary>Convert the Percent instance to a Float.</summary>
        /// <returns>A Float instance.</returns>
        /// <param name="p">The Percent to convert.</param>
        public static implicit operator float(Percent p) => Value;

        /// <summary>Convert the Float instance to Percent.</summary>
        /// <returns>A Percent instance.</returns>
        /// <param name="f">The Float to convert.</param>
        public static implicit operator Percent(float f) => new Percent(f);
    }
}