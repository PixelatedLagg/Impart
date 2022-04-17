using System.Text;
using System.Collections.Generic;
using System;

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
        }
        private string _ID = null;

        /// <value>The ID value of the Header.</value>
        public string ID 
        {
            get 
            {
                return _ID;
            }
        }
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The Attribute values of the Head.</value>
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
                Changed = true;
                _Number = value;
            }
        }
        private bool Changed = false;
        private string Render;

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
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Header SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"<h{_Number}{attributeBuilder.ToString()}{style}>{text}</h{number}>";
        }
        string Nested.First()
        {
            return $"<h{_Number}{attributeBuilder.ToString()}{style}>{text}";
        }
        string Nested.Last()
        {
            return $"</h{_Number}>";
        }
    } 
}