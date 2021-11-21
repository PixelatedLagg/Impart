using System;

namespace Impart
{
    /// <summary>Class that represents a link.</summary>
    public struct Link : Element
    {
        private Text _text;
        private Image _image;
        private string _path;
        private string _id;
        internal Type linkType;
        public Text text 
        {
            get 
            {
                return _text;
            }
        }
        public Image image 
        {
            get 
            {
                return _image;
            }
        }
        public string path 
        {
            get 
            {
                return _path;
            }
        }
        public string id 
        {
            get 
            {
                return _id;
            }
        }

        /// <summary>Constructor for the link class.</summary>
        public Link(Text text, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new LinkError("Path cannot be null or empty!");
            }
            this._text = text;
            this._id = id;
            this._path = path;
            this._image = new Image();
            linkType = typeof(Text);
        }

        /// <summary>Constructor for the link class.</summary>
        public Link(Image image, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new LinkError("Path cannot be null or empty!");
            }
            this._text = new Text();
            this._id = id;
            this._path = path;
            this._image = image;
            linkType = typeof(Image);
        }
    } 
}