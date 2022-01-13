using System;

namespace Impart
{
    /// <summary>Class that represents a header.</summary>
    public struct Header : Element
    {
        private string _text;
        private string _id;
        private string _style;
        private string _attributes;
        internal int num;
        private bool[] setProperties;
        internal string attributes 
        {
            get
            {
                return _attributes;
            }
        }
        internal string style 
        {
            get
            {
                if (_style == "")
                {
                    return "";
                }
                return $" style=\"{_style}\"".Replace("\" ", "\"");
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

        /// <summary>Constructor for the header class.</summary>
        public Header(int num, string text, string id = null)
        {
            if (num > 5 || num < 1)
            {
                throw new ImpartError("Number must be between 1-5!");
            }
            if (String.IsNullOrEmpty(text))
            {
                throw new ImpartError("Text cannot be null or empty!");
            }
            this._text = text;
            this._id = id;
            this.num = num;
            _style = "";
            _attributes = "";
            setProperties = new bool[] {false, false, false};
        }

        /// <summary>Method for setting the header color.</summary>
        public Header SetColor(Color color)
        {
            if (setProperties[0])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            setProperties[0] = true;
            switch (color)
            {
                case Rgb rgb:
                    _style += $" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case Hsl hsl:
                    _style += $" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case Hex hex:
                    _style += $" color: #{hex.hex};";
                    break;
            }
            return this;
        }

        /// <summary>Method for setting the header hover message.</summary>
        public Header SetHoverMessage(string message)
        {
            if (setProperties[1])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            setProperties[1] = true;
            if (String.IsNullOrEmpty(message))
            {
                throw new ImpartError("Hover message cannot be empty or null!");
            }
            _attributes += $" title=\"{message}\"";
            return this;
        }

        /// <summary>Method for setting the header font size.</summary>
        public Header SetFontSize(int pixels)
        {
            if (setProperties[2])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            setProperties[2] = true;
            if (pixels < 0)
            {
                throw new ImpartError("Invalid font size!");
            }
            _style += $" font-size: {pixels}px;";
            return this;
        }
    } 
}