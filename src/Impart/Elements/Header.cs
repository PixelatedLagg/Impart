using System;
using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Header element.</summary>
    public struct Header : Element, Nested
    {
        private string _Text = "";

        /// <value>The text value of the Header.</value>
        public string TextValue
        {
            get
            {
                return _Text;
            }
            set
            {
                Changed = true;
                _Text = value;
            }
        }
        private string _ID = null;

        /// <value>The ID value of the Header.</value>
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

        /// <value>The Attribute values of the Header.</value>
        public AttrList Attrs = new AttrList();
        private int _Number = 1;

        /// <value>The Header number value of the Header.</value>
        public int Number 
        {
            get 
            {
                return _Number;
            }
            set
            {
                if (value > 5 || value < 1)
                {
                    throw new ImpartError("Header number must be between 1 and 5!");
                }
                Changed = true;
                _Number = value;
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
        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates an empty Header instance.</summary>
        public Header() : this("") { }

        /// <summary>Creates a Header instance.</summary>
        /// <param name="text">The Header text.</param>
        /// <param name="number">The Header type.</param>
        /// <param name="id">The Header ID.</param>
        public Header(string text, int number = 1, string id = null)
        {
            if (number > 5 || number < 1)
            {
                throw new ImpartError("Header number must be between 1 and 5.");
            }
            if (String.IsNullOrEmpty(text))
            {
                throw new ImpartError("Text cannot be null or empty.");
            }
            _Text = text;
            _ID = id;
            _Number = number;
            if (id != null)
            {
                _ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, id));
            }
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed && !Attrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            StringBuilder result = new StringBuilder($"<h{_Number}");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in _ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            Render = result.Append($">{_Text}</h{_Number}>").ToString();
            return Render;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Header result = new Header();
            result.Attrs = Attrs;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Number = _Number;
            result._Text = _Text;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Header result = new Header();
            result.Attrs = Attrs;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Number = _Number;
            result._Text = _Text;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        string Nested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - 5);
        }
        
        string Nested.Last()
        {
            return $"</h{_Number}>";
        }
    } 
}