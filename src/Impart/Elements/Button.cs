using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Button element.</summary>
    public struct Button : IElement, INested
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

        /// <value>The Attr values of the instance.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the instance.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <value>The ExtAttr values of the instance.</value>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int IElement.IOID
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
        
        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Button result = new Button();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Text = _Text;
            result.Render = Render;
            return result;
        }
        
        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Button result = new Button();
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
            Render = result.Append($">{_Text}").ToString();
            return Render;
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</button>";
        }
    }
}