using System;
using System.Text;
using Impart.Internal;
using System.Collections.Generic;

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
        private string _ID;

        /// <value>The ID value of the Image.</value>
        public string ID
        {
            get 
            {
                return _ID;
            }
            set
            {
                Changed = true;
                _ID = value;
            }
        }
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The Attribute values of the Image.</value>
        public List<Attribute> Attributes
        {
            get 
            {
                return _Attributes;
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

        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates an empty Image instance.</summary>
        public Image() : this("/") { }
        
        /// <summary>Creates an Image instance.</summary>
        /// <param name="path">The Image path.</param>
        /// <param name="id">The Image ID.</param>
        public Image(string path, string id = null)
        {
            if (path == null) 
            {
                throw new ImpartError("Path cannot be null!");
            }
            _Path = path;
            _ID = id;
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttrType.ID, id));
            }
        }

        /// <summary>Sets an Attribute of the instance.</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Image SetAttribute(AttrType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            Changed = true;
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder($"<img src=\"{_Path}\"");
            if (_Attributes.Count != 0)
            {
                result.Append($" style=\"");
                foreach (Attribute attribute in _Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttribute extAttribute in _ExtAttributes)
            {
                result.Append(extAttribute);
            }
            Render = result.Append('>').ToString();
            return Render;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Image result = new Image();
            result._Attributes = _Attributes;
            result._ExtAttributes = _ExtAttributes;
            result._ID = _ID;
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
            result._Attributes = _Attributes;
            result._ExtAttributes = _ExtAttributes;
            result._ID = _ID;
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