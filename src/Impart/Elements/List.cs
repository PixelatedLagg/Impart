using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Store a list of IElements.</summary>
    public class EList<T> : IElement, INested where T : IElement
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private List<T> _Entries = new List<T>();

        /// <summary>The entry values of the EList.</summary>
        public List<T> Entries 
        {
            get 
            {
                return _Entries;
            }
        }
        private EListType _Type;

        /// <summary>The EListType value of the EList.</summary>
        public EListType Type
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

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        internal int _IOID = Ioid.Generate();
        internal EventManager _Events = new EventManager();
        internal bool Changed = true;
        private string _EListType;
        private string Render = "";

        /// <summary>Creates an EList instance.</summary>
        /// <param name="type">The EList type.</param>
        /// <param name="entries">The EList entries.</param>
        public EList(EListType type, params T[] entries)
        {
            _Type = type;
            if (type == EListType.Ordered)
            {
                _EListType = "ol";
            }
            else
            {
                _EListType = "ul";
            }
            foreach (T entry in entries)
            {
                _Entries.Add(entry);
            }
        }

        /// <summary>Add entries to the EList.</summary>
        /// <param name="entries">The entry to add.</param>
        public EList<T> Add(params T[] entries)
        {
            foreach (T entry in entries)
            {
                _Entries.Add(entry);
            }
            Changed = true;
            return this;
        }

        /// <summary>Remove entries from the EList.</summary>
        /// <param name="entries">The entries to remove.</param>
        public EList<T> Remove(params T[] entries)
        {
            foreach (T entry in entries)
            {
                if (!_Entries.Contains(entry))
                {
                    throw new ImpartError("EList does not contain this entry!");
                }
                _Entries.Remove(entry);
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
            StringBuilder result = new StringBuilder($"<{_EListType}");
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
            result.Append('>');
            foreach (T entry in _Entries)
            {
                result.Append($"<li>{entry}</li>");
            }
            Render = result.Append($"</{_EListType}>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            EList<T> result = new EList<T>(_Type);
            result.Attrs = Attrs;
            result._Entries = _Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result.Render = Render;
            result._EListType = _EListType;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            EList<T> result = new EList<T>(_Type);
            result.Attrs = Attrs;
            result._Entries = _Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result.Render = Render;
            result._EListType = _EListType;
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
            return $"</{_EListType}>";
        }
    }
}