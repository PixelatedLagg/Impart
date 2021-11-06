using System;

namespace CSWeb
{
    public class Scrollbar : Element
    {
        internal string textCache;
        internal string cssCache;
        private string id;
        internal string divID;
        public Scrollbar(ICSWeb.Axis axis, string id, ICSWeb.IDType type, int width)
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
            cssCache += $"{this.id}::-webkit-scrollbar {{%^    width: {width}px;%^    background-color: #808080; %^}}%^";
        }
    } 
}