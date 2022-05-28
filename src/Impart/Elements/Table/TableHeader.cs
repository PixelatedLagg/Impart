using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>A header row of a Table.</summary>
    public class TableHeader : TableRow
    {
        private List<Element> _Elements = new List<Element>();

        /// <value>The Element values of the TableHeader.</value>
        public new Element[] Elements
        {
            get
            {
                return _Elements.ToArray();
            }
        }

        /// <value>The Attribute values of the TableHeader.</value>
        new public AttrList Attrs = new AttrList();
        new public ExtAttrList ExtAttrs = new ExtAttrList();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a TableHeader instance.</summary>
        /// <param name="elements">The Elements to add.</param>
        public TableHeader(params Element[] elements)
        {
            _Elements.AddRange(elements);
        }

        /// <summary>Add Elements to the TableHeader.</summary>
        /// <param name="elements">The Elements to add.</param>
        public new TableHeader AddRow(params Element[] elements)
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
            foreach (Element element in _Elements)
            {
                result.Append($"<th>{element}</th>");
            }
            Render = result.Append("</tr>").ToString();
            return Render;
        }
    }
}