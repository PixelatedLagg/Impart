using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Submit button for Form.</summary>
    public sealed class SubmitField : FormField
    {
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The attribute values of the SubmitField.</value>
        public List<Attribute> Attributes
        {
            get 
            {
                return _Attributes;
            }
        }
        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a SubmitField instance.</summary>
        public SubmitField() { }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder($"<input type=\"submit\"");
            if (_Attributes.Count != 0)
            {
                result.Append("style=\"");
                foreach (Attribute attribute in _Attributes)
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