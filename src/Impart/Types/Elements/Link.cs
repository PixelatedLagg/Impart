using System;

namespace Impart
{
    /// <summary>Link element.</summary>
    public struct Link : Element
    {
        private Text _text;

        /// <value>The Text value of the Link.</value>
        public Text text 
        {
            get 
            {
                return _text;
            }
        }
        private Image _image;

        /// <value>The Image value of the Link.</value>
        public Image image 
        {
            get 
            {
                return _image;
            }
        }
        private string _id;

        /// <value>The ID value of the Link.</value>
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        private string _path;

        /// <value>The path value of the Link.</value>
        public string path 
        {
            get 
            {
                return _path;
            }
        }
        internal Type linkType;

        /// <summary>Creates a Link instance with <paramref name="text"/> as the Link text, and <paramref name="path"/> as the Link path.</summary>
        /// <returns>A Link instance.</returns>
        /// <param name="text">The Link text.</param>
        /// <param name="path">The Link path.</param>
        /// <param name="id">The Link ID.</param>
        public Link(Text text, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ImpartError("Path cannot be null or empty!");
            }
            this._text = text;
            this._id = id;
            this._path = path;
            this._image = new Image();
            linkType = typeof(Text);
        }

        /// <summary>Creates a Link instance with <paramref name="image"/> as the Link image, and <paramref name="path"/> as the Link path.</summary>
        /// <returns>A Link instance.</returns>
        /// <param name="image">The Link image.</param>
        /// <param name="path">The Link path.</param>
        /// <param name="id">The Link ID.</param>
        public Link(Image image, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ImpartError("Path cannot be null or empty!");
            }
            this._text = new Text();
            this._id = id;
            this._path = path;
            this._image = image;
            linkType = typeof(Image);
        }
    } 
}