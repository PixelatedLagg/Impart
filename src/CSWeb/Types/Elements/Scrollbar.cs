using System;

namespace CSWeb
{
    public class Scrollbar : Element
    {
        internal string cssCache;
        private string id;
        internal string divID;
        public Scrollbar(ICSWeb.Axis axis, string id, ICSWeb.IDType type, int width, Color color, Color colorThumb, int? rounded = null)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ScrollbarError("ID cannot be null or empty!", this);
            }
            switch (type)
            {
                case ICSWeb.IDType.ID:
                    this.id = $"#{id.Str()}";
                    divID = $" id=\"{id.Str()}\"";
                    break;
                case ICSWeb.IDType.Class:
                    this.id = $".{id.Str()}";
                    divID = $" class=\"{id.Str()}\"";
                    break;
            }
            cssCache = $"%^{this.id} {{%^";
            switch (axis)
            {
                case ICSWeb.Axis.X:
                    cssCache += "    overflow-x: auto;%^}%^";
                    break;
                case ICSWeb.Axis.Y:
                    cssCache += "    overflow-y: auto;%^}%^";
                    break;
                default:
                    throw new ScrollbarError("Invalid axis!", this);
            }
            if (width <= 0)
            {
                throw new ScrollbarError("Scrollbar width cannot be less than or equal to 0!", this);
            }
            cssCache += $"{this.id}::-webkit-scrollbar {{%^    width: {width}px;%^    background-color: #808080; %^}}%^{this.id}::-webkit-scrollbar-track {{%^";
            switch (color.GetType().FullName)
            {
                case "CSWeb.Rgb":
                    Rgb rgb = (Rgb)color;
                    cssCache += $"    background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^}}";
                    break;
                case "CSWeb.Hsl":
                    Hsl hsl = (Hsl)color;
                    cssCache += $"    background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^}}";
                    break;
                case "CSWeb.Hex":
                    Hex hex = (Hex)color;
                    cssCache += $"    background-color: #{hex.hex};%^}}";
                    break;
            }
            cssCache += $"%^{this.id}::-webkit-scrollbar-thumb {{%^";
            switch (colorThumb.GetType().FullName)
            {
                case "CSWeb.Rgb":
                    Rgb rgb = (Rgb)colorThumb;
                    cssCache += $"    background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^";
                    break;
                case "CSWeb.Hsl":
                    Hsl hsl = (Hsl)colorThumb;
                    cssCache += $"    background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^";
                    break;
                case "CSWeb.Hex":
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
        }
    } 
}