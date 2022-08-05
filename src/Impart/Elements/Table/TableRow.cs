using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>A row of a Table.</summary>
    public class TableRow
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private List<IElement> _Elements = new List<IElement>();

        /// <summary>The IElements values of the instance.</summary>
        public IElement[] Elements
        {
            get
            {
                return _Elements.ToArray();
            }
        }

        /// <summary>The Attr values of the instance.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        public ExtAttrList ExtAttrs = new ExtAttrList();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a TableRow instance.</summary>
        /// <param name="elements">The IElements to add.</param>
        public TableRow(params IElement[] elements)
        {
            _Elements.AddRange(elements);
        }

        /// <summary>Add IElements to the TableRow.</summary>
        /// <param name="elements">The IElements to add.</param>
        public TableRow AddRow(params IElement[] elements)
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
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            result.Append('>');
            foreach (IElement element in _Elements)
            {
                result.Append($"<td>{element}</td>");
            }
            Render = result.Append("</tr>").ToString();
            return Render;
        }
    }
}