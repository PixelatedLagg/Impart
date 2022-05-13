namespace Impart
{
    /// <summary>Store a view width value (a view width is a percent of the viewport width).</summary>
    public class ViewWidth : Length
    {
        private double Value;

        /// <summary>Creates a ViewWidth instance.</summary>
        /// <param name="viewWidth">The view width value.</param>
        public ViewWidth(double viewWidth)
        {
            Value = viewWidth;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"{Value}vw";
        }

        /// <summary>Convert the ViewWidth instance to a Double.</summary>
        /// <param name="p">The ViewWidth to convert.</param>
        public static implicit operator double(ViewWidth v) => v.Value;

        /// <summary>Convert the Double instance to ViewWidth.</summary>
        /// <param name="i">The Double to convert.</param>
        public static implicit operator ViewWidth(double d) => new ViewWidth(d);
    }
}