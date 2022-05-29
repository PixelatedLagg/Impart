using System.Text;
using Impart.Internal;

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

        /// <value>The attribute values of the Button.</value>
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

        /// <summary>Creates an empty Button instance.</summary>
        public Button() : this(new Text("")) { }

        /// <summary>Creates a Button instance.</summary>
        /// <param name="text">The Button text.</param>
        public Button(Text text)
        {
            _Text = text;
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
            StringBuilder result = new StringBuilder("<button");
            if (Attrs.Count != 0)
            {
                result.Append(" style\"");
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
            Render = result.Append($">{_Text}</button>").ToString();
            return Render;
        }
        
        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Button result = new Button();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
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
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
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