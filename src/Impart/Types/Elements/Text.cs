using System;

namespace Impart
{
    /// <summary>Class that represents text.</summary>
    public struct Text : Element
    {
        private bool[] setProperties;
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

        /// <summary>Constructor for the text class.</summary>
        public Text(string text, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new TextError("Text cannot be null or empty!");
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
                throw new TextError("Cannot set properties twice!");
            }
            if (color == null)
            {
                throw new ColorError("Color cannot be null!");
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
            return this;
        }

        /// <summary>Method for setting the text margin.</summary>
        public Text SetMargin(Measurement measurement)
        {
            if (setProperties[1])
            {
                throw new TextError("Cannot set properties twice!");
            }
            setProperties[1] = true;
            switch (measurement.GetType().FullName)
            {
                case "Impart.Percent":
                    _style += $" margin: {measurement.Pct().percent}%;";
                    break;
                case "Impart.Pixels":
                    _style += $" margin: {measurement.Px().pixels}px;";
                    break;
            }
            return this;
        }

        /// <summary>Method for setting the text hover message.</summary>
        public Text SetHoverMessage(string message)
        {
            if (setProperties[2])
            {
                throw new TextError("Cannot set properties twice!");
            }
            setProperties[2] = true;
            if (String.IsNullOrEmpty(message))
            {
                throw new TextError("Hover message cannot be empty or null!");
            }
            _attributes += $" title=\"{message.Str()}\"";
            return this;
        }

        /// <summary>Method for setting the text font size.</summary>
        public Text SetFontSize(Measurement measurement)
        {
            if (setProperties[3])
            {
                throw new TextError("Cannot set properties twice!");
            }
            setProperties[3] = true;
            switch (measurement.GetType().FullName)
            {
                case "Impart.Percent":
                    _style += $" font-size: {measurement.Pct().percent}%;";
                    break;
                case "Impart.Pixels":
                    _style += $" font-size: {measurement.Px().pixels}px;";
                    break;
            }
            return this;
        }
    }
}