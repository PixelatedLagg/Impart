namespace Impart
{
    /// <summary>Scrollbar element.</summary>
    public struct Scrollbar : Element
    {
        internal string cssCache;
        #pragma warning disable
        internal string divID;
        #pragma warning restore
        internal string bodyCache;
        private string id;
        
        /// <summary>Constructor for the scrollbar class.</summary>
        public Scrollbar(ImpartCommon.Axis axis, Measurement width, Color color, Color colorThumb, Division division, Measurement rounded = null)
        {
            if (division.scrollId == null)
            {
                throw new ImpartError("ID for division must be defined!");
            }
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            divID = "";
            bodyCache = "";
            cssCache = "";
            id = division.scrollId;
            cssCache = $"{this.id} {{";
            switch (axis)
            {
                case ImpartCommon.Axis.X:
                    cssCache += "overflow-x: auto;}";
                    break;
                case ImpartCommon.Axis.Y:
                    cssCache += "overflow-y: auto;}";
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            switch (width)
            {
                case Percent pct:
                    cssCache += $"{this.id}::-webkit-scrollbar {{width: {width.Pct().percent}%;background-color: #808080; }}{this.id}::-webkit-scrollbar-track {{";
                    break;
                case Pixels pxls:
                    cssCache += $"{this.id}::-webkit-scrollbar {{width: {width.Px().pixels}px;background-color: #808080; }}{this.id}::-webkit-scrollbar-track {{";
                    break;
            }
            switch (color)
            {
                case Rgb rgb:
                    cssCache += $"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});}}";
                    break;
                case Hsl hsl:
                    cssCache += $"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);}}";
                    break;
                case Hex hex:
                    cssCache += $"background-color: #{hex.hex};}}";
                    break;
            }
            cssCache += $"{this.id}::-webkit-scrollbar-thumb {{";
            switch (colorThumb)
            {
                case Rgb rgb:
                    cssCache += $"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case Hsl hsl:
                    cssCache += $"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case Hex hex:
                    cssCache += $"background-color: #{hex.hex};";
                    break;
            }
            if (rounded != null)
            {
                switch (rounded)
                {
                    case Percent pct:
                        cssCache += $"border-radius: {rounded.Pct().percent}%;}}";
                        break;
                    case Pixels pxls:
                        cssCache += $"border-radius: {rounded.Px().pixels}px;}}";
                        break;
                }
            }
            else
            {
                cssCache += "}";
            }
        }

        /// <summary>Constructor for the scrollbar class.</summary>
        public Scrollbar(ImpartCommon.Axis axis, Measurement width, Color color, Color colorThumb, Measurement rounded = null)
        {
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            divID = "";
            bodyCache = "";
            cssCache = "";
            id = "";
            switch (axis)
            {
                case ImpartCommon.Axis.X:
                    bodyCache += "overflow-x: auto;";
                    break;
                case ImpartCommon.Axis.Y:
                    bodyCache += "overflow-y: auto;";
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            switch (width)
            {
                case Percent pct:
                    cssCache += $"::-webkit-scrollbar {{width: {width.Pct().percent}%;background-color: #808080; }}::-webkit-scrollbar-track {{";
                    break;
                case Pixels pxls:
                    cssCache += $"::-webkit-scrollbar {{width: {width.Px().pixels}px;background-color: #808080; }}::-webkit-scrollbar-track {{";
                    break;
            }
            switch (color)
            {
                case Rgb rgb:
                    cssCache += $"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});}}";
                    break;
                case Hsl hsl:
                    cssCache += $"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);}}";
                    break;
                case Hex hex:
                    cssCache += $"background-color: #{hex.hex};}}";
                    break;
            }
            cssCache += $"::-webkit-scrollbar-thumb {{";
            switch (colorThumb)
            {
                case Rgb rgb:
                    cssCache += $"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case Hsl hsl:
                    cssCache += $"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case Hex hex:
                    cssCache += $"background-color: #{hex.hex};";
                    break;
            }
            if (rounded != null)
            {
                switch (rounded)
                {
                    case Percent pct:
                        cssCache += $"border-radius: {rounded.Pct().percent}%;}}";
                        break;
                    case Pixels pxls:
                        cssCache += $"border-radius: {rounded.Px().pixels}px;}}";
                        break;
                }
            }
            else
            {
                cssCache += "}";
            }
        }
    } 
}