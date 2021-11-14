using System;

namespace Impart
{
    /// <summary>Class that represents text input for the form.</summary>
    public class TextField : FormElement
    {
        internal string textCache;

        /// <summary>Constructor for the text field class.</summary>
        public TextField(string text, string inputid)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new FormError("Input ID cannot be empty or null!", this);
            }
            textCache = $"%^        <label for=\"{inputid}\">{text}</label>%^        <input type=\"text\" name=\"{inputid}\">";
        }

        /// <summary>Constructor for the text field class.</summary>
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