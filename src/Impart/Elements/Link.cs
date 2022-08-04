using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Link element.</summary>
    public class Link : IElement, INested
    {
        /// <summary>The ID value of the IElement.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private Text _Text = new Text();

        /// <summary>The Text value of the Link.</summary>
        public Text Text
        {
            get 
            {
                return _Text;
            }
        }
        private Image _Image = new Image();

        /// <summary>The Image value of the Link.</summary>
        public Image Image
        {
            get
            {
                return _Image;
            }
        }
        private string _Path;

        /// <summary>The path value of the Link.</summary>
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

        /// <summary>The Attr values of the instance.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private Type _LinkType;

        /// <summary>The Type of Link.</summary>
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

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
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

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
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

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);
        
        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
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
        
        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            if (_LinkType == typeof(Image))
            {
                return ToString().Remove(Render.Length - 10);
            }
            else
            {
                return ToString().Remove(Render.Length - $"{((INested)_Text).Last()}</a>".Length);
            }
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            if (_LinkType == typeof(Image))
            {
                return "</img></a>";
            }
            else
            {
                return $"{((INested)_Text).Last()}</a>";
            }
        }
    }
}