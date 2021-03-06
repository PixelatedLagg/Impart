using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Table element.</summary>
    public class Table : IElement, INested
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

        /// <value>The Attr values of the instance.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the instance.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <value>The ExtAttr values of the instance.</value>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int IElement.IOID
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

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Table result = new Table();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Rows = _Rows;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Table result = new Table();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Rows = _Rows;
            result.Render = Render;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            return ToString().Remove(Render.Length - 8);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</table>";
        }
    }
}