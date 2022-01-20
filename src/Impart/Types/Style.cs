using System;
using System.Text;

namespace Impart
{
    /// <summary>Style an ID, element, or class.</summary>
    /// <remarks>Very important class in Impart.</remarks>
    public class Style : IDisposable
    {
        private StringBuilder textBuilder;
        private string id;

        /// <value>The ID value of the Style.</value>
        public string ID
        {
            get
            {
                return id;
            }
        }

        /// <summary>Creates a Style instance with <paramref name="path"/> as the style type and <paramref name="id"/> as the ID/element/class..</summary>
        /// See <see cref="Style.Dispose()"/> to dispose the Style.
        /// <returns>A Style instance.</returns>
        /// <example>
        /// <code>
        /// using (var example = new Style(StyleType.IDStyle, "example"))
        /// {
        ///     
        /// }
        /// </code>
        /// </example>
        /// <param name="style">The style type.</param>
        /// <param name="id">The ID/element/class.</param>
        public Style(StyleType style, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ImpartError("ID/Class/Element cannot be null!");
            }
            textBuilder = new StringBuilder(1000);
            switch (style)
            {
                case StyleType.IDStyle:
                    textBuilder.Append($"#{id} {{%^");
                    break;
                case StyleType.EStyle:
                    textBuilder.Append($"{id} {{%^");
                    break;
                case StyleType.ClassStyle:
                    textBuilder.Append($".{id} {{%^");
                    break;
            }
            this.id = id;
        }

        internal void Write(string text)
        {
            if (textBuilder.Length + text.Length > textBuilder.Length)
            {
                textBuilder.Capacity += 1000;
            }
            textBuilder.Append(text);
        }

        /// <summary>Set the Style color to <paramref name="color"/>.</summary>
        /// See <see cref="Rgb.Rgb(int, int, int)"/> to create a RGB instance.
        /// See <see cref="Hsl.Hsl(float, float, float)"/> to create a HSL instance.
        /// See <see cref="Hex.Hex(string)"/> to create a Hex instance.
        /// <returns>A Style instance.</returns>
        /// <example>
        /// <code>
        /// style.SetColor(color);
        /// </code>
        /// </example>
        /// <param name="color">The Color instance to set to.</param>
        public Style SetColor(Color color)
        {
            switch (color)
            {
                case Rgb rgb:
                    Write($"    color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^");
                    break;
                case Hsl hsl:
                    Write($"    color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^");
                    break;
                case Hex hex:
                    Write($"    color: #{hex.hex};%^");
                    break;
            }
            return this;
        }

        /// <summary>Set the Style alignment to <paramref name="alignment"/>.</summary>
        /// See <see cref="Alignment"/> to create an Alignment instance.
        /// <returns>A Style instance.</returns>
        /// <example>
        /// <code>
        /// style.SetAlign(align);
        /// </code>
        /// </example>
        /// <param name="alignment">The Alignment instance to set to.</param>
        public Style SetAlign(string alignment)
        {
            if (!Alignment.Any(alignment))
            {
                throw new ImpartError("Invalid alignment value!");
            }
            Write($"    text-align: {alignment};%^");
            return this;
        }

        /// <summary>Set the Style margin to <paramref name="size"/>.</summary>
        /// See <see cref="Pixels.Pixels(int)"/> to create a Pixels instance.
        /// See <see cref="Percent.Percent(int)"/> to create a Percent instance.
        /// <returns>A Style instance.</returns>
        /// <example>
        /// <code>
        /// style.SetMargin(size);
        /// </code>
        /// </example>
        /// <param name="size">The Measurement instance to set to.</param>
        public Style SetMargin(Measurement size)
        {
            switch (size)
            {
                case Pixels pixels:
                    Write($"    margin: {pixels.pixels}px;%^");
                    break;
                case Percent percent:
                    Write($"    margin: {percent.percent}%;%^");
                    break;
            }
            return this;
        }
        internal string Render()
        {
            return $"{textBuilder.Replace("%^", Environment.NewLine).ToString()}}}";
        }

        /// <summary>Dispose of all the associated variables in the Style instance. Included to support using() statements.</summary>
        /// <example>
        /// <code>
        /// style.Dispose();
        /// </code>
        /// </example>
        public void Dispose()
        {
            id = "";
            textBuilder.Clear();
        }
    }

    /// <summary>List of every type of Style. Includes ID, element, and class.</summary>
    public enum StyleType
    {
        IDStyle = 0,
        EStyle = 1,
        ClassStyle = 2
    }
}