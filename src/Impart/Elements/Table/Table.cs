using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Table element.</summary>
    public class Table : Element
    {
        private string _ID;

        /// <value>The ID value of the Table.</value>
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
        private List<TableRow> _Rows = new List<TableRow>();

        /// <value>The TableRow values of the Table.</value>
        public TableRow[] Rows
        {
            get
            {
                return _Rows.ToArray();
            }
        }

        /// <value>The Attribute values of the Table.</value>
        public AttrList Attrs = new AttrList();
        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
        private bool Changed = true;
        private string Render;
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }

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
            if (!Changed && !Attrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
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
            foreach (ExtAttr ExtAttr in _ExtAttrs)
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
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Rows = _Rows;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Table result = new Table();
            result.Attrs = Attrs;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
            result._IOID = _IOID;
            result._Rows = _Rows;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }
    }
}