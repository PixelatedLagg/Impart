using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Button element.</summary>
    public struct Button : Element, Nested
    {
        private List<Attribute> _attributes;

        /// <value>The attribute values of the Button.</value>
        public List<Attribute> attributes
        {
            get 
            {
                return _attributes;
            }
        }

        private string _ID;

        /// <value>The ID value of the Button.</value>
        public string ID 
        {
            get 
            {
                return _ID;
            }
        }
        internal StringBuilder attributeBuilder;
        internal StringBuilder textBuilder;

        /// <summary>Creates an empty Button instance.</summary>
        /// <returns>An Button instance.</returns>
        public Button()
        {
            textBuilder = new StringBuilder();
            _ID = null;
            _style = new StringBuilder();
            _attributes = new List<Attribute>();
            attributeBuilder = new StringBuilder(" type=\"button\">");
        }

        /// <summary>Creates a Button instance with <paramref name="text"/> as the Button text.</summary>
        /// <returns>A Button instance.</returns>
        /// <param name="text">The Button text.</param>
        /// <param name="id">The Button ID.</param>
        public Button(Text text, string id = null)
        {
            textBuilder = new StringBuilder();
            if (text.id == null)
            {
                textBuilder.Append($"<p{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>");
            }
            else
            {
                textBuilder.Append($"<p id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>");
            }
            _id = id;
            _style = new StringBuilder();
            _attributes = new List<Attribute>();
            if (id != null)
            {
                attributeBuilder = new StringBuilder($" type=\"button\"> id=\"{id}\"");
            }
            else
            {
                attributeBuilder = new StringBuilder(" type=\"button\">");
            }
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Button instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Button SetAttribute(AttributeType type, params object[] value)
        {
            _attributes.Add(new Attribute(type, value));
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            if (_attributes.Count != 0)
            {
                StringBuilder style = new StringBuilder();
                foreach (Attribute attribute in _attributes)
                {
                    style.Append(attribute);
                }
                return $"<button style=\"{style.ToString()}\">{textBuilder.ToString()}</button>";
            }
            return $"<button>{textBuilder.ToString()}</button>";
        }
        string Nested.First()
        {
            StringBuilder style = new StringBuilder();
            foreach (Attribute attribute in _attributes)
            {
                style.Append(attribute);
            }
            return $"<button style=\"{style.ToString()}\">{textBuilder.ToString()}</button>";
        }
        string Nested.Last()
        {
            return "</button>";
        }
    }
}