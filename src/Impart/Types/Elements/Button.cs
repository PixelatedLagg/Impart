using System;

namespace Impart
{
    /// <summary>Class that represents a button.</summary>
    public class Button : Element
    {
        private string _text;
        private string _id;
        private string _style;
        private string _attributes;
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

        /// <summary>Constructor for the button class.</summary>
        public Button(string text, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ButtonError("Text cannot be null or empty!", null);
            }
            this._text = text.Str();
            this._id = id;
            _style = "";
            _attributes = "";
        }

        /// <summary>Constructor for the button class.</summary>
        public Button(Text text, string id = null)
        {
            if (text.id == null)
            {
                _text += $"%^    <p{text.attributes}{text.style}>{text.text}</p>%^";
            }
            else
            {
                _text += $"%^    <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>%^";
            }
            if (String.IsNullOrEmpty(id))
            {
                id = null;
            }
            else
            {
                _id = id;
            }
            _style = "";
            _attributes = "";
        }

        /// <summary>Method for setting the button text color.</summary>
        public Button SetTextColor(Color color)
        {
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
            return this;
        }

        /// <summary>Method for setting the button background color.</summary>
        public Button SetBGColor(Color color)
        {
            switch (color.GetType().FullName)
            {
                case "Impart.Rgb":
                    Rgb rgb = (Rgb)color;
                    _style += $" background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case "Impart.Hsl":
                    Hsl hsl = (Hsl)color;
                    _style += $" background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case "Impart.Hex":
                    Hex hex = (Hex)color;
                    _style += $" background-color: #{hex.hex};";
                    break;
            }
            return this;
        }

        /// <summary>Method for setting the button margin.</summary>
        public Button SetMargin(int pixels)
        {
            if (pixels < 0)
            {
                throw new ButtonError("Invalid margin value!", this);
            }
            _style += $" margin: {pixels}px;";
            return this;
        }

        /// <summary>Method for setting the button hover message.</summary>
        public Button SetHoverMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ButtonError("Hover message cannot be empty or null!", this);
            }
            _attributes += $" title=\"{message.Str()}\"";
            return this;
        }

        /// <summary>Method for setting the button font size.</summary>
        public Button SetFontSize(int pixels)
        {
            if (pixels < 0)
            {
                throw new ButtonError("Invalid font size!", this);
            }
            _style += $" font-size: {pixels}px;";
            return this;
        }

        /// <summary>Method for setting the division border.</summary>
        public Button SetBorder(int pixels, string border, Color color, int roundedPixels = 0)
        {
            Timer.StartTimer();
            if (!Border.Any(border))
            {
                throw new ButtonError("Invalid border value!", this);
            }
            switch (color.GetType().FullName)
            {
                case "Impart.Rgb":
                    Rgb rgb = (Rgb)color;
                    _style += $" border: {pixels}px {border} rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case "Impart.Hsl":
                    Hsl hsl = (Hsl)color;
                    _style += $" border: {pixels}px {border} hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case "Impart.Hex":
                    Hex hex = (Hex)color;
                    _style += $" border: {pixels}px {border} #{hex};";
                    break;
            }
            if (roundedPixels > 0)
            {
                _style += $" border-radius: {roundedPixels}px;";
            }
            return this;
        }
    }
}