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
    } 
}