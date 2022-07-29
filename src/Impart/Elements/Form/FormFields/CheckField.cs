using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Checkbox input for Form.</summary>
    public sealed class CheckField : IFormField
    {
        /// <summary>The Attr values of the SubmitField.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The ExtAttr values of the SubmitField.</summary>
        public ExtAttrList ExtAttrs = new ExtAttrList();
        
        internal double InputID;
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        public CheckField(string text)
        {
            Render = $"<label for=\"{InputID}\">{text}</label><input type=\"checkbox\" name=\"{InputID}\">";
        }

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        /// <param name="id">The CheckField style ID.</param>
        public CheckField(Text text, string id = null)
        {
            Render = $"<label for=\"{InputID}\">{text}</label><input type=\"checkbox\" name=\"{InputID}\">";
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
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