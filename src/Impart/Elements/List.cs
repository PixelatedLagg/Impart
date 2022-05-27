using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>List element.</summary>
    public struct List : Element, Nested
    {
        private string _ID;

        /// <value>The ID value of the List.</value>
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
        private List<Text> _Entries = new List<Text>();

        /// <value>The entry values of the List.</value>
        public List<Text> Entries 
        {
            get 
            {
                return _Entries;
            }
        }
        private ListType _Type;

        /// <value>The type value of the List.</value>
        public ListType Type
        {
            get
            {
                return _Type;
            }
        }

        /// <value>The Attribute values of the List.</value>
        public AttrList Attrs = new AttrList();
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }
        private string _ListType;
        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a List instance.</summary>
        /// <param name="type">The List type.</param>
        /// <param name="id">The List ID.</param>
        /// <param name="textEntries">The List entries.</param>
        public List(ListType type, string id = null, params Text[] textEntries)
        {
            _Type = type;
            _ID = id;
            if (type == ListType.Ordered)
            {
                _ListType = "ol";
            }
            else
            {
                _ListType = "ul";
            }
            if (id != null)
            {
                _ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, id));
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
            if (!Changed && !Attrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
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
            foreach (ExtAttr ExtAttr in _ExtAttrs)
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
        
        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            List result = new List();
            result.Attrs = Attrs;
            result._Entries = _Entries;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Type = _Type;
            result.Changed = Changed;
            result.Render = Render;
            result._ListType = _ListType;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            List result = new List();
            result.Attrs = Attrs;
            result._Entries = _Entries;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Type = _Type;
            result.Changed = Changed;
            result.Render = Render;
            result._ListType = _ListType;
            return result;
        }

        string Nested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - ($"</{_ListType}>".Length));
        }

        string Nested.Last()
        {
            return $"</{_ListType}>";
        }
    }
}