using System;

namespace Impart
{
    /// <summary>Class that represents a button.</summary>
    public struct Button : Element
    {
        private string _text;
        private string _id;
        private string _style;
        private string _attributes;
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
                throw new ImpartError("Text cannot be null or empty!");
            }
            this._text = text;
            this._id = id;
            _style = "";
            _attributes = "";
            setProperties = new bool[] {false, false, false, false, false, false};
        }

        /// <summary>Constructor for the button class.</summary>
        public Button(Text text, string id = null)
        {
            _id = "";
            _text = "";
            if (text.id == null)
            {
                _text += $"    <p{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>";
            }
            else
            {
                _text += $"    <p id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>";
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
            setProperties = new bool[] {false, false, false, false, false, false};
        }
    }
}