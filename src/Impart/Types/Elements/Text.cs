using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Text element.</summary>
    public struct Text : Element, Nested
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
        private TextType _type;

        /// <value>The type value of the Text.</value>
        public TextType type
        {
            get
            {
                return _type;
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
        internal string textType;

        /// <summary>Creates an empty Text instance.</summary>
        /// <returns>A Text instance.</returns>
        public Text() : this("") {}

        /// <summary>Creates a Text instance with <paramref name="text"/> as the text.</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="text">The Text text.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, string id = null)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _text = text;
            _id = id;
            _type = TextType.Regular;
            _attributes = new List<Attribute>();
            _style = new StringBuilder();
            textType = "p";
            if (id != null)
            {
                _attributes.Add(new Attribute(AttributeType.ID, id));
                attributeBuilder = new StringBuilder($" id=\"{id}\"");
            }
            else
            {
                attributeBuilder = new StringBuilder();
            }
        }

        /// <summary>Creates a Text instance with <paramref name="text"/> as the text and <paramref name="type"> as the Text type.</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="text">The Text text.</param>
        /// <param name="type">The Text type.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, TextType type, string id = null)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _text = text;
            _id = id;
            _type = type;
            _attributes = new List<Attribute>();
            _style = new StringBuilder();
            switch (type)
            {
                case TextType.Regular:
                    textType = "p";
                    break;
                case TextType.Bold:
                    textType = "b";
                    break;
                case TextType.Delete:
                    textType = "del";
                    break;
                case TextType.Emphasize:
                    textType = "em";
                    break;
                case TextType.Important:
                    textType = "strong";
                    break;
                case TextType.Insert:
                    textType = "ins";
                    break;
                case TextType.Italic:
                    textType = "i";
                    break;
                case TextType.Mark:
                    textType = "mark";
                    break;
                case TextType.Small:
                    textType = "small";
                    break;
                case TextType.Subscript:
                    textType = "sub";
                    break;
                case TextType.Superscript:
                    textType = "sup";
                    break;
                default:
                    textType = "";
                    break;
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
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Text SetAttribute(AttributeType type, params object[] value)
        {
            Attribute.AddAttribute(ref attributeBuilder, ref _style, ref _attributes, type, value);
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"<{textType}{attributeBuilder.ToString()}{style}>{text}</{textType}>";
        }
        string Nested.First()
        {
            return $"<{textType}{attributeBuilder.ToString()}{style}>{text}";
        }
        string Nested.Last()
        {
            return $"</{textType}>";
        }
    }
}