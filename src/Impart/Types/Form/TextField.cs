using System;

namespace Impart
{
    /// <summary>Text field in Form.</summary>
    public sealed class TextField : FormElement
    {
        private string Render;

        /// <summary>Creates a TextField instance with <paramref name="text"/> as the text and <paramref name="inputid"/> as the ID.</summary>
        /// <returns>A TextField instance.</returns>
        /// <param name="text">The TextField text.</param>
        /// <param name="inputid">The TextField ID.</param>
        public TextField(string text, string inputid)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new ImpartError("Input ID cannot be empty or null!");
            }
            Render = $"<label for=\"{inputid}\">{text}</label><input type=\"text\" name=\"{inputid}\">";
        }

        /// <summary>Creates a TextField instance with <paramref name="text"/> as the text and <paramref name="inputid"/> as the ID.</summary>
        /// <returns>A TextField instance.</returns>
        /// <param name="text">The TextField text.</param>
        /// <param name="inputid">The TextField ID.</param>
        /// <param name="inputid">The TextField style ID.</param>
        public TextField(Text text, string inputid, string id = null)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new ImpartError("Input ID cannot be empty or null!");
            }
            Render = $"<label for=\"{inputid}\">{text}</label><input type=\"text\" name=\"{inputid}\" id=\"{id}\">";
        }
    }
}