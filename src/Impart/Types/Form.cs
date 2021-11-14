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
        public Form AddTextField(TextField textField)
        {
            textCache += textField.textCache;
            return this;
        }

        /// <summary>Method for adding a check field to the form.</summary>
        public Form AddCheckField(CheckField checkField)
        {
            textCache += checkField.textCache;
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