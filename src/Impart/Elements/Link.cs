using System;
using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Link element.</summary>
    public class Link : IElement, INested
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                foreach (ExtAttr ext in ExtAttrs)
                {
                    if (ext.Type == ExtAttrType.ID)
                    {
                        return ext.Value;
                    }
                }
                return null;
            }
        }

        /// <summary>The Text value of the Link.</summary>
        public Text Text;

        /// <summary>The Image value of the Link.</summary>
        public Image Image;

        /// <summary>The path value of the Link.</summary>
        public string Path;

        /// <summary>The Attr values of the instance.</summary>
        public List<Attr> Attrs { get; set; } = new List<Attr>();

        /// <summary>The ExtAttr values of the instance.</summary>
        public List<ExtAttr> ExtAttrs { get; set; } = new List<ExtAttr>();

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
                _LinkType = value;
            }
        }

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        internal int _IOID;
        internal EventManager _Events = new EventManager();

        /// <summary>Creates an empty Link instance.</summary>
        public Link() : this(new Text(), "/") {}

        /// <summary>Creates a Link instance.</summary>
        /// <param name="text">The Link text.</param>
        /// <param name="path">The Link path.</param>
        public Link(Text text, string path)
        {
            Text = text;
            Path = path;
            _LinkType = typeof(Text);
            _IOID = Ioid.Generate();
        }

        /// <summary>Creates a Link instance.</summary>
        /// <param name="image">The Link image.</param>
        /// <param name="path">The Link path.</param>
        public Link(Image image, string path)
        {
            Path = path;
            Image = image;
            _LinkType = typeof(Image);
            _IOID = Ioid.Generate();
        }

        internal Link(string path, int ioid)
        {
            Path = path;
            _IOID = ioid;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<a href=\"{Path}\"");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append($"\"class=\"{_IOID}\"{_Events}");
            }
            foreach (ExtAttr ExtAttr in ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            if (_LinkType == typeof(Image))
            {
                result.Append($">{Image}</a>");
            }
            else
            {
                result.Append($">{Text}</a>");
            }
            return result.ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Link result = new Link();
            result.Image = Image;
            result._LinkType = _LinkType;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result.Text = Text;
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
            result.Image = Image;
            result._LinkType = _LinkType;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result.Text = Text;
            return result;
        }
        
        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            string render = ToString();
            if (_LinkType == typeof(Image))
            {
                return render.Remove(render.Length - 10);
            }
            else
            {
                return render.Remove(render.Length - $"{((INested)Text).Last()}</a>".Length);
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
                return $"{((INested)Text).Last()}</a>";
            }
        }
    }
}