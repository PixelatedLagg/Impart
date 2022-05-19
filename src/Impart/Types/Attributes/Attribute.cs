using System.Text;

namespace Impart
{
    /// <summary>An element attribute.</summary>
    public struct Attribute
    {
        private AttrType _Type;

        /// <Value>The Attribute Type.</Value>
        public AttrType Type
        {
            get
            {
                return _Type;
            }
        }
        private object[] _Value;

        /// <Value>The Attribute Value(s).</Value>
        public object[] Value
        {
            get
            {
                return _Value;
            }
        }

        /// <summary>Creates an Attribute instance.</summary>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The value array.</param>
        public Attribute(AttrType type, params object[] value)
        {
            _Type = type;
            _Value = value;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            switch (Type)
            {
                case AttrType.BackgroundColor:
                    return Color.Convert(Value[0]) switch
                    {
                        Rgb rgb => $"background-color: {rgb};",
                        Hsl hsl => $"background-color: {hsl};",
                        Hex hex => $"background-color: {hex};",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttrType.ForegroundColor:
                    return Color.Convert(Value[0]) switch
                    {
                        Rgb rgb => $"color: {rgb};",
                        Hsl hsl => $"color: {hsl};",
                        Hex hex => $"color: {hex};",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttrType.Alignment:
                    return Value[0] switch
                    {
                        Alignment.Center => "align: center;",
                        Alignment.Justify => "align: justify;",
                        Alignment.Left => "align: left;",
                        Alignment.Right => "align: right;",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttrType.FontFamily:
                    return Value[0] switch
                    {
                        FontFamily.AndaleMono => "font-family: Andale Mono;",
                        FontFamily.AppleChancery => "font-family: Apple Chancery;",
                        FontFamily.Arial => "font-family: Arial;",
                        FontFamily.AvantaGarde => "font-family: Avanta Garde;",
                        FontFamily.Baskerville => "font-family: Baskerville;",
                        FontFamily.BigCaslon => "font-family: Big Caslon;",
                        FontFamily.BodoniMT => "font-family: Bodoni MT;",
                        FontFamily.BookAntiqua => "font-family: Book Antiqua;",
                        FontFamily.Bookman => "font-family Book Antiqua;",
                        FontFamily.BradleyHand => " font-family: Bradley Hand;",
                        FontFamily.BrushScriptMT => "font-family: Brush Script MT;",
                        FontFamily.BrushScriptStd => "font-family: Brush Script Std;",
                        FontFamily.Calibri => "font-family: Calibri;",
                        FontFamily.CalistoMT => "font-family: Calisto MT;",
                        FontFamily.Cambria => "font-family: Cambria;",
                        FontFamily.Candara => "font-family: Candara;",
                        FontFamily.CenturyGothic => "font-family: Century Gothic;",
                        FontFamily.ComicSans => "font-family: Comic Sans;",
                        FontFamily.ComicSansMS => "font-family: Comic Sans MS;",
                        FontFamily.Consolas => "font-family: Consolas;",
                        FontFamily.Coronetscript => "font-family: Coronet script;",
                        FontFamily.Courier => "font-family: Courier;",
                        FontFamily.CourierNew => "font-family: Courier New;",
                        FontFamily.Didot => "font-family: Didot;",
                        FontFamily.Florence => "font-family: Florence;",
                        FontFamily.FranklinGothicMedium => "font-family: Franklin Gothic Medium;",
                        FontFamily.Futara => "font-family: Futara;",
                        FontFamily.Garamond => "font-family: Garamond;",
                        FontFamily.Geneva => "font-family: Geneva;",
                        FontFamily.Georgia => "font-family: Georgia;",
                        FontFamily.GillSans => "font-family: Gill Sans;",
                        FontFamily.GoudyOldStyle => "font-family: Goudy Old Style;",
                        FontFamily.Helvetica => "font-family: Helvetica;",
                        FontFamily.HoeflerText => "font-family: Hoefler Text;",
                        FontFamily.LucidaBright => "font-family: Lucida Bright;",
                        FontFamily.LucidaConsole => "font-family: Lucida Console;",
                        FontFamily.LucidaSans => "font-family: Lucida Sans;",
                        FontFamily.LucidaSansTypewriter => "font-family: Lucida Sans Typewriter;",
                        FontFamily.Monaco => "font-family: Monaco;",
                        FontFamily.NewCenturySchoolbook => "font-family: New Century Schoolbook;",
                        FontFamily.Noto => "font-family: Noto;",
                        FontFamily.Optima => "font-family: Optima;",
                        FontFamily.Palatino => "font-family: Palatino;",
                        FontFamily.Parkavenue => "font-family: Parkavenue;",
                        FontFamily.Perpetua => "font-family: Perpetua;",
                        FontFamily.Rockwell => "font-family: Rockwell;",
                        FontFamily.RockwellExtraBold => "font-family: Rockwell Extra Bold;",
                        FontFamily.SegoeUI => "font-family: Segoe UI;",
                        FontFamily.SnellRoundhan => "font-family: Snell Roundhan;",
                        FontFamily.TimesNewRoman => "font-family: Times New Roman;",
                        FontFamily.TrebuchetMS => "font-family: Trebuchet MS;",
                        FontFamily.URWChancery => "font-family: URW Chancery;",
                        FontFamily.Verdana => "font-family: Verdana;",
                        FontFamily.ZapfChancery => "font-family: Zapf Chancery;",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttrType.FontSize:
                    return $"font-size: {Length.Convert(Value[0])};";
                case AttrType.Margin:
                    return $"margin: {Length.Convert(Value[0])};";
                case AttrType.Padding:
                    return $"padding: {Length.Convert(Value[0])};";
                case AttrType.Width:
                    return $"width: {Length.Convert(Value[0])};";
                case AttrType.Height:
                    return $"height: {Length.Convert(Value[0])};";
                case AttrType.Border:
                    StringBuilder result = new StringBuilder();
                    result.Append($"border: {Length.Convert(Value[0])}");
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
                    return result.Append($" {Color.Convert(Value[2])};").ToString();
                case AttrType.OverflowX:
                    if ((bool)Value[0])
                    {
                        return " overflow-x: auto;";
                    }
                    return " overflow-x: hidden;";
                case AttrType.OverflowY:
                    if ((bool)Value[0])
                    {
                        return " overflow-y: auto;";
                    }
                    return " overflow-y: hidden;";
                case AttrType.AlignText:
                    return Value[0] switch
                    {
                        Alignment.Center => " text-align: center;",
                        Alignment.Justify => " text-align: justify;",
                        Alignment.Left => " text-align: left;",
                        Alignment.Right => " text-align: right;",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                case AttrType.Animation:
                    return (Value[0] as AnimationArgs ?? throw new ImpartError("Invalid attribute parameters.")).ToString();
                case AttrType.Background:
                    BackgroundArgs args = (BackgroundArgs)Value[0];
                    return args.Background switch
                    {
                        Background.Loop => $"background-image: url('{args.Image}');",
                        Background.Single => $"background-repeat: no-repeat;background-image: url('{args.Image}');",
                        Background.Stretch => $"background-repeat: no-repeat;background-attachment: fixed;background-size: cover;background-image: url('{args.Image}');",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    };
                default:
                    throw new ImpartError("Invalid attribute parameters.");
            }
        }
    }
}