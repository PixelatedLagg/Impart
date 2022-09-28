using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Checkbox input for Form.</summary>
    public sealed class CheckField : IFormField
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

        /// <summary>The Attr values of the instance.</summary>
        public List<Attr> Attrs { get; set; } = new List<Attr>();

        /// <summary>The ExtAttr values of the instance.</summary>
        public List<ExtAttr> ExtAttrs { get; set; } = new List<ExtAttr>();

        public Text Text;

        internal double _OtherIOID;
        internal int _IOID;
        internal EventManager _Events = new EventManager();

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        public CheckField(string text)
        {
            Text = new Text(text);
            _IOID = Ioid.Generate();
        }

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        /// <param name="id">The CheckField style ID.</param>
        public CheckField(Text text, string id = null)
        {
            Text = text;
            _IOID = Ioid.Generate();
            ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, id));
        }

        internal CheckField(int ioid)
        {
            _IOID = ioid;
        }


        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<label for=\"{_OtherIOID}\" class=\"{_IOID}\"{_Events}");
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
            return result.Append($">{Text}</label><input type=\"checkbox\" name=\"{_OtherIOID}\">").ToString();
        }
    }
}