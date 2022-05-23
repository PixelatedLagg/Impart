using System;

namespace Impart
{
    /// <summary>Class that represents checkbox input for Form.</summary>
    public sealed class CheckField : FormElement
    {
        private string Render;

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        /// <param name="inputid">The CheckField ID.</param>
        public CheckField(string text, string inputid)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new ImpartError("Input ID cannot be empty or null!");
            }
            Render = $"<label for=\"{inputid}\">{text}</label><input type=\"checkbox\" name=\"{inputid}\">";
        }

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        /// <param name="inputid">The CheckField ID.</param>
        /// <param name="id">The CheckField style ID.</param>
        public CheckField(Text text, string inputid, string id = null)
        {
            if (String.IsNullOrEmpty(inputid))
            {
                throw new ImpartError("Input ID cannot be empty or null!");
            }
            Render = $"<label for=\"{inputid}\">{text}</label><input type=\"checkbox\" name=\"{inputid}\" id=\"{id}\">";
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString() => Render;
    }
}