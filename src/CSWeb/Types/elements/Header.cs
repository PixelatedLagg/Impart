using System;

namespace CSWeb
{
    public class Header : Element
    {
        private string _text;
        private string _id;
        private string _style;
        private string _attributes;
        internal int num;
        private int colorCheck;
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
        public Header(int num, string text, string id = null)
        {
            if (num > 5 || num < 1)
            {
                throw new HeaderError("Number must be between 1-5!", this);
            }
            if (String.IsNullOrEmpty(text))
            {
                throw new HeaderError("Text cannot be null or empty!", null);
            }
            this._text = text;
            this._id = id;
            this.num = num;
        }
        public Header AddColor(Hex hex)
        {
            if (colorCheck > 1)
            {
                throw new HeaderError("Cannot set color more than once!", this);
            }
            colorCheck++;
            _style += $" color: #{hex.hex};";
            return this;
        }
        public Header AddColor(Hsl hsl)
        {
            if (colorCheck > 1)
            {
                throw new HeaderError("Cannot set color more than once!", this);
            }
            colorCheck++;
            _style += $" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
            return this;
        }
        public Header AddColor(Rgb rgb)
        {
            if (colorCheck > 1)
            {
                throw new HeaderError("Cannot set color more than once!", this);
            }
            colorCheck++;
            _style += $" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
            return this;
        }
        public Header SetHoverMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new HeaderError("Hover message cannot be empty or null!", this);
            }
            _attributes += $" title=\"{message}\"";
            return this;
        }
        public Header SetFontSize(int pixels)
        {
            if (pixels < 0)
            {
                throw new HeaderError("Invalid font size!", this);
            }
            _style += $" font-size: {pixels}px;";
            return this;
        }
    } 
}