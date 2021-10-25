using System;

namespace Csweb
{
    public class Header
    {
        private string _text;
        private string _id;
        internal int num;
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
    } 
}