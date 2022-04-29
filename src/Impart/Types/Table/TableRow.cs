using System.Text;
using System.Collections.Generic;

namespace Impart
{
    public struct TableRow
    {
        private string _ID = null;
        
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
        private List<TableEntry> _Entries = new List<TableEntry>();

        public List<TableEntry> Entries
        {
            get
            {
                return _Entries;
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

        public TableRow(params TableEntry[] entries)
        {
            _Entries.AddRange(entries);
        }
        public TableRow AddRow(params TableEntry[] entries)
        {
            _Entries.AddRange(entries);
            Changed = true;
            return this;
        }
        public TableRow SetAttribute(AttributeType type, params object[] value)
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
            Changed = false;
            StringBuilder result = new StringBuilder("<table>");
            if (_Attributes.Count != 0)
            {
                result.Append("style\")");
                foreach (Attribute attribute in _Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
                return Render;
            }
            foreach (ExtAttribute extAttribute in _ExtAttributes)
            {
                result.Append(extAttribute);
            }
            Render = result.Append("</table>").ToString();
            return Render;
        }
    }
}