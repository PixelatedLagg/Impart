using System.Text;
using Impart.Internal;
using Impart.Scripting;

namespace Impart
{
    /// <summary>Image element.</summary>
    public class Image : IElement, INested
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private string _Source;

        /// <summary>The source link of the Image.</summary>
        public string Source
        {
            get 
            {
                return _Source;
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

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        internal int _IOID = Ioid.Generate();
        internal EventManager _Events = new EventManager();
        internal bool Changed = true;
        private string Render = "";

        /// <summary>Creates an empty Image instance.</summary>
        public Image() : this("/") { }
        
        /// <summary>Creates an Image instance.</summary>
        /// <param name="path">The Image path.</param>
        public Image(string path)
        {
            if (path == null) 
            {
                throw new ImpartError("Path cannot be null!");
            }
            _Source = path;
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
            StringBuilder result = new StringBuilder($"<img src=\"{_Source}\"");
            if (Attrs.Count != 0)
            {
                result.Append($" style=\"");
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
            Render = result.Append('>').ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Image result = new Image();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Source = _Source;
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
            Image result = new Image();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Source = _Source;
            result.Render = Render;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            return ToString().Remove(Render.Length - 6);
        }
        
        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</img>";
        }
    } 
}