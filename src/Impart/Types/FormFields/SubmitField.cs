using System;

namespace Impart
{
    /// <summary>Class that represents the submit button for the form.</summary>
    public class SubmitField : FormElement
    {
        private string attributes;
        private string style;
        internal string textCache;

        /// <summary>Constructor for the submit field class.</summary>
        public SubmitField()
        {
            style = "";
            attributes = "";
        }

        /// <summary>Method for setting the button background color.</summary>
        public SubmitField SetBGColor(Color color)
        {
            switch (color)
            {
                case Rgb rgb:
                    style += $"    background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});%^}}";
                    break;
                case Hsl hsl:
                    style += $"    background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);%^}}";
                    break;
                case Hex hex:
                    style += $"    background-color: #{hex.hex};%^}}";
                    break;
            }
            return this;
        }
        internal string Render()
        {
            if (style == "")
            {
                return $"%^        <input type=\"submit\"{attributes}>";
            }
            else
            {
                return $"%^        <input type=\"submit\" style=\"{style}\"{attributes}>";
            }
        }
    }
}