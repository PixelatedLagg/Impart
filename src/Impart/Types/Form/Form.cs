using System.Text;
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
        private List<FormElement> Elements = new List<FormElement>();
        private bool Changed;
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
        /// <returns>A Form instance.</returns>
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
        /// <returns>A Form instance.</returns>
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
        /// <returns>A Form instance.</returns>
        /// <example>
        /// <param name="submitField">The SubmitField to add.</param>
        public Form AddSubmitField(SubmitField submitField)
        {
            Elements.Add(submitField);
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
            StringBuilder result = new StringBuilder("<form>");
            foreach (FormElement element in Elements)
            {
                result.Append(element);
            }
            Render = result.Append("</form>").ToString();
            return Render;
        }
    }

    /// <summary>Base class for all form-related elements.</summary>
    public class FormElement {}
}