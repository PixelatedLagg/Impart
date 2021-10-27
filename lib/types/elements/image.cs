using System.Drawing;
using System;
using System.IO;

namespace Csweb
{
    public class Image
    {
        private string _path;
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
        public Image(string path, string id = null)
        {
            if (String.IsNullOrEmpty(path)) 
            {
                throw new ImageError("Path cannot be empty or null!", this);
            }
            if (!File.Exists(path))
            {
                throw new ImageError("Image file not found!", this);
            }
            if (!Common.IsImage(Path.GetExtension(path)))
            {
                throw new ImageError("Unsupported file extension!", this);
            }
            if (String.IsNullOrEmpty(id))
            {
                this._id = null;
            }
            else
            {
                this._id = id;
            }
            this._path = path;
            _style = "";
            _attributes = "";
        }
        public Image SetSize(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ImageError("Width and height values must be positive!", this);
            }
            _attributes += $" width=\"{x}\" height=\"{y}\"";
            return this;
        }
        public Image SetBorder(int pixels, string border, Color color)
        {
            if (pixels < 0)
            {
                throw new ImageError("Border thickness must be above 0!", this);
            }
            if (!Border.Any(border))
            {
                throw new ImageError("Invalid border value!", this);
            }
            _style += $" border: {pixels}px {border} {color.ToKnownColor()};";
            return this;
        }
        public Image SetBorder(float percent, string border, Color color)
        {
            if (percent > 1 || percent < 0)
            {
                throw new ImageError("Percent number must be between 1-0!", this);
            }
            if (!Border.Any(border))
            {
                throw new ImageError("Invalid border value!", this);
            }
            _style += $" border: {percent * 100}% {border} {color.ToKnownColor()};";
            return this;
        }
    } 
}