using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Button element.</summary>
    public struct Button : Element, Nested
    {
        private Text _Text;

        /// <value>The Text value of the Button.</value>
        public Text Text 
        {
            get 
            {
                return _Text;
            }
            set
            {
                Changed = true;
                _Text = value;
            }
        }

        private string _ID;

        /// <value>The ID value of the Button.</value>
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

        /// <value>The attribute values of the Button.</value>
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

        /// <summary>Creates an empty Button instance.</summary>
        public Button() : this(new Text("")) { }

        /// <summary>Creates a Button instance.</summary>
        /// <param name="text">The Button text.</param>
        /// <param name="id">The Button ID.</param>
        public Button(Text text, string id = null)
        {
            _Text = text;
            _ID = id;
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
        }

        /// <summary>Sets an Attribute of the instance.</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Button SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
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
            StringBuilder result = new StringBuilder("<button");
            if (_Attributes.Count != 0)
            {
                result.Append(" style\"");
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
            Render = result.Append($">{_Text}</button>").ToString();
            return Render;
        }
        
        string Nested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - 9);
        }

        string Nested.Last()
        {
            return "</button>";
        }
    }
}