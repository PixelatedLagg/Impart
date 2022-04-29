using System;
using System.Text;
using Impart.Internal;
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
        internal StringBuilder _ScrollbarCache = new StringBuilder();
        private List<Element> _Elements = new List<Element>();
        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
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
        /// <param name="id">The optional ID for the Division.</param>
        public Division(string id = null)
        {
            _ID = id;
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
        }

        /// <summary>Add a Text to the Division.</summary>
        /// <param name="text">The Text instance to add.</param>
        public Division AddText(Text text)
        {
            _Elements.Add(text);
            Changed = true;
            return this;
        }

        /// <summary>Add a Image to the Division.</summary>
        /// <param name="image">The Image instance to add.</param>
        public Division AddImage(Image image)
        {
            _Elements.Add(image);
            Changed = true;
            return this;
        }

        /// <summary>Add a Header to the Division.</summary>
        /// <param name="header">The Header instance to add.</param>
        public Division AddHeader(Header header)
        {
            _Elements.Add(header);
            Changed = true;
            return this;
        }

        /// <summary>Add a Link to the Division.</summary>
        /// <param name="link">The Link instance to add.</param>
        public Division AddLink(Link link)
        {
            _Elements.Add(link);
            Changed = true;
            return this;
        }

        /// <summary>Add a Division to the Division.</summary>
        /// <param name="division">The Division instance to add.</param>
        public Division AddDivision(Division division)
        {
            _Elements.Add(division);
            Changed = true;
            return this;
        }

        /// <summary>Add a Form to the Division.</summary>
        /// <param name="form">The Form instance to add.</param>
        public Division AddForm(Form form)
        {
            _Elements.Add(form);
            Changed = true;
            return this;
        }

        /// <summary>Add a Button to the Division.</summary>
        /// <param name="button">The Button instance to add.</param>
        public Division AddButton(Button button)
        {
            _Elements.Add(button);
            Changed = true;
            return this;
        }

        /// <summary>Add a Nest to the Division.</summary>
        /// <param name="nest">The Nest instance to add.</param>
        public Division AddNest(Nest nest)
        {
            _Elements.Add(nest);
            Changed = true;
            return this;
        }

        /// <summary>Sets a Style to the Division.</summary>
        /// <param name="style">The Style instance to use.</param>
        public Division SetStyle(Style style)
        {
            /*foreach statement to iterate over each attribute in style (have yet to implement style)*/
            Changed = true;
            return this;
        }

        /// <summary>Sets an Attribute of the instance.</summary>
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
        public Division SetScrollbar(Scrollbar scrollbar)
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
            StringBuilder result = new StringBuilder("<div ");
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
            foreach (Element e in _Elements)
            {
                result.Append(e);
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