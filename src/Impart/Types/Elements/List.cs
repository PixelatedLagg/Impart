using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>List element.</summary>
    public struct List : Element, Nested
    {
        private string _id;

        /// <value>The ID value of the List.</value>
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        private Dictionary<int, Text> _entries;

        /// <value>The entry values of the List.</value>
        public Dictionary<int, Text> entries 
        {
            get 
            {
                return _entries;
            }
        }
        private ListType _type;

        /// <value>The type value of the List.</value>
        public ListType type
        {
            get
            {
                return _type;
            }
        }
        private List<Attribute> _attributes;

        /// <value>The attribute values of the List.</value>
        public List<Attribute> attributes
        {
            get 
            {
                return _attributes;
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
        internal StringBuilder textBuilder;
        private string listType;

        /// <summary>Creates a List instance with <paramref name="text"/> as the List type, and Text[] as the entries.</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="type">The List type.</param>
        /// <param name="id">The List ID.</param>
        /// <param name="textEntries">The List entries.</param>
        public List(ListType type, string id = null, params Text[] textEntries)
        {
            textBuilder = new StringBuilder(1000);
            _entries = new Dictionary<int, Text>();
            _type = type;
            _attributes = new List<Attribute>();
            _style = new StringBuilder();
            _id = id;
            if (type == ListType.Ordered)
            {
                listType = "ol";
            }
            else
            {
                listType = "ul";
            }
            if (id != null)
            {
                _attributes.Add(new Attribute(AttributeType.ID, id));
                attributeBuilder = new StringBuilder($" id=\"{id}\"");
            }
            else
            {
                attributeBuilder = new StringBuilder();
            }
            foreach (Text text in textEntries)
            {
                textBuilder.Append($"<li><p{text.attributeBuilder.ToString()}{text.style}>{text.text}</p></li>");
                _entries.Add(_entries.Count, text);
            }
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A List instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public List SetAttribute(AttributeType type, params object[] value)
        {
            Attribute.AddAttribute(ref attributeBuilder, ref _style, ref _attributes, type, value);
            return this;
        }
        internal string Render()
        {
            return $"<{listType}{attributeBuilder.ToString()}{style}>{textBuilder.ToString()}</{listType}>";
        }
        string Nested.First()
        {
            return $"<{listType}{attributeBuilder.ToString()}{style}>{textBuilder.ToString()}";
        }
        string Nested.Last()
        {
            return $"</{listType}>";
        }
    }
}