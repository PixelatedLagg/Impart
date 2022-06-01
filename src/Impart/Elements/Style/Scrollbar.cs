using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Scrollbar style element.</summary>
    public class Scrollbar : StyleElement
    {
        internal Axis Axis;
        internal Length Width;
        internal Color BackgroundColor;
        internal Color ForegroundColor;
        internal Length Radius = null;

        /// <value>The ID value of the Scrollbar. (always returns null)</value>
        string StyleElement.ID
        {
            get
            {
                return null;
            }
            set { }
        }
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int StyleElement.IOID
        {
            get
            {
                return _IOID;
            }
        }
        
        /// <summary>Creates a Scrollbar instance.</summary>
        /// <param name="axis">The axis of the scrollbar.</param>
        /// <param name="width">The width of the scrollbar.</param>
        /// <param name="backgroundColor">The background color of the scrollbar.</param>
        /// <param name="foregroundColor">The foreground color of the scrollbar.</param>
        /// <param name="radius">The radius of the top and bottom of the scrollbar thumb, if rounded.</param>
        public Scrollbar(Axis axis, Length width, Color backgroundColor, Color foregroundColor, Length radius = null)
        {
            Axis = axis;
            Width = width;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            Radius = radius;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($"::-webkit-scrollbar {{width: {Width};background-color: #808080; }}::-webkit-scrollbar-track{{background-color: {BackgroundColor};}}::-webkit-scrollbar-thumb {{background-color: {ForegroundColor};");
            if (Radius != null)
            {
                result.Append($"border-radius: {Radius};}}");
            }
            else
            {
                result.Append('}');
            }
            return result.ToString();
        }
    }
}