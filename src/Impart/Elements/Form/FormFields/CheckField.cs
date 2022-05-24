using Impart.Internal;

namespace Impart
{
    /// <summary>Checkbox input for Form.</summary>
    public sealed class CheckField : FormField
    {
        internal int ID;
        private string Render;

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        public CheckField(string text)
        {
            Render = $"<label for=\"{ID}\">{text}</label><input type=\"checkbox\" name=\"{ID}\">";
        }

        /// <summary>Creates a CheckField instance.</summary>
        /// <param name="text">The CheckField text.</param>
        /// <param name="id">The CheckField style ID.</param>
        public CheckField(Text text, string id = null)
        {
            Render = $"<label for=\"{ID}\">{text}</label><input type=\"checkbox\" name=\"{ID}\" id=\"{id}\">";
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString() => Render;
    }
}