using System.Text;

namespace Impart
{
    /// <summary>Class that represents the submit button for Form.</summary>
    public sealed class SubmitField : FormElement
    {
        private string attributes;
        private StringBuilder styleBuilder;
        internal string textCache;

        /// <summary>Creates a SubmitField instance.</summary>
        /// <returns>A SubmitField instance.</returns>
        public SubmitField()
        {
            styleBuilder = new StringBuilder(1000);
            attributes = "";
        }

        private void WriteStyle(string text)
        {
            if (styleBuilder.Length + text.Length > styleBuilder.Capacity)
            {
                styleBuilder.Capacity += 1000;
            }
            styleBuilder.Append(text);
        }

        /// <summary>Set the background color to <paramref name="color"/>.</summary>
        /// <returns>A SubmitField instance.</returns>
        /// <param name="color">The Color to set to.</param>
        public SubmitField SetBGColor(Color color)
        {
            switch (color)
            {
                case Rgb rgb:
                    WriteStyle($"background-color: {rgb};}}");
                    break;
                case Hsl hsl:
                    WriteStyle($"background-color: {hsl};}}");
                    break;
                case Hex hex:
                    WriteStyle($"background-color: {hex};}}");
                    break;
            }
            return this;
        }
        internal string Render()
        {
            if (styleBuilder.Length == 0)
            {
                return $"<input type=\"submit\"{attributes}>";
            }
            else
            {
                return $"<input type=\"submit\" style=\"{styleBuilder.ToString()}\"{attributes}>";
            }
        }
    }
}