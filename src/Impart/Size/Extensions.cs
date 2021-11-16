namespace Impart
{
    public static class SizeExtensions
    {
        public static Percent Pct(this int percent)
        {
            return new Percent(percent);
        }
        public static Pixels Px(this int pixels)
        {
            return new Pixels(pixels);
        }
        internal static Percent Pct(this Measurement measurement)
        {
            return (Percent)measurement;
        }
        internal static Pixels Px(this Measurement measurement)
        {
            return (Pixels)measurement;
        }
    }
}