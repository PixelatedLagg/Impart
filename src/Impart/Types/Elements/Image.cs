using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Image element.</summary>
    public struct Image : Element, Nested
    {
        private StringBuilder _style;
        internal string style 
        {
            get
            {
                if (_style.ToString() == "")
                {
                    return "";
                }
                return $" style=\"{_style.ToString()}\"";
            }
        }
        private string _path;

        /// <value>The path value of the Image.</value>
        public string path 
        {
            get 
            {
                return _path;
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
        private string _id;

        /// <value>The ID value of the Image.</value>
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        internal StringBuilder attributeBuilder;
        internal Type elementType = typeof(Image);

        /// <summary>Creates an empty Image instance.</summary>
        /// <returns>An Image instance.</returns>
        public Image()
        {
            attributeBuilder = new StringBuilder();
            _path = "";
            _style = new StringBuilder();
            _attributes = new List<Attribute>();
            _id = null;
        }
        
        /// <summary>Creates an Image instance with <paramref name="path"/> as the Image path.</summary>
        /// <returns>An Image instance.</returns>
        /// <param name="path">The Image path.</param>
        /// <param name="id">The Image ID.</param>
        public Image(string path, string id = null)
        {
            if (String.IsNullOrEmpty(path)) 
            {
                throw new ImpartError("Path cannot be empty or null!");
            }
            if (id != null)
            {
                attributeBuilder = new StringBuilder($" id=\"{id}\"");
            }
            else
            {
                attributeBuilder = new StringBuilder();
            }
            _path = path;
            _style = new StringBuilder();
            _attributes = new List<Attribute>();
            _id = id;
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>An Image instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Image SetAttribute(AttributeType type, params object[] value)
        {
            Attribute.AddAttribute(ref attributeBuilder, ref _style, ref _attributes, type, value);
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"<img src=\"{path}\"{attributeBuilder.ToString()}{style}>";
        }
        string Nested.First()
        {
            return $"<img src=\"{path}\"{attributeBuilder.ToString()}{style}>";
        }
        string Nested.Last()
        {
            return "</img>";
        }
    } 
}