using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>An element attribute.</summary>
    public struct Attribute
    {
        private AttributeType _type;
        private object[] _value;

        /// <value>The attribute type.</value>
        public AttributeType type
        {
            get
            {
                return _type;
            }
        }

        /// <value>The attribute value(s).</value>
        public object[] value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>Creates the attribute with the type and value(s).</summary>
        /// <returns>An Attribute instance.</returns>
        /// <param name="type">The attribute type.</param>
        /// <param name="value">The attribute value.</param>
        public Attribute(AttributeType type, params object[] value)
        {
            _type = type;
            _value = value;
        }
        internal static void AddAttribute(ref StringBuilder attributes, ref StringBuilder style, ref List<Attribute> attributeList, AttributeType type, params object[] value)
        {
            switch (type)
            {
                case AttributeType.BackgroundColor:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Rgb rgb:
                            style.Append($" background-color: {rgb};");
                            break;
                        case Hsl hsl:
                            style.Append($" background-color: {hsl};");
                            break;
                        case Hex hex:
                            style.Append($" background-color: {hex};");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    attributeList.Add(new Attribute(AttributeType.BackgroundColor, value[0]));
                    break;
                case AttributeType.ForegroundColor:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Rgb rgb:
                            style.Append($" color: {rgb};");
                            break;
                        case Hsl hsl:
                            style.Append($" color: {hsl};");
                            break;
                        case Hex hex:
                            style.Append($" color: {hex};");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    attributeList.Add(new Attribute(AttributeType.ForegroundColor, value[0]));
                    break;
                case AttributeType.Alignment:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Alignment.Center:
                            style.Append(" align: center;");
                            break;
                        case Alignment.Justify:
                            style.Append(" align: justify;");
                            break;
                        case Alignment.Left:
                            style.Append(" align: left;");
                            break;
                        case Alignment.Right:
                            style.Append(" align: right;");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    break;
                case AttributeType.FontFamily:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case FontFamily.AndaleMono:
                            style.Append(" font-family: Andale Mono;");
                            break;
                        case FontFamily.AppleChancery:
                            style.Append(" font-family: Apple Chancery;");
                            break;
                        case FontFamily.Arial:
                            style.Append(" font-family: Arial;");
                            break;
                        case FontFamily.AvantaGarde:
                            style.Append(" font-family: Avanta Garde;");
                            break;
                        case FontFamily.Baskerville:
                            style.Append(" font-family: Baskerville;");
                            break;
                        case FontFamily.BigCaslon:
                            style.Append(" font-family: Big Caslon;");
                            break;
                        case FontFamily.BodoniMT:
                            style.Append(" font-family: Bodoni MT;");
                            break;
                        case FontFamily.BookAntiqua:
                            style.Append(" font-family: Book Antiqua;");
                            break;
                        case FontFamily.Bookman:
                            style.Append(" font-family: Bookman;");
                            break;
                        case FontFamily.BradleyHand:
                            style.Append(" font-family: Bradley Hand;");
                            break;
                        case FontFamily.BrushScriptMT:
                            style.Append(" font-family: Brush Script MT;");
                            break;
                        case FontFamily.BrushScriptStd:
                            style.Append(" font-family: Brush Script Std;");
                            break;
                        case FontFamily.Calibri:
                            style.Append(" font-family: Calibri;");
                            break;
                        case FontFamily.CalistoMT:
                            style.Append(" font-family: Calisto MT;");
                            break;
                        case FontFamily.Cambria:
                            style.Append(" font-family: Cambria;");
                            break;
                        case FontFamily.Candara:
                            style.Append(" font-family: Candara;");
                            break;
                        case FontFamily.CenturyGothic:
                            style.Append(" font-family: Century Gothic;");
                            break;
                        case FontFamily.ComicSans:
                            style.Append(" font-family: Comic Sans;");
                            break;
                        case FontFamily.ComicSansMS:
                            style.Append(" font-family: Comic Sans MS;");
                            break;
                        case FontFamily.Consolas:
                            style.Append(" font-family: Consolas;");
                            break;
                        case FontFamily.Coronetscript:
                            style.Append(" font-family: Coronet script;");
                            break;
                        case FontFamily.Courier:
                            style.Append(" font-family: Courier;");
                            break;
                        case FontFamily.CourierNew:
                            style.Append(" font-family: Courier New;");
                            break;
                        case FontFamily.Didot:
                            style.Append(" font-family: Didot;");
                            break;
                        case FontFamily.Florence:
                            style.Append(" font-family: Florence;");
                            break;
                        case FontFamily.FranklinGothicMedium:
                            style.Append(" font-family: Franklin Gothic Medium;");
                            break;
                        case FontFamily.Futara:
                            style.Append(" font-family: Futara;");
                            break;
                        case FontFamily.Garamond:
                            style.Append(" font-family: Garamond;");
                            break;
                        case FontFamily.Geneva:
                            style.Append(" font-family: Geneva;");
                            break;
                        case FontFamily.Georgia:
                            style.Append(" font-family: Georgia;");
                            break;
                        case FontFamily.GillSans:
                            style.Append(" font-family: Gill Sans;");
                            break;
                        case FontFamily.GoudyOldStyle:
                            style.Append(" font-family: Goudy Old Style;");
                            break;
                        case FontFamily.Helvetica:
                            style.Append(" font-family: Helvetica;");
                            break;
                        case FontFamily.HoeflerText:
                            style.Append(" font-family: Hoefler Text;");
                            break;
                        case FontFamily.LucidaBright:
                            style.Append(" font-family: Lucida Bright;");
                            break;
                        case FontFamily.LucidaConsole:
                            style.Append(" font-family: Lucida Console;");
                            break;
                        case FontFamily.LucidaSans:
                            style.Append(" font-family: Lucida Sans;");
                            break;
                        case FontFamily.LucidaSansTypewriter:
                            style.Append(" font-family: Lucida Sans Typewriter;");
                            break;
                        case FontFamily.Monaco:
                            style.Append(" font-family: Monaco;");
                            break;
                        case FontFamily.NewCenturySchoolbook:
                            style.Append(" font-family: New Century Schoolbook;");
                            break;
                        case FontFamily.Noto:
                            style.Append(" font-family: Noto;");
                            break;
                        case FontFamily.Optima:
                            style.Append(" font-family: Optima;");
                            break;
                        case FontFamily.Palatino:
                            style.Append(" font-family: Palatino;");
                            break;
                        case FontFamily.Parkavenue:
                            style.Append(" font-family: Parkavenue;");
                            break;
                        case FontFamily.Perpetua:
                            style.Append(" font-family: Perpetua;");
                            break;
                        case FontFamily.Rockwell:
                            style.Append(" font-family: Rockwell;");
                            break;
                        case FontFamily.RockwellExtraBold:
                            style.Append(" font-family: Rockwell Extra Bold;");
                            break;
                        case FontFamily.SegoeUI:
                            style.Append(" font-family: Segoe UI;");
                            break;
                        case FontFamily.SnellRoundhan:
                            style.Append(" font-family: Snell Roundhan;");
                            break;
                        case FontFamily.TimesNewRoman:
                            style.Append(" font-family: Times New Roman;");
                            break;
                        case FontFamily.TrebuchetMS:
                            style.Append(" font-family: Trebuchet MS;");
                            break;
                        case FontFamily.URWChancery:
                            style.Append(" font-family: URW Chancery;");
                            break;
                        case FontFamily.Verdana:
                            style.Append(" font-family: Verdana;");
                            break;
                        case FontFamily.ZapfChancery:
                            style.Append(" font-family: Zapf Chancery;");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    attributeList.Add(new Attribute(AttributeType.FontFamily, value[0]));
                    break;
                case AttributeType.FontSize:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            style.Append($" font-size: {pct};");
                            break;
                        case Pixels pxls:
                            style.Append($" font-size: {pxls};");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    attributeList.Add(new Attribute(AttributeType.FontFamily, value[0]));
                    break;
                case AttributeType.Margin:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            style.Append($" margin: {pct};");
                            break;
                        case Pixels pxls:
                            style.Append($" margin: {pxls};");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    attributeList.Add(new Attribute(AttributeType.Margin, value[0]));
                    break;
                case AttributeType.Padding:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            style.Append($" padding: {pct};");
                            break;
                        case Pixels pxls:
                            style.Append($" padding: {pxls};");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    attributeList.Add(new Attribute(AttributeType.Padding, value[0]));
                    break;
                case AttributeType.HoverMessage:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    if (value[0] as string != null)
                    {
                        attributes.Append($" title=\"{value[0]}\"");
                        attributeList.Add(new Attribute(AttributeType.HoverMessage, value[0]));
                    }
                    else
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    break;
                case AttributeType.Size:
                    if (value.Length != 2)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            style.Append($" width: {pct};");
                            break;
                        case Pixels pxls:
                            style.Append($" width: {pxls};");
                            break;
                        case null:
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[1])
                    {
                        case Percent pct:
                            style.Append($" height: {pct};");
                            break;
                        case Pixels pxls:
                            style.Append($" height: {pxls};");
                            break;
                        case null:
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    attributeList.Add(new Attribute(AttributeType.Size, value[0], value[1]));
                    break;
                case AttributeType.Border:
                    if (value.Length != 3)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            style.Append($" border: {pct}");
                            break;
                        case Pixels pxls:
                            style.Append($" border: {pxls}");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[1])
                    {
                        case Border.Dashed:
                            style.Append($" dashed");
                            break;
                        case Border.Dotted:
                            style.Append($" dotted");
                            break;
                        case Border.Double:
                            style.Append($" double");
                            break;
                        case Border.In3D:
                            style.Append($" inset");
                            break;
                        case Border.Normal:
                            style.Append($" solid");
                            break;
                        case Border.Out3D:
                            style.Append($" outset");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[2])
                    {
                        case Rgb rgb:
                            style.Append($" {rgb};");
                            break;
                        case Hsl hsl:
                            style.Append($" {hsl};");
                            break;
                        case Hex hex:
                            style.Append($" {hex};");
                            break;
                    }
                    attributeList.Add(new Attribute(AttributeType.Border, value[0], value[1], value[2]));
                    break;
                default:
                    throw new ImpartError("Invalid attribute parameters.");
            }
        }
    }
}