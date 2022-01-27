namespace Impart
{
    /// <summary>The main input class in Impart.</summary>
    public class Form
    {
        private string textCache;

        /// <summary>Creates a Form instance.</summary>
        /// <returns>A Form instance.</returns>
        public Form()
        {
            textCache = "<form>";
        }

        /// <summary>Add <paramref name="textFields"/> to the Form.</summary>
        /// <param name="textFields">The TextField array to add.</param>
        public Form AddTextField(params TextField[] textFields)
        {
            foreach (TextField tf in textFields)
            {
                textCache += tf.textCache;
            }
            return this;
        }

        /// <summary>Add <paramref name="checkFields"/> to the Form.</summary>
        /// <returns>A Form instance.</returns>
        /// <param name="checkFields">The CheckField array to add.</param>
        public Form AddCheckField(params CheckField[] checkFields)
        {
            foreach (CheckField cf in checkFields)
            {
                textCache += cf.textCache;
            }
            return this;
        }

        /// <summary>Add <paramref name="submitField"/> to the Form.</summary>
        /// <returns>A Form instance.</returns>
        /// <example>
        /// <param name="submitField">The SubmitField to add.</param>
        public Form AddSubmitField(SubmitField submitField)
        {
            textCache += submitField.Render();
            return this;
        }
        internal string Render()
        {
            return $"{textCache}</form>";
        }
    }

    /// <summary>Base class for all form-related elements.</summary>
    public class FormElement {}
}