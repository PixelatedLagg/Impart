namespace Impart
{
    /// <summary>Arguments for setting the border.</summary>
    public class BorderArgs
    {
        /// <summary>The border line type.</summary>
        public BorderType Type;

        /// <summary>The border line width.</summary>
        public Length Width;

        /// <summary>The border color.</summary>
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