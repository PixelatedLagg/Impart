namespace Impart
{
    /// <summary>Store milliseconds as a Float.</summary>
    public class Milliseconds : Time
    {
        private float Value;

        /// <summary>Creates a Milliseconds instance.</summary>
        /// <param name="milliseconds">The Milliseconds value.</param>
        public Milliseconds(float milliseconds)
        {
            Value = milliseconds;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"{Value}ms";
        }

        /// <summary>Convert the Milliseconds instance to a Float.</summary>
        /// <param name="s">The Milliseconds to convert.</param>
        public static implicit operator float(Milliseconds s) => s.Value;

        /// <summary>Convert the Float instance to Milliseconds.</summary>
        /// <param name="i">The Float to convert.</param>
        public static implicit operator Milliseconds(float i) => new Milliseconds(i);

        /// <value>A second represented by Milliseconds.</value>
        public static Milliseconds Second = new Milliseconds(1000);

        /// <value>A minute represented by Milliseconds.</value>
        public static Milliseconds Minute = new Milliseconds(60000);
    }
}