using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Text field for Form.</summary>
    public sealed class TextField : IFormField
    {
        /// <summary>The ID value of the IElement.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private string _Text;

        /// <summary>The Text value of the TextField.</summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                Changed = true;
                _Text = value;
            }
        }

        /// <summary>The Attr values of the SubmitField.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The ExtAttr values of the SubmitField.</summary>
        public ExtAttrList ExtAttrs = new ExtAttrList();
        
        internal double InputID;
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a TextField instance.</summary>
        /// <param name="text">The TextField text.</param>
        public TextField(string text)
        {
            Text = text;
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
            StringBuilder result = new StringBuilder($"<label for=\"{InputID}\"");
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
            Render = result.Append($">{Text}</label><input type=\"text\" name=\"{InputID}\">").ToString();
            return Render;
        }
    }
}