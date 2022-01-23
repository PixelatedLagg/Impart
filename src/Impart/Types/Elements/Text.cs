using System;
using System.Text;

namespace Impart
{
    /// <summary>
    /// Represents the base of a WebSocket-based Discord client.
    /// </summary>
    public struct Text : Element
    {
        private string _text;
        private string _id;
        private StringBuilder attributeBuilder;
        private StringBuilder styleBuilder;
        private TextType _type;
        public TextType type
        {
            get
            {
                return _type;
            }
        }
        internal string attributes 
        {
            get
            {
                return attributeBuilder.ToString();
            }
        }
        internal string style 
        {
            get
            {
                if (styleBuilder.Length == 0)
                {
                    return "";
                }
                return $" style=\"{styleBuilder.ToString()}\"".Replace("\" ", "\"");
            }
        }
        public string text 
        {
            get 
            {
                return _text;
            }
        }
        public string id 
        {
            get 
            {
                return _id;
            }
        }

        /// <summary>Constructor for the text class.</summary>
        public Text(string text, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ImpartError("Text cannot be null or empty!");
            }
            _text = text.Str();
            _id = id;
            styleBuilder = new StringBuilder(1000);
            attributeBuilder = new StringBuilder(1000);
            _type = TextType.Regular;
        }

        public Text(string text, TextType type, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ImpartError("Text cannot be null or empty!");
            }
            _text = text.Str();
            _id = id;
            styleBuilder = new StringBuilder(1000);
            attributeBuilder = new StringBuilder(1000);
            _type = type;
        }

        private void WriteStyle(string text)
        {
            if (styleBuilder.Length + text.Length > styleBuilder.Capacity)
            {
                styleBuilder.Capacity += 1000;
            }
            styleBuilder.Append(text);
        }

        /// <summary>Method for setting the text color.</summary>
        public Text SetColor(Color color)
        {
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            switch (color)
            {
                case Rgb rgb:
                    WriteStyle($" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                    break;
                case Hsl hsl:
                    WriteStyle($" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                    break;
                case Hex hex:
                    WriteStyle($" color: #{hex.hex};");
                    break;
            }
            return this;
        }

        /// <summary>Method for setting the text margin.</summary>
        public Text SetMargin(Measurement measurement)
        {
            switch (measurement)
            {
                case Percent pct:
                    WriteStyle($" margin: {measurement.Pct().percent}%;");
                    break;
                case Pixels pxls:
                    WriteStyle($" margin: {measurement.Px().pixels}px;");
                    break;
            }
            return this;
        }

        /// <summary>Method for setting the text hover message.</summary>
        public Text SetHoverMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ImpartError("Hover message cannot be empty or null!");
            }
            attributeBuilder.Append($" title=\"{message.Str()}\"");
            return this;
        }

        /// <summary>Method for setting the text font size.</summary>
        public Text SetFontSize(Measurement measurement)
        {
            switch (measurement)
            {
                case Percent pct:
                    WriteStyle($" font-size: {measurement.Pct().percent}%;");
                    break;
                case Pixels pxls:
                    WriteStyle($" font-size: {measurement.Px().pixels}px;");
                    break;
            }
            return this;
        }
    }
}