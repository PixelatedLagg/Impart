using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Impart
{
    public class Table
    {
        private string _ID;
        
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

        public List<TableRow> Rows
        {
            get
            {
                return _Rows;
            }
        }
        private List<Attribute> _Attributes = new List<Attribute>();

        public List<Attribute> Attributes
        {
            get 
            {
                return _Attributes;
            }
        }
        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        private bool Changed = true;
        private string Render = "";

        public Table(params TableRow[] rows)
        {
            _Rows.AddRange(rows);
        }
        public Table AddRow(params TableRow[] rows)
        {
            _Rows.AddRange(rows);
            Changed = true;
            return this;
        }
        public Table SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            Changed = true;
            return this;
        }
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            StringBuilder result = new StringBuilder("<table>");
            foreach (TableRow row in _Rows)
            {
                result.Append(row);
            }
            Render = result.Append("</table>").ToString();
            return Render;
        }
    }
}