using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Image element.</summary>
    public struct Image : Element, Nested
    {
        private string _Path;

        /// <value>The path value of the Image.</value>
        public string Path 
        {
            get 
            {
                return _Path;
            }
        }

        /// <value>The Attribute values of the Image.</value>
        public AttrList Attrs = new AttrList();
        public ExtAttrList ExtAttrs = new ExtAttrList();
        ExtAttrList Element.ExtAttrs
        {
            get
            {
                return ExtAttrs;
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
            _Path = path;
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
            StringBuilder result = new StringBuilder($"<img src=\"{_Path}\"");
            if (Attrs.Count != 0)
            {
                result.Append($" style=\"");
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
            Render = result.Append('>').ToString();
            return Render;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Image result = new Image();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Path = _Path;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Image result = new Image();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Path = _Path;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        string Nested.First()
        {
            return ToString();
        }
        
        string Nested.Last()
        {
            return "</img>";
        }
    } 
}