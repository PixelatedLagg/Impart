using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>The main input class in Impart.</summary>
    public sealed class Form : Element
    {
        private string _ID;

        /// <value>The ID value of the Form.</value>
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
        private List<FormField> Elements = new List<FormField>();
        private bool Changed = true;
        private string Render;
        private int IOIDValue = Ioid.Generate();
        int Element.IOID
        {
            get
            {
                return IOIDValue;
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
                Elements.Add(cf);
            }
            Changed = true;
            return this;
        }

        /// <summary>Add <paramref name="submitField"/> to the Form.</summary>
        /// <param name="submitField">The SubmitField to add.</param>
        public Form AddSubmitField(SubmitField submitField)
        {
            Elements.Add(submitField);
            Changed = true;
            return this;
        }
        
        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder("<form>");
            foreach (FormField field in Elements)
            {
                result.Append(field);
            }
            Render = result.Append("</form>").ToString();
            return Render;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Form result = new Form();
            result._ID = _ID;
            result.Changed = Changed;
            result.Elements = Elements;
            result.IOIDValue = IOIDValue;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Form result = new Form();
            result._ID = _ID;
            result.Changed = Changed;
            result.Elements = Elements;
            result.IOIDValue = IOIDValue;
            result.Render = Render;
            return result;
        }
    }
}