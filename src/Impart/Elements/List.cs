using System;
using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Store a list of IElements.</summary>
    public class EList : IElement, INested
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

        /// <summary>The entry values of the EList.</summary>
        public List<IElement> Entries = new List<IElement>();
        private EListType _Type;

        /// <summary>The EListType value of the EList.</summary>
        public EListType Type
        {
            get
            {
                return _Type;
            }
        }

        /// <summary>The ExtAttr values of the instance.</summary>
        public List<ExtAttr> ExtAttrs { get; set; } = new List<ExtAttr>();

        /// <summary>The Attr values of the instance.</summary>
        public List<Attr> Attrs { get; set; } = new List<Attr>();

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
        private string _EListType;

        /// <summary>Creates an EList instance.</summary>
        /// <param name="type">The EList type.</param>
        /// <param name="entries">The EList entries.</param>
        public EList(EListType type, params IElement[] entries)
        {
            _Type = type;
            _IOID = Ioid.Generate();
            if (type == EListType.Ordered)
            {
                _EListType = "ol";
            }
            else
            {
                _EListType = "ul";
            }
            foreach (IElement entry in entries)
            {
                Entries.Add(entry);
            }
        }

        internal EList(EListType type, int ioid, params IElement[] entries)
        {
            _Type = type;
            _IOID = ioid;
            if (type == EListType.Ordered)
            {
                _EListType = "ol";
            }
            else
            {
                _EListType = "ul";
            }
            foreach (IElement entry in entries)
            {
                Entries.Add(entry);
            }
        }

        /// <summary>Add entries to the EList.</summary>
        /// <param name="entries">The entry to add.</param>
        public EList Add(params IElement[] entries)
        {
            foreach (IElement entry in entries)
            {
                Entries.Add(entry);
            }
            return this;
        }

        /// <summary>Remove entries from the EList.</summary>
        /// <param name="entries">The entries to remove.</param>
        public EList Remove(params IElement[] entries)
        {
            foreach (IElement entry in entries)
            {
                if (!Entries.Contains(entry))
                {
                    throw new ImpartError("EList does not contain this entry!");
                }
                Entries.Remove(entry);
            }
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<{_EListType} class=\"{_IOID}\"{_Events}");
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
            foreach (IElement entry in Entries)
            {
                result.Append($"<li>{entry}</li>");
            }
            return result.Append($"</{_EListType}>").ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            EList result = new EList(_Type);
            result.Attrs = Attrs;
            result.Entries = Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
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
            EList result = new EList(_Type);
            result.Attrs = Attrs;
            result.Entries = Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._EListType = _EListType;
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
            return $"</{_EListType}>";
        }
    }
}