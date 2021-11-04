using System;

namespace CSWeb
{
    public class List : Element
    {
        private string[] _entries;
        private string _id;
        private string _style;
        private string _attributes;
        private int colorCheck;
        private bool ordered;
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
        public string[] entries 
        {
            get 
            {
                return _entries;
            }
        }
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        public List(int type, string id = null)
        {
            switch (type)
            {
                case 0:
                    ordered = false;
                    break;
                case 1:
                    ordered = true;
                    break;
                default:
                    throw new ListError("Invalid list type!", this);
            }
            if (String.IsNullOrEmpty(id))
            {
                id = null;
            }
            else
            {
                this._id = id.Str();
            }
            _style = "";
            _attributes = "";
            colorCheck = 0;
        }
    }
}