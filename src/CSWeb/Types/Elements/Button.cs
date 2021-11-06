using System;
using System.Drawing;

namespace CSWeb
{
    public class Button : Element
    {
        private string _text;
        private string _id;
        private string _style;
        private string _attributes;
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
        public Button(string text, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ButtonError("Text cannot be null or empty!", null);
            }
            this._text = text;
            this._id = id;
            _style = "";
            _attributes = "";
            colorCheck = 0;
        }
        public Button SetColor(Color color)
        {
            if (colorCheck > 1)
            {
                throw new ButtonError("Cannot set color more than once!", this);
            }
            switch (color.GetType().FullName)
            {
                case "CSWeb.Rgb":
                    Rgb rgb = (Rgb)color;
                    _style += $" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case "CSWeb.Hsl":
                    Hsl hsl = (Hsl)color;
                    _style += $" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case "CSWeb.Hex":
                    Hex hex = (Hex)color;
                    _style += $" color: #{hex.hex};";
                    break;
            }
            colorCheck++;
            return this;
        }
        public Button SetHoverMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ButtonError("Hover message cannot be empty or null!", this);
            }
            _attributes += $" title=\"{message}\"";
            return this;
        }
        public Button SetDraggable(bool obj)
        {
            _attributes += $" draggable=\"{obj}\"";
            return this;
        }
        public Button SetFontSize(int pixels)
        {
            if (pixels < 0)
            {
                throw new ButtonError("Font size cannot be negative!", this);
            }
            _style += $" font-size: {pixels}px;";
            return this;
        }
    } 
}