using Impart.Internal;

namespace Impart
{
    /// <summary>Text field for Form.</summary>
    public sealed class TextField : FormField
    {
        internal int ID;
        private string Render;

        /// <summary>Creates a TextField instance.</summary>
        /// <param name="text">The TextField text.</param>
        public TextField(string text)
        {
            Render = $"<label for=\"{ID}\">{text}</label><input type=\"text\" name=\"{ID}\">";
        }

        /// <summary>Creates a TextField instance.</summary>
        /// <param name="text">The TextField text.</param>
        /// <param name="id">The TextField style ID.</param>
        public TextField(Text text, string id = null)
        {
            Render = $"<label for=\"{ID}\">{text}</label><input type=\"text\" name=\"{ID}\" id=\"{id}\">";
        }
    }
}