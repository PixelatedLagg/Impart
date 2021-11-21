namespace Impart
{
    /// <summary>Class that represents the form tag in html.</summary>
    public class Form
    {
        private string textCache;

        /// <summary>Constructor for the form class.</summary>
        public Form()
        {
            textCache = "%^    <form>";
        }

        /// <summary>Method for adding a text field to the form.</summary>
        public Form AddTextField(params TextField[] textFields)
        {
            foreach (TextField tf in textFields)
            {
                textCache += tf.textCache;
            }
            return this;
        }

        /// <summary>Method for adding a check field to the form.</summary>
        public Form AddCheckField(params CheckField[] checkFields)
        {
            foreach (CheckField cf in checkFields)
            {
                textCache += cf.textCache;
            }
            return this;;;;;;
        }

        /// <summary>Method for adding a submit field to the form.</summary>
        public Form AddSubmitField(SubmitField submitField)
        {
            textCache += submitField.Render();
            return this;
        }
        internal string Render()
        {
            return $"{textCache}%^    </form>";
        }
    }

    /// <summary>Base class for all form-related elements.</summary>
    public class FormElement {}
}