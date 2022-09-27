using System;
using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Header element.</summary>
    public class Header : IElement, INested
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                foreach (ExtAttr ext in ExtAttrs)
                {
                    if (ext.Type == ExtAttrType.ID)
                    {
                        return ext.Value;
                    }
                }
                return null;
            }
        }

        /// <summary>The text value of the Header.</summary>
        public string TextValue;

        /// <summary>The Attr values of the instance.</summary>
        public List<Attr> Attrs { get; set; }= new List<Attr>();

        /// <summary>The ExtAttr values of the instance.</summary>
        public List<ExtAttr> ExtAttrs { get; set; }= new List<ExtAttr>();

        private int _Number = 1;

        /// <summary>The Header number value of the Header.</summary>
        public int Number 
        {
            get 
            {
                return _Number;
            }
            set
            {
                if (value > 6 || value < 1)
                {
                    throw new ImpartError("Header number must be between 1 and 5!");
                }
                _Number = value;
            }
        }

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        internal int _IOID;
        internal EventManager _Events = new EventManager();

        /// <summary>Creates an empty Header instance.</summary>
        public Header() : this("") { }

        /// <summary>Creates a Header instance.</summary>
        /// <param name="text">The Header text.</param>
        /// <param name="number">The Header type.</param>
        public Header(string text, int number = 1)
        {
            if (number > 6 || number < 1)
            {
                throw new ImpartError("Header number must be between 1 and 5.");
            }
            TextValue = text;
            _Number = number;
            _IOID = Ioid.Generate();
        }

        internal Header(string text, int number, int ioid)
        {
            if (number > 6 || number < 1)
            {
                throw new ImpartError("Header number must be between 1 and 5.");
            }
            TextValue = text;
            _Number = number;
            _IOID = ioid;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<h{_Number}");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append($"\"class=\"{_IOID}\"{_Events}");
            }
            foreach (ExtAttr ExtAttr in ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            return result.Append($">{TextValue}</h{Number}>").ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Header result = new Header();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Number = _Number;
            result.TextValue = TextValue;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Header result = new Header();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Number = _Number;
            result.TextValue = TextValue;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            string render = ToString();
            return render.Remove(render.Length - 5);
        }
        
        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return $"</h{_Number}>";
        }
    } 
}