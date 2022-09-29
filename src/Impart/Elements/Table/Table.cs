using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Table element.</summary>
    public class Table : IElement, INested
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
        private List<TableRow> _Rows = new List<TableRow>();

        /// <summary>The TableRow values of the instance.</summary>
        public TableRow[] Rows
        {
            get
            {
                return _Rows.ToArray();
            }
        }

        /// <summary>The Attr values of the instance.</summary>
        public List<Attr> Attrs { get; set; } = new List<Attr>();

        /// <summary>The ExtAttr values of the instance.</summary>
        public List<ExtAttr> ExtAttrs { get; set; } = new List<ExtAttr>();

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

        /// <summary>Creates a Table instance.</summary>
        /// <param name="rows">The TableRows to add.</param>
        public Table(params TableRow[] rows)
        {
            _IOID = Ioid.Generate();
            _Rows.AddRange(rows);
        }

        internal Table(int ioid)
        {
            _IOID = ioid;
        }

        /// <summary>Add TableRows to the Table.</summary>
        /// <param name="rows">The TableRows to add.</param>
        public Table AddRow(params TableRow[] rows)
        {
            _Rows.AddRange(rows);
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<table class=\"{_IOID}\"{_Events}");
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
            return result.Append("</table>").ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Table result = new Table();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Rows = _Rows;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Table result = new Table();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Rows = _Rows;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            string render = ToString();
            return render.Remove(render.Length - 8);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</table>";
        }
    }
}