using System;
namespace Impart
{
    public class TextField : FormElement
    {
        internal string textCache;
        public TextField(string text, string inputid)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new FormError("Input ID cannot be empty or null!", this);
            }
            textCache = $"%^        <label for=\"{inputid}\">{text}</label>%^        <input type=\"text\" name=\"{inputid}\">";
        }
        public TextField(Text text, string inputid, string id = null)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new FormError("Input ID cannot be empty or null!", this);
            }
            if (text.id == null)
            {
                textCache = $"%^        <label for=\"{inputid}\">%^            <p{text.attributes}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"text\" name=\"{inputid}\" id=\"{id}\">";
            }
            else
            {
                textCache = $"%^        <label for=\"{inputid}\">%^            <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"text\" name=\"{inputid}\" id=\"{id}\">";
            }
        }
    }
}