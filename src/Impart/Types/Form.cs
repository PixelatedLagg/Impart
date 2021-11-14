namespace Impart
{
    public class Form
    {
        private string textCache;
        public Form()
        {
            textCache = "%^    <form>";
        }
        public Form AddTextField(TextField textField)
        {
            textCache += textField.textCache;
            return this;
        }
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
    public class FormElement {}
}