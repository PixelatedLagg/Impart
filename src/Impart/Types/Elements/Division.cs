using System.Collections.Generic;
using System.Text;
using System;

namespace Impart
{
    /// <summary>Division element.</summary>
    public struct Division : Element, IDisposable
    {
        private string _ID;

        /// <value>The ID value of the List.</value>
        public string ID
        {
            get
            {
                return _ID;
            }
        }
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The attribute values of the List.</value>
        public List<Attribute> Attributes
        {
            get
            {
                return _Attributes;
            }
        }
        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a Division instance.</summary>
        /// <returns>A Division instance.</returns>
        public Division(string id = null)
        {
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
            _ID = id;
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Division instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Division SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            Changed = true;
            return this;
        }

        /// <summary>Add <paramref name="scrollbar"> to the Division.</summary>
        /// <returns>A Division instance.</returns>
        /// <param name="scrollbar">The scrollbar to add.</param>
        public Division AddScrollbar(Scrollbar scrollbar)
        {
            if (_ID == null)
            {
                throw new ImpartError("To add scrollbar, division must have an ID.");
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
                    webPageStyleBuilder.Append($"background-color: {rgb};}}");
                    break;
                case Hsl hsl:
                    webPageStyleBuilder.Append($"background-color: {hsl};}}");
                    break;
                case Hex hex:
                    webPageStyleBuilder.Append($"background-color: {hex};}}");
                    break;
            }
            webPageStyleBuilder.Append($"{this.id}::-webkit-scrollbar-thumb {{");
            switch (scrollbar.fgColor)
            {
                case Rgb rgb:
                    webPageStyleBuilder.Append($"background-color: {rgb};");
                    break;
                case Hsl hsl:
                    webPageStyleBuilder.Append($"background-color: {hsl};");
                    break;
                case Hex hex:
                    webPageStyleBuilder.Append($"background-color: {hex};");
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
        public void Dispose() { }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"<div{attributeBuilder.ToString()}{style}>{textBuilder.ToString()}</div>";
        }
    } 
}