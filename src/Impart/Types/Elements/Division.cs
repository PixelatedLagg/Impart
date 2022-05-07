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
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }
        private List<Element> _Elements = new List<Element>();
        private List<StyleElement> _StyleElements = new List<StyleElement>();
        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a Division instance.</summary>
        public Division()
        {
            _ID = null;
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

        /// <summary>Add a Table to the Division.</summary>
        /// <param name="table">The Table instance to add.</param>
        public Division AddTable(Table table)
        {
            _Elements.Add(table);
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
            foreach (Attribute attribute in style.Attributes)
            {
                _Attributes.Add(attribute);
            }
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
            switch (scrollbar.Axis)
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
            _StyleElements.Add(new DivisionScrollbar(scrollbar, _ID, ((StyleElement)(scrollbar)).IOID));
            Changed = true;
            return this;
        }

        //
        public Division AddAnimation(Animation animation)
        {
            _StyleElements.Add(animation);
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
            return result.Remove(result.Length - 6);
        }

        string Nested.Last()
        {
            return "</div>";
        }
    } 
}