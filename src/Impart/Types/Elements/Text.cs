using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>The Text element.</summary>
    public struct Text : Element
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

        /// <value>The attributes value of the Text.</value>
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
                return $" style=\"{_style.ToString()}\"".Replace("\" ", "\"");
            }
        }
        internal StringBuilder attributeBuilder;

        /// <summary>Creates a Text instance with <paramref name="text"/> as the text.</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="text">The Text text.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ImpartError("Text cannot be null or empty!");
            }
            _text = text;
            _id = id;
            _type = TextType.Regular;
            _attributes = new List<Attribute>();
            _style = new StringBuilder(1000);
            attributeBuilder = new StringBuilder(1000);
        }

        /// <summary>Creates a Text instance with <paramref name="text"/> as the text and <paramref name="type"> as the Text type.</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="text">The Text text.</param>
        /// <param name="type">The Text type.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, TextType type, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ImpartError("Text cannot be null or empty!");
            }
            _text = text;
            _id = id;
            _type = type;
            _attributes = new List<Attribute>();
            _style = new StringBuilder(1000);
            attributeBuilder = new StringBuilder(1000);
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Text SetAttribute(AttributeType type, params object[] value)
        {
            switch (type)
            {
                case AttributeType.BackgroundColor:
                    switch (value[0])
                    {
                        case Rgb rgb:
                            _style.Append($" background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                            break;
                        case Hsl hsl:
                            _style.Append($" background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                            break;
                        case Hex hex:
                            _style.Append($" background-color: #{hex.hex};");
                            break;
                        default:
                            return this;
                    }
                    _attributes.Add(new Attribute(AttributeType.BackgroundColor, value[0]));
                    break;
                case AttributeType.ForegroundColor:
                    switch (value[0])
                    {
                        case Rgb rgb:
                            _style.Append($" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                            break;
                        case Hsl hsl:
                            _style.Append($" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                            break;
                        case Hex hex:
                            _style.Append($" color: #{hex.hex};");
                            break;
                        default:
                            return this;
                    }
                    _attributes.Add(new Attribute(AttributeType.ForegroundColor, value[0]));
                    break;
                case AttributeType.FontSize:
                    switch (value[0])
                    {
                        case Percent pct:
                            _style.Append($" font-size: {pct.percent}%;");
                            break;
                        case Pixels pxls:
                            _style.Append($" font-size: {pxls.pixels}px;");
                            break;
                        default:
                            return this;
                    }
                    _attributes.Add(new Attribute(AttributeType.FontFamily, value[0]));
                    break;
                case AttributeType.Margin:
                    switch (value[0])
                    {
                        case Percent pct:
                            _style.Append($" margin: {pct.percent}%;");
                            break;
                        case Pixels pxls:
                            _style.Append($" margin: {pxls.pixels}px;");
                            break;
                        default:
                            return this;
                    }
                    _attributes.Add(new Attribute(AttributeType.Margin, value[0]));
                    break;
                case AttributeType.HoverMessage:
                    if (value[0] as string != null)
                    {
                        attributeBuilder.Append($" title=\"{value[0]}\"");
                        _attributes.Add(new Attribute(AttributeType.HoverMessage, value[0]));
                    }
                    break;
            }
            return this;
        }
    }
}