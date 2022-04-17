using System.Collections.Generic;
using System.Text;

namespace Impart
{
    /// <summary>Class that represents the submit button for Form.</summary>
    public sealed class SubmitField : FormElement
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
        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a SubmitField instance.</summary>
        public SubmitField() { }

        public SubmitField SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            Changed = true;
            return this;
        }

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
                foreach (ExtAttribute extAttribute in _ExtAttributes)
                {
                    result.Append(extAttribute);
                }
                Render = result.Append('>').ToString();
                return Render;
            }
            foreach (ExtAttribute extAttribute in _ExtAttributes)
            {
                result.Append(extAttribute);
            }
            Render = result.Append('>').ToString();
            return Render;
        }
    }
}