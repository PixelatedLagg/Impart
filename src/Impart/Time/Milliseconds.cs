namespace Impart
{
    public class Milliseconds : Time
    {
        private static float Value;

        /// <summary>Creates a Milliseconds instance.</summary>
        /// <param name="seconds">The Milliseconds value.</param>
        public Milliseconds(float milliseconds)
        {
            if (milliseconds < 0)
            {
                throw new ImpartError("Milliseconds number must be positive!");
            }
            Value = milliseconds;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"{Value}ms";
        }

        /// <summary>Convert the Milliseconds instance to a Float.</summary>
        /// <param name="s">The Milliseconds to convert.</param>
        public static implicit operator float(Milliseconds s) => Value;

        /// <summary>Convert the Float instance to Milliseconds.</summary>
        /// <param name="i">The Float to convert.</param>
        public static implicit operator Milliseconds(float i) => new Milliseconds(i);
    }
}