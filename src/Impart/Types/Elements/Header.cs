using System.Text;
using System.Collections.Generic;
using System;

namespace Impart
{
    /// <summary>Header element.</summary>
    public struct Header : Element, Nested
    {
        private string _text;

        /// <value>The text value of the Text.</value>
        public string text 
        {
            get 
            {
                return _text;
            }
        }
        private string _id;

        /// <value>The ID value of the Text.</value>
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        private List<Attribute> _attributes;

        /// <value>The attribute values of the Text.</value>
        public List<Attribute> attributes
        {
            get 
            {
                return _attributes;
            }
        }
        private int _number;

        /// <value>The Header number value of the Header.</value>
        public int number 
        {
            get 
            {
                return _number;
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
        internal Type elementType = typeof(Header);

        /// <summary>Creates an empty Header instance.</summary>
        /// <returns>An Header instance.</returns>
        public Header()
        {
            attributeBuilder = new StringBuilder();
            _text = "";
            _id = null;
            _attributes = new List<Attribute>();
            _style = new StringBuilder();
            _number = 1;   
        }

        /// <summary>Creates a Header instance with <paramref name="text"/> as the text and <paramref name="number"> as the Header number.</summary>
        /// <returns>A Header instance.</returns>
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
                attributeBuilder = new StringBuilder($"id=\"{id}\"");
            }
            else
            {
                attributeBuilder = new StringBuilder();
            }
            _text = text;
            _id = id;
            _attributes = new List<Attribute>();
            _style = new StringBuilder();
            _number = number;
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Header instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Header SetAttribute(AttributeType type, params object[] value)
        {
            Attribute.AddAttribute(ref attributeBuilder, ref _style, ref _attributes, type, value);
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"<h{number}{attributes}{style}>{text}</h{number}>";
        }
        string Nested.First()
        {
            return $"<h{number}{attributes}{style}>{text}";
        }
        string Nested.Last()
        {
            return $"</h{number}>";
        }
    } 
}