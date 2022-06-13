using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Link element.</summary>
    public struct Link : Element, Nested
    {
        private Text _Text = new Text();

        /// <value>The Text value of the Link.</value>
        public Text Text 
        {
            get 
            {
                return _Text;
            }
        }
        private Image _Image = new Image();

        /// <value>The Image value of the Link.</value>
        public Image Image 
        {
            get
            {
                return _Image;
            }
        }
        private string _Path;

        /// <value>The path value of the Link.</value>
        public string Path 
        {
            get 
            {
                return _Path;
            }
            set
            {
                Changed = true;
                _Path = value;
            }
        }

        /// <value>The Attr values of the Link.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the Link.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <value>The ExtAttr values of the instance.</value>
        ExtAttrList Element.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private Type _LinkType;

        /// <value>The Type of Link.</value>
        public Type LinkType
        {
            get
            {
                return _LinkType;
            }
            set
            {
                if (value != typeof(Text) && value != typeof(Image))
                {
                    throw new ImpartError("Link Type must be Text or Image!");
                }
                Changed = true;
                _LinkType = value;
            }
        }
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates an empty Link instance.</summary>
        public Link() : this(new Text(), "/") {}

        /// <summary>Creates a Link instance.</summary>
        /// <param name="text">The Link text.</param>
        /// <param name="path">The Link path.</param>
        public Link(Text text, string path)
        {
            if (path == null)
            {
                throw new ImpartError("Path cannot be null!");
            }
            _Text = text;
            _Path = path;
            _LinkType = typeof(Text);
        }

        /// <summary>Creates a Link instance.</summary>
        /// <param name="image">The Link image.</param>
        /// <param name="path">The Link path.</param>
        public Link(Image image, string path)
        {
            if (path == null)
            {
                throw new ImpartError("Path cannot be null!");
            }
            _Path = path;
            _Image = image;
            _LinkType = typeof(Image);
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed && !Attrs.Changed && !ExtAttrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            ExtAttrs.Changed = false;
            StringBuilder result = new StringBuilder($"<a href=\"{_Path}\"");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            if (_LinkType == typeof(Image))
            {
                result.Append($">{_Image}</a>");
            }
            else
            {
                result.Append($">{_Text}</a>");
            }
            Render = result.ToString();
            return Render;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Link result = new Link();
            result._Image = _Image;
            result._LinkType = _LinkType;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Text = _Text;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Link result = new Link();
            result._Image = _Image;
            result._LinkType = _LinkType;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Text = _Text;
            result.Render = Render;
            return result;
        }
        
        string Nested.First()
        {
            string result = ToString();
            if (_LinkType == typeof(Image))
            {
                return result.Remove(result.Length - 10);
            }
            else
            {
                return result.Remove(result.Length - ($"{((Nested)_Text).Last()}</a>".Length));
            }
        }

        string Nested.Last()
        {
            if (_LinkType == typeof(Image))
            {
                return "</img></a>";
            }
            else
            {
                return $"{((Nested)_Text).Last()}</a>";
            }
        }
    }
}