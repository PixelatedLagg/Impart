using System.Collections.Generic;
using System.Text;
using System;

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

        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        private bool Changed = true;
        private string Render = "";
        private int IOIDValue = Ioid.Generate();
        int Element.IOID
        {
            get
            {
                return IOIDValue;
            }
        }

        /// <summary>Creates an empty Image instance.</summary>
        public Image() : this("") { }
        
        /// <summary>Creates an Image instance.</summary>
        /// <param name="path">The Image path.</param>
        /// <param name="id">The Image ID.</param>
        public Image(string path, string id = null)
        {
            if (String.IsNullOrEmpty(path)) 
            {
                throw new ImpartError("Path cannot be empty or null!");
            }
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
            _Path = path;
            _ID = id;
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Image SetAttribute(AttributeType type, params object[] value)
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
                result.Append($"style=\"");
                foreach (Attribute attribute in _Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
                return Render;
            }
            foreach (ExtAttribute extAttribute in _ExtAttributes)
            {
                result.Append(extAttribute);
            }
            Render = result.Append('>').ToString();
            return Render;
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