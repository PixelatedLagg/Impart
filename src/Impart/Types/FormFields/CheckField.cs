using System;

namespace Impart
{
    public class CheckField : FormElement
    {
        internal string textCache;
        public CheckField(string text, string inputid)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new FormError("Input ID cannot be empty or null!", this);
            }
            textCache = $"%^        <label for=\"{inputid}\">{text}</label>%^        <input type=\"checkbox\" name=\"{inputid}\">";
        }
        public CheckField(Text text, string inputid, string id = null)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new FormError("Input ID cannot be empty or null!", this);
            }
            if (text.id == null)
            {
                textCache = $"%^        <label for=\"{inputid}\">%^            <p{text.attributes}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"checkbox\" name=\"{inputid}\" id=\"{id}\">";
            }
            else
            {
                textCache = $"%^        <label for=\"{inputid}\">%^            <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"checkbox\" name=\"{inputid}\" id=\"{id}\">";
            }
        }
    }
}