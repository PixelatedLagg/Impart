using System;
using System.Text;

namespace Impart
{
    /// <summary>Class that represents ID/class styling in html.</summary>
    public class Style : IDisposable
    {
        private bool[] setProperties;
        private StringBuilder textBuilder;
        private string id;

        /// <summary>Constructor for the style class.</summary>
        public Style(ImpartCommon.StyleType style, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ImpartError("ID/Class/Element cannot be null!");
            }
            textBuilder = new StringBuilder(1000);
            switch (style)
            {
                case ImpartCommon.StyleType.IDStyle:
                    textBuilder.Append($"#{id} {{%^");
                    break;
                case ImpartCommon.StyleType.EStyle:
                    textBuilder.Append($"{id} {{%^");
                    break;
                case ImpartCommon.StyleType.ClassStyle:
                    textBuilder.Append($".{id} {{%^");
                    break;
            }
            this.id = id;
            setProperties = new bool[] {false, false, false};
        }

        internal void Write(string text)
        {
            if (textBuilder.Length + text.Length > textBuilder.Length)
            {
                textBuilder.Capacity += 1000;
            }
            textBuilder.Append(text);
        }
        /// <summary>Method for setting the style color.</summary>
        public Style SetColor(Color color)
        {
            if (setProperties[0])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            setProperties[0] = true;
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

        /// <summary>Method for setting the style align.</summary>
        public Style SetAlign(string alignment)
        {
            if (setProperties[1])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            setProperties[1] = true;
            if (!Alignment.Any(alignment))
            {
                throw new ImpartError("Invalid alignment value!");
            }
            Write($"    text-align: {alignment};%^");
            return this;
        }

        /// <summary>Method for setting the style margin.</summary>
        public Style SetMargin(int pixels)
        {
            if (setProperties[2])
            {
                throw new ImpartError("Cannot set properties twice!");
            }
            setProperties[2] = true;
            if (pixels < 0)
            {
                throw new ImpartError("Invalid margin value!");
            }
            Write($"    margin: {pixels}px;%^");
            return this;
        }
        internal string Render()
        {
            return $"{textBuilder.Replace("%^", Environment.NewLine).ToString()}}}";
        }

        public void Dispose()
        {
            id = "";
            setProperties = null;
            textBuilder.Clear();
        }
    }
}