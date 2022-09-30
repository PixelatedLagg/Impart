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

        /// <summary>Whether or not the entries in the EList are ordered.</summary>
        public bool IsOrdered;

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

        /// <summary>Creates an EList instance.</summary>
        /// <param name="entries">The EList entries.</param>
        public EList(params IElement[] entries)
        {
            IsOrdered = false;
            _IOID = Ioid.Generate();
            foreach (IElement entry in entries)
            {
                Entries.Add(entry);
            }
        }

        /// <summary>Creates an EList instance.</summary>
        /// <param name="isOrdered">Whether or not the entries in the EList are ordered.</param>
        /// <param name="entries">The EList entries.</param>
        public EList(bool isOrdered, params IElement[] entries)
        {
            IsOrdered = isOrdered;
            _IOID = Ioid.Generate();
            foreach (IElement entry in entries)
            {
                Entries.Add(entry);
            }
        }

        internal EList(bool isOrdered, int ioid)
        {
            IsOrdered = isOrdered;
            _IOID = ioid;
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
            StringBuilder result = new StringBuilder($"<{(IsOrdered ? "ol" : "ul")} class=\"{_IOID}\"{_Events}");
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
            return result.Append($"</{(IsOrdered ? "ol" : "ul")}>").ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            EList result = new EList(IsOrdered);
            result.Attrs = Attrs;
            result.Entries = Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            EList result = new EList(IsOrdered);
            result.Attrs = Attrs;
            result.Entries = Entries;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
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
            return $"</{(IsOrdered ? "ol" : "ul")}>";
        }
    }
}