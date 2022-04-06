using System.Collections.Generic;
using System.Text;
using System;

namespace Impart
{
    /// <summary>Division element.</summary>
    public struct Division : Element, IDisposable
    {
        private string _id;

        /// <value>The ID value of the List.</value>
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        private List<Attribute> _attributes;

        /// <value>The attribute values of the List.</value>
        public List<Attribute> attributes
        {
            get 
            {
                return _attributes;
            }
        }
        private StringBuilder _style;
        internal string style 
        {
            get
            {
                if (_style.Length == 0)
                {
                    return "";
                }
                return $" style=\"{_style.ToString()}\"";
            }
        }
        internal StringBuilder attributeBuilder;
        internal StringBuilder textBuilder;
        internal StringBuilder webPageStyleBuilder;
        internal Type elementType = typeof(Division);

        /// <summary>Creates a Division instance.</summary>
        /// <returns>A Division instance.</returns>
        public Division(string id = null)
        {
            if (id == null)
            {
                attributeBuilder = new StringBuilder();
            }
            else
            {
                attributeBuilder = new StringBuilder($" id=\"{id}\"");
            }
            _id = id;
            _attributes = new List<Attribute>();
            _style = new StringBuilder("overflow-x: auto; overflow-y: auto;");
            textBuilder = new StringBuilder();
            webPageStyleBuilder = new StringBuilder();
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Division instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Division SetAttribute(AttributeType type, params object[] value)
        {
            Attribute.AddAttribute(ref attributeBuilder, ref _style, ref _attributes, type, value);
            return this;
        }

        /// <summary>Add <paramref name="scrollbar"> to the Division.</summary>
        /// <returns>A Division instance.</returns>
        /// <param name="scrollbar">The scrollbar to add.</param>
        public Division AddScrollbar(Scrollbar scrollbar)
        {
            if (id == null)
            {
                throw new ImpartError("To add scrollbar, division must have an ID set.");
            }
            switch (scrollbar.axis)
            {
                case Axis.X:
                    _style.Append("overflow-x: auto;");
                    break;
                case Axis.Y:
                    _style.Append("overflow-y: auto;");
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            switch (scrollbar.width)
            {
                case Percent pct:
                    webPageStyleBuilder.Append($"{this.id}::-webkit-scrollbar {{width: {scrollbar.width}%;background-color: #808080; }}{this.id}::-webkit-scrollbar-track {{");
                    break;
                case Pixels pxls:
                    webPageStyleBuilder.Append($"{this.id}::-webkit-scrollbar {{width: {scrollbar.width}px;background-color: #808080; }}{this.id}::-webkit-scrollbar-track {{");
                    break;
            }
            switch (scrollbar.bgColor)
            {
                case Rgb rgb:
                    webPageStyleBuilder.Append($"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});}}");
                    break;
                case Hsl hsl:
                    webPageStyleBuilder.Append($"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);}}");
                    break;
                case Hex hex:
                    webPageStyleBuilder.Append($"background-color: #{hex.hex};}}");
                    break;
            }
            webPageStyleBuilder.Append($"{this.id}::-webkit-scrollbar-thumb {{");
            switch (scrollbar.fgColor)
            {
                case Rgb rgb:
                    webPageStyleBuilder.Append($"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                    break;
                case Hsl hsl:
                    webPageStyleBuilder.Append($"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                    break;
                case Hex hex:
                    webPageStyleBuilder.Append($"background-color: #{hex.hex};");
                    break;
            }
            if (scrollbar.radius != null)
            {
                switch (scrollbar.radius)
                {
                    case Percent pct:
                        webPageStyleBuilder.Append($"border-radius: {scrollbar.radius}%;}}");
                        break;
                    case Pixels pxls:
                        webPageStyleBuilder.Append($"border-radius: {scrollbar.radius}px;}}");
                        break;
                }
            }
            else
            {
                webPageStyleBuilder.Append("}");
            }
            return this;
        }

        /// <summary>Dispose of the Division instance.</summary>
        public void Dispose()
        {
            //only implemented to allow easy scope management for the dev
        }
        internal string Render()
        {
            return $"<div{attributeBuilder.ToString()}{style}>{textBuilder.ToString()}</div>";
        }
    } 
}