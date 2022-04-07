using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Impart
{
    /// <summary>Link element.</summary>
    public struct Link : Element, Nested
    {
        private Text _text;

        /// <value>The Text value of the Link.</value>
        public Text text 
        {
            get 
            {
                return _text;
            }
        }
        private Image _image;

        /// <value>The Image value of the Link.</value>
        public Image image 
        {
            get 
            {
                return _image;
            }
        }
        private string _id;

        /// <value>The ID value of the Link.</value>
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        private string _path;

        /// <value>The path value of the Link.</value>
        public string path 
        {
            get 
            {
                return _path;
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
        internal Type linkType;

        /// <summary>Creates an empty Link instance.</summary>
        /// <returns>A Link instance.</returns>
        public Link() : this(new Text(), Directory.GetCurrentDirectory()) {}

        /// <summary>Creates a Link instance with <paramref name="text"/> as the Link text, and <paramref name="path"/> as the Link path.</summary>
        /// <returns>A Link instance.</returns>
        /// <param name="text">The Link text.</param>
        /// <param name="path">The Link path.</param>
        /// <param name="id">The Link ID.</param>
        public Link(Text text, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ImpartError("Path cannot be null or empty!");
            }
            _text = text;
            _id = id;
            _path = path;
            _image = new Image();
            _attributes = new List<Attribute>();
            _style = new StringBuilder();
            linkType = typeof(Text);
            if (id == null)
            {
                _attributes.Add(new Attribute(AttributeType.ID, id));
                attributeBuilder = new StringBuilder($" id=\"{id}\"");
            }
            else
            {
                attributeBuilder = new StringBuilder();
            }
        }

        /// <summary>Creates a Link instance with <paramref name="image"/> as the Link image, and <paramref name="path"/> as the Link path.</summary>
        /// <returns>A Link instance.</returns>
        /// <param name="image">The Link image.</param>
        /// <param name="path">The Link path.</param>
        /// <param name="id">The Link ID.</param>
        public Link(Image image, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ImpartError("Path cannot be null or empty!");
            }
            _text = new Text();
            _id = id;
            _path = path;
            _image = image;
            _attributes = new List<Attribute>();
            _style = new StringBuilder();
            linkType = typeof(Image);
            if (id == null)
            {
                _attributes.Add(new Attribute(AttributeType.ID, id));
                attributeBuilder = new StringBuilder($" id=\"{id}\"");
            }
            else
            {
                attributeBuilder = new StringBuilder();
            }
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Link instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Link SetAttribute(AttributeType type, params object[] value)
        {
            Attribute.AddAttribute(ref attributeBuilder, ref _style, ref _attributes, type, value);
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            if (linkType == typeof(Image))
            {
                return $"<a href=\"{path}\"><img src=\"{image.path}\" {image.attributeBuilder.ToString()}{image.style}></a>";
            }
            else
            {
                return $"<a href=\"{path}\"><{text.textType}{text.attributeBuilder.ToString()}{text.style}>{text.text}</{text.textType}></a>";
            }
        }
        string Nested.First()
        {
            if (linkType == typeof(Image))
            {
                return $"<a href=\"{path}\"><img src=\"{image.path}\" {image.attributeBuilder.ToString()}{image.style}>";
            }
            else
            {
                return $"<a href=\"{path}\"><{text.textType}{text.attributeBuilder.ToString()}{text.style}>{text.text}";
            }
        }
        string Nested.Last()
        {
            if (linkType == typeof(Image))
            {
                return "</img></a>";
            }
            else
            {
                return $"</{text.textType}></a>";
            }
        }
    }
}