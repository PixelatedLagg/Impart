using System;

namespace Impart
{
    /// <summary>Class that represents ID/class styling in html.</summary>
    public class Style
    {
        private bool[] setProperties;
        private string textCache;
        private string id;

        /// <summary>Constructor for the style class.</summary>
        public Style(ImpartCommon.StyleType style, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new StyleError("ID/Class/Element cannot be null!");
            }
            switch (style)
            {
                case ImpartCommon.StyleType.IDStyle:
                    textCache = $"#{id} {{%^";
                    break;
                case ImpartCommon.StyleType.EStyle:
                    textCache = $"{id} {{%^";
                    break;
                case ImpartCommon.StyleType.ClassStyle:
                    textCache = $".{id} {{%^";
                    break;
            }
            this.id = id;
            setProperties = new bool[] {false, false, false};
        }

        /// <summary>Method for setting the style color.</summary>
        public Style SetColor(Color color)
        {
            if (setProperties[0])
            {
                throw new StyleError("Cannot set properties twice!");
            }
            setProperties[0] = true;
            switch (color.GetType().FullName)
            {
                case "Impart.Rgb":
                    Rgb rgb = (Rgb)color;
                    textCache += $"    color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^";
                    break;
                case "Impart.Hsl":
                    Hsl hsl = (Hsl)color;
                    textCache += $"    color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^";
                    break;
                case "Impart.Hex":
                    Hex hex = (Hex)color;
                    textCache += $"    color: #{hex.hex};%^";
                    break;
            }
            return this;
        }

        /// <summary>Method for setting the style align.</summary>
        public Style SetAlign(string alignment)
        {
            if (setProperties[1])
            {
                throw new StyleError("Cannot set properties twice!");
            }
            setProperties[1] = true;
            if (!Alignment.Any(alignment))
            {
                throw new StyleError("Invalid alignment value!");
            }
            textCache += $"    text-align: {alignment};%^";
            return this;
        }

        /// <summary>Method for setting the style margin.</summary>
        public Style SetMargin(int pixels)
        {
            if (setProperties[2])
            {
                throw new StyleError("Cannot set properties twice!");
            }
            setProperties[2] = true;
            if (pixels < 0)
            {
                throw new StyleError("Invalid margin value!");
            }
            textCache += $"    margin: {pixels}px;%^";
            return this;
        }
        internal string Render()
        {
            return $"{textCache}}}".Replace("%^", Environment.NewLine);
        }
    }
}