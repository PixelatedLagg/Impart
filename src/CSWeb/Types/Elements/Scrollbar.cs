using System;

namespace CSWeb
{
    public class Scrollbar : Element
    {
        internal string textCache;
        internal string cssCache;
        private string id;
        public Scrollbar(ICSWeb.Axis axis, string id, ICSWeb.IDType type)
        {
            cssCache = "";
            if (String.IsNullOrEmpty(id))
            {
                throw new ScrollbarError("ID cannot be null or empty!", this);
            }
            switch (type)
            {
                case ICSWeb.IDType.ID:
                    this.id = $"#{id.Str()}";
                    break;
                case ICSWeb.IDType.Class:
                    this.id = $".{id.Str()}";
                    break;
            }
            switch (axis)
            {
                case ICSWeb.Axis.X:
                    cssCache += "    overflow-x: auto;%^";
                    break;
                case ICSWeb.Axis.Y:
                    cssCache += "    overflow-y: auto;%^";
                    break;
                default:
                    throw new ScrollbarError("Invalid axis!", this);
            }
        }
    } 
}