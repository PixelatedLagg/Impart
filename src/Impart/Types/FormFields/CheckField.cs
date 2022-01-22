using System;

namespace Impart
{
    /// <summary>Class that represents checkbox input for the form.</summary>
    public class CheckField : FormElement
    {
        internal string textCache;

        /// <summary>Creates a CheckField instance with <paramref name="text"/> as the text and <paramref name="inputid"/> as the ID.</summary>
        /// See <see cref="Form.AddCheckField(CheckField[])"/> to add the CheckField.
        /// <returns>A CheckField instance.</returns>
        /// <example>
        /// <code>
        /// CheckField checkField = new CheckField("example", "exampleID");
        /// </code>
        /// </example>
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
        /// See <see cref="Form.AddCheckField(CheckField[])"/> to add the CheckField.
        /// See <see cref="Text.Text()"/> create a Text instance.
        /// <returns>A CheckField instance.</returns>
        /// <example>
        /// <code>
        /// CheckField checkField = new CheckField("example", "exampleID");
        /// </code>
        /// </example>
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
                textCache = $"%^        <label for=\"{inputid}\">%^            <p{text.attributes}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"checkbox\" name=\"{inputid}\" id=\"{id}\">";
            }
            else
            {
                textCache = $"%^        <label for=\"{inputid}\">%^            <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>%^        </label>%^        <input type=\"checkbox\" name=\"{inputid}\" id=\"{id}\">";
            }
        }
    }
}