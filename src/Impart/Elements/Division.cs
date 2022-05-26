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

        /// <value>The attribute values of the Division.</value>
        public List<Attr> Attributes = new List<Attr>();
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }
        internal List<StyleElement> _StyleElements = new List<StyleElement>();
        private List<Element> _Elements = new List<Element>();
        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
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
                Attributes.Add(attribute);
            }
            Changed = true;
            return this;
        }

        /// <summary>Sets a Scrollbar to the Division.</summary>
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
                    Attributes.Add(new Attribute(AttrType.OverflowX, true));
                    break;
                case Axis.Y:
                    Attributes.Add(new Attribute(AttrType.OverflowY, true));
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            _StyleElements.Add(new DivisionScrollbar(scrollbar, _ID, ((StyleElement)(scrollbar)).IOID));
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
            if (_ID != null)
            {
                _ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, _ID));
            }
            StringBuilder result = new StringBuilder("<div ");
            if (Attributes.Count != 0)
            {
                result.Append("style=\"");
                foreach (Attribute attribute in Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in _ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            result.Append('>');
            foreach (Element e in _Elements)
            {
                result.Append(e);
            }
            Render = result.Append("</div>").ToString();
            return Render;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Division result = new Division();
            result.Attributes = Attributes;
            result._Elements = _Elements;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._StyleElements = _StyleElements;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Division result = new Division();
            result.Attributes = Attributes;
            result._Elements = _Elements;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._StyleElements = _StyleElements;
            result.Changed = Changed;
            result.Render = Render;
            return result;
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