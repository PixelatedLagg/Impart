using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Form element.</summary>
    public sealed class Form : IElement, INested
    {
        /// <summary>The ID value of the IElement.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        /// <summary>The Attr values of the instance.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        public ExtAttrList ExtAttrs = new ExtAttrList();
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }

        private List<IFormField> Elements = new List<IFormField>();
        private bool Changed = true;
        private string Render;
        private int _IOID = Ioid.Generate();

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        /// <summary>Creates a Form instance.</summary>
        public Form() { }

        /// <summary>Add <paramref name="textFields"/> to the Form.</summary>
        /// <param name="textFields">The TextField array to add.</param>
        public Form AddTextField(params TextField[] textFields)
        {
            foreach (TextField tf in textFields)
            {
                tf.InputID = Ioid.GenerateOtherUnique();
                Elements.Add(tf);
            }
            Changed = true;
            return this;
        }

        /// <summary>Add <paramref name="checkFields"/> to the Form.</summary>
        /// <param name="checkFields">The CheckField array to add.</param>
        public Form AddCheckField(params CheckField[] checkFields)
        {
            foreach (CheckField cf in checkFields)
            {
                cf.InputID = Ioid.GenerateOtherUnique();
                Elements.Add(cf);
            }
            Changed = true;
            return this;
        }

        /// <summary>Add <paramref name="submitField"/> to the Form.</summary>
        /// <param name="submitField">The SubmitField to add.</param>
        public Form AddSubmitField(SubmitField submitField)
        {
            submitField.InputID = Ioid.GenerateOtherUnique();
            Elements.Add(submitField);
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
            StringBuilder result = new StringBuilder("<form>");
            foreach (IFormField field in Elements)
            {
                result.Append(field);
            }
            Render = result.Append("</form>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Form result = new Form();
            result.Changed = Changed;
            result.Elements = Elements;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result.Render = Render;
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
            result.Changed = Changed;
            result.Elements = Elements;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result.Render = Render;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            return ToString().Remove(Render.Length - 7);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</form>";
        }
    }
}