namespace Impart
{
    /// <summary>Class that represents a division scrollbar.</summary>
    public class Scrollbar : Element
    {
        internal string cssCache;
        internal string divID;
        internal string bodyCache;
        private string id;
        
        /// <summary>Constructor for the scrollbar class.</summary>
        public Scrollbar(ImpartCommon.Axis axis, int width, Color color, Color colorThumb, Division division = null, int? rounded = null)
        {
            if (division != null && division.scrollId == null)
            {
                throw new ScrollbarError("ID for division must be defined!", this);
            }
            bodyCache = "";
            if (division != null)
            {
                id = division.scrollId;
                cssCache = $"%^{this.id} {{%^";
                switch (axis)
                {
                    case ImpartCommon.Axis.X:
                        cssCache += "    overflow-x: auto;%^}%^";
                        break;
                    case ImpartCommon.Axis.Y:
                        cssCache += "    overflow-y: auto;%^}%^";
                        break;
                    default:
                        throw new ScrollbarError("Invalid axis!", this);
                }
                if (width <= 0)
                {
                    throw new ScrollbarError("Scrollbar width cannot be less than or equal to 0!", this);
                }
                cssCache += $"%^{this.id}::-webkit-scrollbar {{%^    width: {width}px;%^    background-color: #808080; %^}}%^{this.id}::-webkit-scrollbar-track {{%^";
                switch (color.GetType().FullName)
                {
                    case "Impart.Rgb":
                        Rgb rgb = (Rgb)color;
                        cssCache += $"    background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^}}";
                        break;
                    case "Impart.Hsl":
                        Hsl hsl = (Hsl)color;
                        cssCache += $"    background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^}}";
                        break;
                    case "Impart.Hex":
                        Hex hex = (Hex)color;
                        cssCache += $"    background-color: #{hex.hex};%^}}";
                        break;
                }
                cssCache += $"%^{this.id}::-webkit-scrollbar-thumb {{%^";
                switch (colorThumb.GetType().FullName)
                {
                    case "Impart.Rgb":
                        Rgb rgb = (Rgb)colorThumb;
                        cssCache += $"    background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^";
                        break;
                    case "Impart.Hsl":
                        Hsl hsl = (Hsl)colorThumb;
                        cssCache += $"    background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^";
                        break;
                    case "Impart.Hex":
                        Hex hex = (Hex)colorThumb;
                        cssCache += $"    background-color: #{hex.hex};%^";
                        break;
                }
                if (rounded != null)
                {
                    if (rounded < 0)
                    {
                        throw new ScrollbarError("Rounded value cannot be below 0!", this);
                    }
                    cssCache += $"    border-radius: {rounded}px;%^}}";
                }
                else
                {
                    cssCache += "}";
                }
                return;
            }
            else
            {
                switch (axis)
                {
                    case ImpartCommon.Axis.X:
                        bodyCache += "    overflow-x: auto;%^";
                        break;
                    case ImpartCommon.Axis.Y:
                        bodyCache += "    overflow-y: auto;%^";
                        break;
                    default:
                        throw new ScrollbarError("Invalid axis!", this);
                }
                if (width <= 0)
                {
                    throw new ScrollbarError("Scrollbar width cannot be less than or equal to 0!", this);
                }
                cssCache += $"%^::-webkit-scrollbar {{%^    width: {width}px;%^    background-color: #808080; %^}}%^::-webkit-scrollbar-track {{%^";
                switch (color.GetType().FullName)
                {
                    case "Impart.Rgb":
                        Rgb rgb = (Rgb)color;
                        cssCache += $"    background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^}}";
                        break;
                    case "Impart.Hsl":
                        Hsl hsl = (Hsl)color;
                        cssCache += $"    background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^}}";
                        break;
                    case "Impart.Hex":
                        Hex hex = (Hex)color;
                        cssCache += $"    background-color: #{hex.hex};%^}}";
                        break;
                }
                cssCache += $"%^::-webkit-scrollbar-thumb {{%^";
                switch (colorThumb.GetType().FullName)
                {
                    case "Impart.Rgb":
                        Rgb rgb = (Rgb)colorThumb;
                        cssCache += $"    background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^";
                        break;
                    case "Impart.Hsl":
                        Hsl hsl = (Hsl)colorThumb;
                        cssCache += $"    background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^";
                        break;
                    case "Impart.Hex":
                        Hex hex = (Hex)colorThumb;
                        cssCache += $"    background-color: #{hex.hex};%^";
                        break;
                }
                if (rounded != null)
                {
                    if (rounded < 0)
                    {
                        throw new ScrollbarError("Rounded value cannot be below 0!", this);
                    }
                    cssCache += $"    border-radius: {rounded}px;%^}}";
                }
                else
                {
                    cssCache += "}";
                }
                return;
            }
        }
    } 
}