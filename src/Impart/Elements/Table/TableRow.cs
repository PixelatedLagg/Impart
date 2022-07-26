using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>A row of a Table.</summary>
    public class TableRow
    {
        private string _ID = null;
        
        /// <value>The ID value of the TableRow.</value>
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
        private List<IElement> _Elements = new List<IElement>();

        /// <value>The IElements values of the TableRow.</value>
        public IElement[] Elements
        {
            get
            {
                return _Elements.ToArray();
            }
        }

        /// <value>The Attr values of the instance.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the instance.</value>
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