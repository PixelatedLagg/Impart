using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Button element.</summary>
    public class Button : IElement, INested
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private Text _Text;

        /// <summary>The Text value of the Button.</summary>
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
        private int _IOID = Ioid.Generate();

        /// <summary>The internal ID of the instance.</summary>
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

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);
        
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
            return ToString().Remove(Render.Length - 9);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</button>";
        }
    }
}