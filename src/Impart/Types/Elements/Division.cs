using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Division element.</summary>
    public struct Division : Element, IDisposable, Nested
    {
        private string _ID;

        /// <value>The ID value of the Division.</value>
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                Changed = true;
                _ID = value;
            }
        }
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The attribute values of the Division.</value>
        public List<Attribute> Attributes
        {
            get
            {
                return _Attributes;
            }
        }
        private List<(string id, Element element)> _Elements = new List<(string id, Element element)>();
        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        internal StringBuilder _ScrollbarCache = new StringBuilder();
        private bool Changed = true;
        private string Render = "";
        private int IOIDValue = Ioid.Generate();
        int Element.IOID
        {
            get
            {
                return IOIDValue;
            }
        }

        /// <summary>Creates a Division instance.</summary>
        public Division(string id = null)
        {
            _ID = id;
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Division SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            Changed = true;
            return this;
        }

        /// <summary>Add <paramref name="scrollbar"> to the Division.</summary>
        /// <param name="scrollbar">The scrollbar to add.</param>
        public Division AddScrollbar(Scrollbar scrollbar)
        {
            if (_ID == null)
            {
                throw new ImpartError("To add scrollbar, division must have an ID.");
            }
            _ScrollbarCache.Clear();
            switch (scrollbar.axis)
            {
                case Axis.X:
                    _Attributes.Add(new Attribute(AttributeType.OverflowX, true));
                    break;
                case Axis.Y:
                    _Attributes.Add(new Attribute(AttributeType.OverflowY, true));
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            _ScrollbarCache.Append($"#{_ID}::-webkit-scrollbar {{width: {scrollbar.width};background-color: #808080; }}#{_ID}::-webkit-scrollbar-track{{background-color: {scrollbar.bgColor};}}#{_ID}::-webkit-scrollbar-thumb {{background-color: {scrollbar.fgColor};");
            if (scrollbar.radius != null)
            {
                _ScrollbarCache.Append($"border-radius: {scrollbar.radius};}}");
            }
            else
            {
                _ScrollbarCache.Append('}');
            }
            Changed = true;
            return this;
        }

        /// <summary>Dispose of the Division instance.</summary>
        public void Dispose() { }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder("<div");
            if (_Attributes.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attribute attribute in _Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttribute extAttribute in _ExtAttributes)
            {
                result.Append(extAttribute);
            }
            result.Append('>');
            foreach ((string id, Element element) e in _Elements)
            {
                result.Append(e.element);
            }
            Render = result.Append("</div>").ToString();
            return Render;
        }

        string Nested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - ("</div>".Length) - 1);
        }

        string Nested.Last()
        {
            return "</div>";
        }
    } 
}