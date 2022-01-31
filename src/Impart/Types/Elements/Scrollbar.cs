namespace Impart
{
    /// <summary>Scrollbar element.</summary>
    public struct Scrollbar : Element
    {
        internal Axis axis;
        internal Measurement width;
        internal Color bgColor;
        internal Color fgColor;
        internal Measurement radius = null;
        
        /// <summary>Creates a Division instance with <paramref name="axis"> as the axis, <paramref name="width"> as the width, 
        /// <paramref name="bgColor"> as the background color, <paramref name="fgColor"> as the foreground color, and <paramref name="radius"> as the optional radius of the top and bottom of the thumb.</summary>
        /// <returns>A Scrollbar instance.</returns>
        /// <param name="axis">The axis of the scrollbar.</param>
        /// <param name="width">The width of the scrollbar.</param>
        /// <param name="bgColor">The background color of the scrollbar.</param>
        /// <param name="fgColor">The foreground color of the scrollbar.</param>
        /// <param name="radius">The radius of the top and bottom of the scrollbar thumb, if rounded.</param>
        public Scrollbar(Axis axis, Measurement width, Color bgColor, Color fgColor, Measurement radius = null)
        {
            this.axis = axis;
            this.width = width;
            this.bgColor = bgColor;
            this.fgColor = fgColor;
            this.radius = radius;
        }
    } 
}