using System;
using System.Text;
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
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The Attribute values of the Header.</value>
        public List<Attribute> Attributes
        {
            get 
            {
                return _Attributes;
            }
        }
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
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
            _Text = text;
            _ID = id;
            _Number = number;
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Header SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            Changed = true;
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder($"<h{_Number}");
            if (_Attributes.Count != 0)
            {
                result.Append("style\")");
                foreach (Attribute attribute in _Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
                return Render;
            }
            foreach (ExtAttribute extAttribute in _ExtAttributes)
            {
                result.Append(extAttribute);
            }
            Render = result.Append($">{_Text}</h{_Number}>").ToString();
            return Render;
        }

        string Nested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - ($"</h1>".Length) - 1);
        }
        
        string Nested.Last()
        {
            return $"</h{_Number}>";
        }
    } 
}