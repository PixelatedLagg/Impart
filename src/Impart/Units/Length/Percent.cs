namespace Impart
{
    /// <summary>Store a percent value.</summary>
    public class Percent : Length
    {
        private float Value;

        /// <summary>Creates a Percent instance.</summary>
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
        public override string ToString()
        {
            return $"{Value}%";
        }

        /// <summary>Convert the Percent instance to a Float.</summary>
        /// <param name="p">The Percent to convert.</param>
        public static implicit operator float(Percent p) => p.Value;

        /// <summary>Convert the Float instance to Percent.</summary>
        /// <param name="f">The Float to convert.</param>
        public static implicit operator Percent(float f) => new Percent(f);
    }
}