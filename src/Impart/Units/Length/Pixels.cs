namespace Impart
{
    /// <summary>Store a pixel value.</summary>
    public class Pixels : Length
    {
        private int Value;

        /// <summary>Creates a Pixels instance.</summary>
        /// <param name="pixels">The pixels value.</param>
        public Pixels(int pixels)
        {
            Value = pixels;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"{Value}px";
        }

        /// <summary>Convert the Pixels instance to an Int.</summary>
        /// <param name="p">The Pixels to convert.</param>
        public static implicit operator int(Pixels p) => p.Value;

        /// <summary>Convert the Int instance to Pixels.</summary>
        /// <param name="i">The Int to convert.</param>
        public static implicit operator Pixels(int i) => new Pixels(i);
    }
}