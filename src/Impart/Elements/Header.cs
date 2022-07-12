using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Header element.</summary>
    public struct Header : IElement, INested
    {
        private string _Text = "";

        /// <value>The text value of the Header.</value>
        public string TextValue
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

        /// <value>The Attr values of the Header.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the Header.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <value>The ExtAttr values of the instance.</value>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private int _Number = 1;

        /// <value>The Header number value of the Header.</value>
        public int Number 
        {
            get 
            {
                return _Number;
            }
            set
            {
                if (value > 5 || value < 1)
                {
                    throw new ImpartError("Header number must be between 1 and 5!");
                }
                Changed = true;
                _Number = value;
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

        /// <summary>Creates an empty Header instance.</summary>
        public Header() : this("") { }

        /// <summary>Creates a Header instance.</summary>
        /// <param name="text">The Header text.</param>
        /// <param name="number">The Header type.</param>
        public Header(string text, int number = 1)
        {
            if (number > 5 || number < 1)
            {
                throw new ImpartError("Header number must be between 1 and 5.");
            }
            if (String.IsNullOrEmpty(text))
            {
                throw new ImpartError("Text cannot be null or empty.");
            }
            _Text = text;
            _Number = number;
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
            StringBuilder result = new StringBuilder($"<h{_Number}");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
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
            Render = result.Append($">{_Text}</h{_Number}>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Header result = new Header();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Number = _Number;
            result._Text = _Text;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Header result = new Header();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Number = _Number;
            result._Text = _Text;
            result.Render = Render;
            return result;
        }

        string INested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - 5);
        }
        
        string INested.Last()
        {
            return $"</h{_Number}>";
        }
    } 
}