using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>A header row of a Table.</summary>
    public class TableHeader : TableRow
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public new string ID
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
        private List<IElement> _Elements = new List<IElement>();

        /// <summary>The IElement values of the instance.</summary>
        public new IElement[] Elements
        {
            get
            {
                return _Elements.ToArray();
            }
        }

        /// <summary>The Attr values of the instance.</summary>
        public List<Attr> Attrs { get; set; } = new List<Attr>();

        /// <summary>The ExtAttr values of the instance.</summary>
        public List<ExtAttr> ExtAttrs { get; set; } = new List<ExtAttr>();

        internal new int _IOID;
        internal new EventManager _Events = new EventManager();

        /// <summary>Creates a TableHeader instance.</summary>
        /// <param name="elements">The IElements to add.</param>
        public TableHeader(params IElement[] elements)
        {
            _IOID = Ioid.Generate();
            _Elements.AddRange(elements);
        }

        internal TableHeader(int ioid)
        {
            _IOID = ioid;
        }

        /// <summary>Add IElements to the TableHeader.</summary>
        /// <param name="elements">The IElements to add.</param>
        public new TableHeader AddRow(params IElement[] elements)
        {
            _Elements.AddRange(elements);
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<tr class=\"{_IOID}\"{_Events}");
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
            foreach (IElement element in _Elements)
            {
                result.Append($"<th>{element}</th>");
            }
            return result.Append("</tr>").ToString();
        }
    }
}