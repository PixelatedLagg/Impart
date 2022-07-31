using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>List element.</summary>
    public struct List : IElement, INested
    {
        private List<Text> _Entries = new List<Text>();

        /// <summary>The entry values of the List.</summary>
        public List<Text> Entries 
        {
            get 
            {
                return _Entries;
            }
        }
        private ListType _Type;

        /// <summary>The ListType value of the List.</summary>
        public ListType Type
        {
            get
            {
                return _Type;
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
        private string _ListType;
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a List instance.</summary>
        /// <param name="type">The List type.</param>
        /// <param name="textEntries">The List entries.</param>
        public List(ListType type, params Text[] textEntries)
        {
            _Type = type;
            if (type == ListType.Ordered)
            {
                _ListType = "ol";
            }
            else
            {
                _ListType = "ul";
            }
            foreach (Text text in textEntries)
            {
                _Entries.Add(text);
            }
        }

        /// <summary>Add a Text to the List.</summary>
        /// <param name="textEntries">The Text(s) to add.</param>
        public List Add(params Text[] textEntries)
        {
            foreach (Text text in textEntries)
            {
                _Entries.Add(text);
            }
            Changed = true;
            return this;
        }

        /// <summary>Remove a Text from the List.</summary>
        /// <param name="textEntries">The Text(s) to remove.</param>
        public List Remove(params Text[] textEntries)
        {
            foreach (Text text in textEntries)
            {
                if (!_Entries.Contains(text))
                {
                    throw new ImpartError("List does not contain this Text!");
                }
                _Entries.Remove(text);
            }
            Changed = true;
            return this;
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
            StringBuilder result = new StringBuilder($"<{_ListType}");
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
            result.Append('>');
            foreach (Text text in _Entries)
            {
                result.Append($"<li>{text}</li>");
            }
            Render = result.Append($"</{_ListType}>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            List result = new List();
            result.Attrs = Attrs;
            result._Entries = _Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Type = _Type;
            result.Render = Render;
            result._ListType = _ListType;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            List result = new List();
            result.Attrs = Attrs;
            result._Entries = _Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Type = _Type;
            result.Render = Render;
            result._ListType = _ListType;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            return ToString().Remove(Render.Length - 5);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return $"</{_ListType}>";
        }
    }
}