namespace Impart
{
    public struct Percent : Measurement
    {
        internal int percent;
        public Percent(int percent)
        {
            if (percent > 100 || percent < 0)
            {
                throw new ImpartError("Percent must be between 0-100!");
            }
            this.percent = percent;
        }
    }
    public struct Pixels : Measurement
    {
        internal int pixels;
        public Pixels(int pixels)
        {
            if (pixels < 0)
            {
                throw new ImpartError("Pixel number must be positive!");
            }
            this.pixels = pixels;
        }
        public static Pixels Px(int pixels)
        {
            return new Pixels(pixels);
        }
    }
}