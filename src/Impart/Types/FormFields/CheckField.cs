using System;

namespace Impart
{
    /// <summary>Class that represents checkbox input for the form.</summary>
    public class CheckField : FormElement
    {
        internal string textCache;

        /// <summary>Constructor for the check field class.</summary>
        public CheckField(string text, string inputid)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new FormError("Input ID cannot be empty or null!");
            }
            textCache = $"%^        <label for=\"{inputid}\">{text}</label>%^        <input type=\"checkbox\" name=\"{inputid}\">";
        }

        /// <summary>Constructor for the check field class.</summary>
        public CheckField(Text text, string inputid, string id = null)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new FormError("Input ID cannot be empty or null!");
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