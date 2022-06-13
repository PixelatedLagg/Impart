using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Table element.</summary>
    public class Table : Element
    {
        private List<TableRow> _Rows = new List<TableRow>();

        /// <value>The TableRow values of the Table.</value>
        public TableRow[] Rows
        {
            get
            {
                return _Rows.ToArray();
            }
        }

        /// <value>The Attr values of the Table.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the Table.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <value>The ExtAttr values of the instance.</value>
        ExtAttrList Element.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a Table instance.</summary>
        /// <param name="rows">The TableRows to add.</param>
        public Table(params TableRow[] rows)
        {
            _Rows.AddRange(rows);
        }

        /// <summary>Add TableRows to the Table.</summary>
        /// <param name="rows">The TableRows to add.</param>
        public Table AddRow(params TableRow[] rows)
        {
            _Rows.AddRange(rows);
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
            StringBuilder result = new StringBuilder("<table");
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
            foreach (TableRow row in _Rows)
            {
                result.Append(row);
            }
            Render = result.Append("</table>").ToString();
            return Render;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Table result = new Table();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Rows = _Rows;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Table result = new Table();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Rows = _Rows;
            result.Render = Render;
            return result;
        }
    }
}