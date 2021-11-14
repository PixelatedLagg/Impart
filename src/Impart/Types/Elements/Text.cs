using System;

namespace Impart
{
    /// <summary>Class that represents text.</summary>
    public class Text : Element
    {
        private bool[] setProperties;
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
        internal string text 
        {
            get 
            {
                return _text;
            }
        }
        internal string id 
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
                throw new TextError("Text cannot be null or empty!", null);
            }
            this._text = text.Str();
            this._id = id;
            _style = "";
            _attributes = "";
            setProperties = new bool[] {false, false, false, false};
        }

        /// <summary>Method for setting the text color.</summary>
        public Text SetColor(Color color)
        {
            if (setProperties[0])
            {
                throw new TextError("Cannot set properties twice!", this);
            }
            setProperties[0] = true;
            switch (color.GetType().FullName)
            {
                case "Impart.Rgb":
                    Rgb rgb = (Rgb)color;
                    _style += $" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case "Impart.Hsl":
                    Hsl hsl = (Hsl)color;
                    _style += $" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case "Impart.Hex":
                    Hex hex = (Hex)color;
                    _style += $" color: #{hex.hex};";
                    break;
            }
            colorCheck++;
            return this;
        }

        /// <summary>Method for setting the text margin.</summary>
        public Text SetMargin(int pixels)
        {
            if (setProperties[1])
            {
                throw new TextError("Cannot set properties twice!", this);
            }
            setProperties[1] = true;
            if (pixels < 0)
            {
                throw new TextError("Invalid margin value!", this);
            }
            _style += $" margin: {pixels}px;";
            return this;
        }

        /// <summary>Method for setting the text hover message.</summary>
        public Text SetHoverMessage(string message)
        {
            if (setProperties[2])
            {
                throw new TextError("Cannot set properties twice!", this);
            }
            setProperties[2] = true;
            if (String.IsNullOrEmpty(message))
            {
                throw new TextError("Hover message cannot be empty or null!", this);
            }
            _attributes += $" title=\"{message.Str()}\"";
            return this;
        }

        /// <summary>Method for setting the text font size.</summary>
        public Text SetFontSize(int pixels)
        {
            if (setProperties[3])
            {
                throw new TextError("Cannot set properties twice!", this);
            }
            setProperties[3] = true;
            if (pixels < 0)
            {
                throw new TextError("Invalid font size!", this);
            }
            _style += $" font-size: {pixels}px;";
            return this;
        }
    }
}