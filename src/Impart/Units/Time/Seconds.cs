namespace Impart
{
    public class Seconds : Time
    {
        private int Value;

        /// <summary>Creates a Seconds instance.</summary>
        /// <param name="seconds">The seconds value.</param>
        public Seconds(int seconds)
        {
            Value = seconds;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"{Value}s";
        }

        /// <summary>Convert the Seconds instance to an Int.</summary>
        /// <param name="s">The Seconds to convert.</param>
        public static implicit operator int(Seconds s) => s.Value;

        /// <summary>Convert the Int instance to Seconds.</summary>
        /// <param name="i">The Int to convert.</param>
        public static implicit operator Seconds(int i) => new Seconds(i);

        /// <value>A minute represented by Seconds.</value>
        public static Seconds Minute = new Seconds(60);

        /// <value>A hour represented by Seconds.</value>
        public static Seconds Hour = new Seconds(3600);
    }
}