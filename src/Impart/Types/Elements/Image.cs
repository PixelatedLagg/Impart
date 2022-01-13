using System;
using System.IO;

namespace Impart
{
    /// <summary>Class that represents an image.</summary>
    public struct Image : Element
    {
        private string _path;
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

        /// <summary>Constructor for the image class.</summary>
        public Image(string path, string id = null)
        {
            if (String.IsNullOrEmpty(path)) 
            {
                throw new ImpartError("Path cannot be empty or null!");
            }
            if (!System.IO.File.Exists(path))
            {
                throw new ImpartError("Image file not found!");
            }
            if (!File.IsImage(Path.GetExtension(path)))
            {
                throw new ImpartError("Unsupported file extension!");
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
            setProperties= new bool[] {false, false};
        }

        /// <summary>Method for setting the image size.</summary>
        public Image SetSize(int x, int y)
        {
            if (setProperties[0])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            setProperties[0] = true;
            if (x < 0 || y < 0)
            {
                throw new ImpartError("Width and height values must be positive!");
            }
            _attributes += $" width=\"{x}\" height=\"{y}\"";
            return this;
        }

        /// <summary>Method for setting the image border.</summary>
        public Image SetBorder(int pixels, string border, Color color)
        {
            if (setProperties[1])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            setProperties[1] = true;
            if (pixels < 0)
            {
                throw new ImpartError("Border thickness must be above 0!");
            }
            if (!Border.Any(border))
            {
                throw new ImpartError("Invalid border value!");
            }
            switch (color)
            {
                case Rgb rgb:
                    _style += $" border: {pixels}px {border} rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case Hsl hsl:
                    _style += $" border: {pixels}px {border} hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case Hex hex:
                    _style += $" border: {pixels}px {border} #{hex.hex};";
                    break;
            }
            return this;
        }
    } 
}