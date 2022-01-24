using System;

namespace Impart
{
    /// <summary>Class that represents checkbox input for Form.</summary>
    public class CheckField : FormElement
    {
        internal string textCache;

        /// <summary>Creates a CheckField instance with <paramref name="text"/> as the text and <paramref name="inputid"/> as the ID.</summary>
        /// <returns>A CheckField instance.</returns>
        /// <param name="text">The CheckField text.</param>
        /// <param name="inputid">The CheckField ID.</param>
        public CheckField(string text, string inputid)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new ImpartError("Input ID cannot be empty or null!");
            }
            textCache = $"%^        <label for=\"{inputid}\">{text}</label>%^        <input type=\"checkbox\" name=\"{inputid}\">";
        }

        /// <summary>Creates a CheckField instance with <paramref name="text"/> as the text and <paramref name="inputid"/> as the ID.</summary>
        /// <returns>A CheckField instance.</returns>
        /// <param name="text">The CheckField text.</param>
        /// <param name="inputid">The CheckField ID.</param>
        /// <param name="inputid">The CheckField style ID.</param>
        public CheckField(Text text, string inputid, string id = null)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new ImpartError("Input ID cannot be empty or null!");
            }
            if (text.id == null)
            {
                textCache = $"%^        <label for=\"{inputid}\">%^            <p{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"checkbox\" name=\"{inputid}\" id=\"{id}\">";
            }
            else
            {
                textCache = $"%^        <label for=\"{inputid}\">%^            <p id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"checkbox\" name=\"{inputid}\" id=\"{id}\">";
            }
        }
    }
}