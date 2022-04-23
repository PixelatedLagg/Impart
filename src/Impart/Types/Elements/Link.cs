using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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
        private string _ID;

        /// <value>The ID value of the Link.</value>
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
        private string _Path;

        /// <value>The path value of the Link.</value>
        public string Path 
        {
            get 
            {
                return _Path;
            }
        }
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The Attribute values of the Link.</value>
        public List<Attribute> Attributes
        {
            get 
            {
                return _Attributes;
            }
        }
        internal Type _LinkType;
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

        /// <summary>Creates an empty Link instance.</summary>
        public Link() : this(new Text(), Directory.GetCurrentDirectory()) {}

        /// <summary>Creates a Link instance.</summary>
        /// <param name="text">The Link text.</param>
        /// <param name="path">The Link path.</param>
        /// <param name="id">The Link ID.</param>
        public Link(Text text, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ImpartError("Path cannot be null or empty!");
            }
            _Text = text;
            _ID = id;
            _Path = path;
            _LinkType = typeof(Text);
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
        }

        /// <summary>Creates a Link instance.</summary>
        /// <param name="image">The Link image.</param>
        /// <param name="path">The Link path.</param>
        /// <param name="id">The Link ID.</param>
        public Link(Image image, string path, string id = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ImpartError("Path cannot be null or empty!");
            }
            _ID = id;
            _Path = path;
            _Image = image;
            _LinkType = typeof(Image);
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Link SetAttribute(AttributeType type, params object[] value)
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
            StringBuilder result = new StringBuilder($"<a href=\"{_Path}\"");
            if (_Attributes.Count != 0)
            {
                result.Append("style=\"");
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
        
        string Nested.First()
        {
            string result = ToString();
            if (_LinkType == typeof(Image))
            {
                return result.Remove(result.Length - ("</img></a>".Length) - 1);
            }
            else
            {
                return result.Remove(result.Length - ($"{((Nested)_Text).Last()}</a>".Length) - 1);
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