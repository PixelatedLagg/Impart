using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Submit button for Form.</summary>
    public sealed class SubmitField : IFormField
    {
        /// <value>The Attr values of the SubmitField.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the SubmitField.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();
        internal double InputID;
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a SubmitField instance.</summary>
        public SubmitField() { }

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
            StringBuilder result = new StringBuilder($"<input type=\"submit\"");
            if (Attrs.Count != 0)
            {
                result.Append("style=\"");
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
            Render = result.Append('>').ToString();
            return Render;
        }
    }
}