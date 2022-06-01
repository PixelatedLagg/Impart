namespace Impart
{
    /// <summary>Arguments for setting the border.</summary>
    public class BorderArgs
    {
        /// <value>The border line type.</value>
        public BorderType Type;

        /// <value>The border line width.</value>
        public Length Width;

        /// <value>The border color.</value>
        public Color Color;

        /// <summary>Creates a BorderArgs instance</summary>
        /// <param name="type">The border type.</param>
        /// <param name="width">The border line width.</param>
        /// <param name="color">The border color.</param>
        public BorderArgs(BorderType type, Length width, Color color)
        {
            Type = type;
            Width = width;
            Color = color;
        }
    }
}