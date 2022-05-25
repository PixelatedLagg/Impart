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

        /// <value>The attribute values of the Button.</value>
        public List<Attribute> Attributes = new List<Attribute>();
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }

        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
        private bool Changed = true;
        private string Render = "";

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
                _ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, id));
            }
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
            if (Attributes.Count != 0)
            {
                result.Append(" style\"");
                foreach (Attribute attribute in Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in _ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            Render = result.Append($">{_Text}</button>").ToString();
            return Render;
        }
        
        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Button result = new Button();
            result.Attributes = Attributes;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Text = _Text;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Button result = new Button();
            result.Attributes = Attributes;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Text = _Text;
            result.Changed = Changed;
            result.Render = Render;
            return result;
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