using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Submit button for Form.</summary>
    public sealed class SubmitField : FormField
    {
        /// <value>The attribute values of the SubmitField.</value>
        public AttrList Attrs = new AttrList();
        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a SubmitField instance.</summary>
        public SubmitField() { }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder($"<input type=\"submit\"");
            if (Attrs.Count != 0)
            {
                result.Append("style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append('"');
                foreach (ExtAttr ExtAttr in _ExtAttrs)
                {
                    result.Append(ExtAttr);
                }
                Render = result.Append('>').ToString();
                return Render;
            }
            foreach (ExtAttr ExtAttr in _ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            Render = result.Append('>').ToString();
            return Render;
        }
    }
}