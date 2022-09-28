using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Form element.</summary>
    public sealed class Form : IElement, INested
    {
        /// <summary>The ID value of the IElement. Returns null if ID is not set.</summary>
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

        internal int _IOID;
        internal EventManager _Events = new EventManager();
        private List<IFormField> Elements = new List<IFormField>();

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        /// <summary>Creates a Form instance.</summary>
        public Form()
        {
            _IOID = Ioid.Generate();
        }

        internal Form(int ioid)
        {
            _IOID = ioid;
        }

        /// <summary>Add a TextField to the Form.</summary>
        /// <param name="textFields">All TextFields to add.</param>
        public Form AddTextField(params TextField[] textFields)
        {
            foreach (TextField textField in textFields)
            {
                textField._OtherIOID = Ioid.GenerateOtherUnique();
                Elements.Add(textField);
            }
            return this;
        }

        /// <summary>Add a CheckField to the Form.</summary>
        /// <param name="checkFields">All CheckFields to add.</param>
        public Form AddCheckField(params CheckField[] checkFields)
        {
            foreach (CheckField checkField in checkFields)
            {
                checkField._OtherIOID = Ioid.GenerateOtherUnique();
                Elements.Add(checkField);
            }
            return this;
        }

        /// <summary>Add a SubmitField to the Form.</summary>
        /// <param name="submitField">The SubmitField to add.</param>
        public Form AddSubmitField(SubmitField submitField)
        {
            Elements.Add(submitField);
            return this;
        }
        
        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("<form");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append($"\"class=\"{_IOID}\"{_Events}");
            }
            foreach (ExtAttr ExtAttr in ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            result.Append('>');
            foreach (IFormField field in Elements)
            {
                result.Append(field);
            }
            return result.Append("</form>").ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Form result = new Form();
            result.Elements = Elements;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Form result = new Form();
            result.Elements = Elements;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            string render = ToString();
            return render.Remove(render.Length - 7);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</form>";
        }
    }
}