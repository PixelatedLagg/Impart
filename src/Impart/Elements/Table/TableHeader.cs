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
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
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
        new public AttrList Attrs = new AttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        new public ExtAttrList ExtAttrs = new ExtAttrList();

        internal new int _IOID = Ioid.Generate();
        internal new EventManager _Events = new EventManager();
        internal new bool Changed = true;
        private string Render = "";

        /// <summary>Creates a TableHeader instance.</summary>
        /// <param name="elements">The IElements to add.</param>
        public TableHeader(params IElement[] elements)
        {
            _Elements.AddRange(elements);
        }

        /// <summary>Add IElements to the TableHeader.</summary>
        /// <param name="elements">The IElements to add.</param>
        public new TableHeader AddRow(params IElement[] elements)
        {
            _Elements.AddRange(elements);
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
            StringBuilder result = new StringBuilder("<tr");
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
            foreach (IElement element in _Elements)
            {
                result.Append($"<th>{element}</th>");
            }
            Render = result.Append("</tr>").ToString();
            return Render;
        }
    }
}