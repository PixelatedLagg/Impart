using System.Text;

namespace Impart
{
    /// <summary>An element attribute.</summary>
    public struct Attribute
    {
        private AttributeType _Type;

        /// <Value>The attribute Type.</Value>
        public AttributeType Type
        {
            get
            {
                return _Type;
            }
        }
        private object[] _Value;

        /// <Value>The attribute Value(s).</Value>
        public object[] Value
        {
            get
            {
                return _Value;
            }
        }

        /// <summary>Creates the attribute with the Type and Value(s).</summary>
        /// <returns>An Attribute instance.</returns>
        /// <param name="Type">The attribute Type.</param>
        /// <param name="Value">The attribute Value.</param>
        public Attribute(AttributeType type, params object[] value)
        {
            _Type = type;
            _Value = value;
        }
        
        public override string ToString()
        {
            switch (Type)
            {
                case AttributeType.BackgroundColor:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return Color.Convert(Value[0]) switch
                    {
                        Rgb rgb => $" background-color: {rgb};",
                        Hsl hsl => $" background-color: {hsl};",
                        Hex hex => $" background-color: {hex};",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttributeType.ForegroundColor:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return Color.Convert(Value[0]) switch
                    {
                        Rgb rgb => $" color: {rgb};",
                        Hsl hsl => $" color: {hsl};",
                        Hex hex => $" color: {hex};",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttributeType.Alignment:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return Value[0] switch
                    {
                        Alignment.Center => " align: center;",
                        Alignment.Justify => " align: justify;",
                        Alignment.Left => " align: left;",
                        Alignment.Right => " align: right;",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttributeType.FontFamily:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return Value[0] switch
                    {
                        FontFamily.AndaleMono => " font-family: Andale Mono;",
                        FontFamily.AppleChancery => " font-family: Apple Chancery;",
                        FontFamily.Arial => " font-family: Arial;",
                        FontFamily.AvantaGarde => " font-family: Avanta Garde;",
                        FontFamily.Baskerville => " font-family: Baskerville;",
                        FontFamily.BigCaslon => " font-family: Big Caslon;",
                        FontFamily.BodoniMT => " font-family: Bodoni MT;",
                        FontFamily.BookAntiqua => " font-family: Book Antiqua;",
                        FontFamily.Bookman => " font-family Book Antiqua;",
                        FontFamily.BradleyHand => " font-family: Bradley Hand;",
                        FontFamily.BrushScriptMT => " font-family: Brush Script MT;",
                        FontFamily.BrushScriptStd => " font-family: Brush Script Std;",
                        FontFamily.Calibri => " font-family: Calibri;",
                        FontFamily.CalistoMT => " font-family: Calisto MT;",
                        FontFamily.Cambria => " font-family: Cambria;",
                        FontFamily.Candara => " font-family: Candara;",
                        FontFamily.CenturyGothic => " font-family: Century Gothic;",
                        FontFamily.ComicSans => " font-family: Comic Sans;",
                        FontFamily.ComicSansMS => " font-family: Comic Sans MS;",
                        FontFamily.Consolas => " font-family: Consolas;",
                        FontFamily.Coronetscript => " font-family: Coronet script;",
                        FontFamily.Courier => " font-family: Courier;",
                        FontFamily.CourierNew => " font-family: Courier New;",
                        FontFamily.Didot => " font-family: Didot;",
                        FontFamily.Florence => " font-family: Florence;",
                        FontFamily.FranklinGothicMedium => " font-family: Franklin Gothic Medium;",
                        FontFamily.Futara => " font-family: Futara;",
                        FontFamily.Garamond => " font-family: Garamond;",
                        FontFamily.Geneva => " font-family: Geneva;",
                        FontFamily.Georgia => " font-family: Georgia;",
                        FontFamily.GillSans => " font-family: Gill Sans;",
                        FontFamily.GoudyOldStyle => " font-family: Goudy Old Style;",
                        FontFamily.Helvetica => " font-family: Helvetica;",
                        FontFamily.HoeflerText => " font-family: Hoefler Text;",
                        FontFamily.LucidaBright => " font-family: Lucida Bright;",
                        FontFamily.LucidaConsole => " font-family: Lucida Console;",
                        FontFamily.LucidaSans => " font-family: Lucida Sans;",
                        FontFamily.LucidaSansTypewriter => " font-family: Lucida Sans Typewriter;",
                        FontFamily.Monaco => " font-family: Monaco;",
                        FontFamily.NewCenturySchoolbook => " font-family: New Century Schoolbook;",
                        FontFamily.Noto => " font-family: Noto;",
                        FontFamily.Optima => " font-family: Optima;",
                        FontFamily.Palatino => " font-family: Palatino;",
                        FontFamily.Parkavenue => " font-family: Parkavenue;",
                        FontFamily.Perpetua => " font-family: Perpetua;",
                        FontFamily.Rockwell => " font-family: Rockwell;",
                        FontFamily.RockwellExtraBold => " font-family: Rockwell Extra Bold;",
                        FontFamily.SegoeUI => " font-family: Segoe UI;",
                        FontFamily.SnellRoundhan => " font-family: Snell Roundhan;",
                        FontFamily.TimesNewRoman => " font-family: Times New Roman;",
                        FontFamily.TrebuchetMS => " font-family: Trebuchet MS;",
                        FontFamily.URWChancery => " font-family: URW Chancery;",
                        FontFamily.Verdana => " font-family: Verdana;",
                        FontFamily.ZapfChancery => " font-family: Zapf Chancery;",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttributeType.FontSize:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return $" font-size: {Measurement.Convert(Value[0])}";
                case AttributeType.Margin:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return $" margin: {Measurement.Convert(Value[0])}";
                case AttributeType.Padding:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return $" padding: {Measurement.Convert(Value[0])}";
                case AttributeType.Width:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return $" width: {Measurement.Convert(Value[0])}";
                case AttributeType.Height:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    return $" height: {Measurement.Convert(Value[0])}";
                case AttributeType.Border:
                    if (Value.Length != 3)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    StringBuilder result = new StringBuilder();
                    result.Append($" border: {Measurement.Convert(Value[0])}");
                    switch (Value[1])
                    {
                        case Border.Dashed:
                            result.Append($" dashed");
                            break;
                        case Border.Dotted:
                            result.Append($" dotted");
                            break;
                        case Border.Double:
                            result.Append($" double");
                            break;
                        case Border.In3D:
                            result.Append($" inset");
                            break;
                        case Border.Normal:
                            result.Append($" solid");
                            break;
                        case Border.Out3D:
                            result.Append($" outset");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    return result.Append($" {Color.Convert(Value[2])}").ToString();
                case AttributeType.OverflowX:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    if ((bool)Value[0])
                    {
                        return " overflow-x: auto";
                    }
                    return " overflow-x: hidden";
                case AttributeType.OverflowY:
                    if (Value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    if ((bool)Value[0])
                    {
                        return " overflow-y: auto";
                    }
                    return " overflow-y: hidden";
                default:
                    throw new ImpartError("Invalid attribute parameters.");
            }
        }
    }
}