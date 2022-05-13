namespace Impart
{
    /// <summary>Store a view height value (a view height is a percent of the viewport height).</summary>
    public class ViewHeight : Length
    {
        private decimal Value;

        /// <summary>Creates a ViewHeight instance.</summary>
        /// <param name="viewHeight">The view length value.</param>
        public ViewHeight(decimal viewHeight)
        {
            Value = viewHeight;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"{Value}vh";
        }

        /// <summary>Convert the ViewHeight instance to a Decimal.</summary>
        /// <param name="p">The ViewHeight to convert.</param>
        public static implicit operator decimal(ViewHeight v) => v.Value;

        /// <summary>Convert the Decimal instance to ViewHeight.</summary>
        /// <param name="i">The Decimal to convert.</param>
        public static implicit operator ViewHeight(decimal d) => new ViewHeight(d);
    }
}