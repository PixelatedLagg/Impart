using System;

namespace Impart.Internal
{
    public static class StorageExtensions
    {
        public static Attr GetAttr(string attrType, string attrValue)
        {
            string[] args = attrValue.Split(' ');
            switch (attrType)
            {
                case "background-color":
                    return new Attr(AttrType.BackgroundColor, GetColor(args[0]));
                case "color":
                    return new Attr(AttrType.ForegroundColor, GetColor(args[0]));
                case "margin":
                    if (args[0] == "auto")
                    {
                        return new Attr(AttrType.Alignment, Alignment.Center);
                    }
                    return new Attr(AttrType.Margin, GetLength(args[0]));
                case "font-family":
                    FontFamily font = GetFontFamily(args[0]);
                    if (font == FontFamily.Custom)
                    {
                        return new Attr(AttrType.CustomFont, args[0]);
                    }
                    return new Attr(AttrType.FontFamily, font);
                case "font-size":
                    return new Attr(AttrType.FontSize, GetLength(args[0]));
                case "padding":
                    return new Attr(AttrType.Padding, GetLength(args[0]));
                case "width":
                    return new Attr(AttrType.Width, GetLength(args[0]));
                case "height":
                    return new Attr(AttrType.Height, GetLength(args[0]));
                case "border":
                    return new Attr(AttrType.Border, GetLength(args[0]), args[1] switch {
                        "dashed" => BorderType.Dashed,
                        "dotted" => BorderType.Dotted,
                        "double" => BorderType.Double,
                        "inset" => BorderType.In3D,
                        "solid" => BorderType.Normal,
                        _ => BorderType.Out3D
                    }, GetColor(args[2]));
                case "overflow-x":
                    return new Attr(AttrType.OverflowX, true);
                case "overflow-y":
                    return new Attr(AttrType.OverflowY, true);
                case "text-align":
                    return new Attr(AttrType.AlignText, args[0] switch {
                        "center" => Alignment.Center,
                        "justify" => Alignment.Justify,
                        "left" => Alignment.Left,
                        _ => Alignment.Right
                    });
                case "justify-content":
                    return new Attr(AttrType.Alignment, Alignment.Justify);
                default: //"float"
                    if (args[0] == "left")
                    {
                        return new Attr(AttrType.Alignment, Alignment.Left);
                    }
                    else
                    {
                        return new Attr(AttrType.Alignment, Alignment.Right);
                    }
            }

        }
        public static ExtAttr GetExtAttr(string extAttrType, string extAttrValue)
        {
            return extAttrType switch
            {
                "id" => new ExtAttr(ExtAttrType.ID, extAttrValue),
                _ /* "title" */ => new ExtAttr(ExtAttrType.HoverMessage, extAttrValue)
            };
        }
        public static Color GetColor(string colorValue)
        {
            switch (colorValue[0])
            {
                case '#':
                    return new Hex(colorValue.Remove(0, 1));
                case 'r':
                    string[] rbgValues = colorValue.Remove(0, 4).Remove(colorValue.Length - 5, 1).Split(',');
                    return new Rgb(Int32.Parse(rbgValues[0]), Int32.Parse(rbgValues[1]), Int32.Parse(rbgValues[2]));
                default:
                    string[] hslValues = colorValue.Remove(0, 4).Remove(colorValue.Length - 5, 1).Split(',');
                    return new Hsl(Int32.Parse(hslValues[0]), Int32.Parse(hslValues[1].Remove(hslValues[1].Length - 1, 1)), Int32.Parse(hslValues[2].Remove(hslValues[2].Length - 1, 1)));
            }
        }
        public static Length GetLength(string lengthValue)
        {
            Console.WriteLine(Int32.Parse(lengthValue.Remove(lengthValue.Length - 2, 2)));
            switch (lengthValue[lengthValue.Length - 1])
            {
                case '%':
                    return new Percent(float.Parse(lengthValue.Remove(lengthValue.Length - 1, 1)));
                case 'x':
                    return new Pixels(Int32.Parse(lengthValue.Remove(lengthValue.Length - 2, 2)));
                case 'h':
                    return new ViewHeight(Int32.Parse(lengthValue.Remove(lengthValue.Length - 2, 2)));
                default:
                    return new ViewWidth(Int32.Parse(lengthValue.Remove(lengthValue.Length - 2, 2)));
            }
        }
        public static FontFamily GetFontFamily(string fontFamilyValue)
        {
            return fontFamilyValue switch
            {
                "Andale Mono" => FontFamily.AndaleMono,
                "Apple Chancery" => FontFamily.AppleChancery,
                "Arial" => FontFamily.Arial,
                "Avanta Garde" => FontFamily.AvantaGarde,
                "Baskerville" => FontFamily.Baskerville,
                "Big Caslon" => FontFamily.BigCaslon,
                "Bodoni MT" => FontFamily.BodoniMT,
                "Book Antiqua" => FontFamily.BookAntiqua,
                "Bookman" => FontFamily.Bookman,
                "Bradley Hand" => FontFamily.BradleyHand,
                "Brush Script MT" => FontFamily.BrushScriptMT,
                "Brush Script Std" => FontFamily.BrushScriptStd,
                "Calibri" => FontFamily.Calibri,
                "Calisto MT" => FontFamily.CalistoMT,
                "Cambria" => FontFamily.Cambria,
                "Candara" => FontFamily.Candara,
                "Century Gothic" => FontFamily.CenturyGothic,
                "Comic Sans" => FontFamily.ComicSans,
                "Comic Sans MS" => FontFamily.ComicSansMS,
                "Consolas" => FontFamily.Consolas,
                "Coronet script" => FontFamily.Coronetscript,
                "Courier" => FontFamily.Courier,
                "Courier New" => FontFamily.CourierNew,
                "Didot" => FontFamily.Didot,
                "Florence" => FontFamily.Florence,
                "Franklin Gothic Medium" => FontFamily.FranklinGothicMedium,
                "Futara" => FontFamily.Futara,
                "Garamond" => FontFamily.Garamond,
                "Geneva" => FontFamily.Geneva,
                "Georgia" => FontFamily.Georgia,
                "Gill Sans" => FontFamily.GillSans,
                "Goudy Old Style" => FontFamily.GoudyOldStyle,
                "Helvetica" => FontFamily.Helvetica,
                "Hoefler Text" => FontFamily.HoeflerText,
                "Lucida Bright" => FontFamily.LucidaBright,
                "Lucida Console" => FontFamily.LucidaConsole,
                "Lucida Sans" => FontFamily.LucidaSans,
                "Lucida Sans Typewriter" => FontFamily.LucidaSansTypewriter,
                "Monaco" => FontFamily.Monaco,
                "New Century Schoolbook" => FontFamily.NewCenturySchoolbook,
                "Noto" => FontFamily.Noto,
                "Optima" => FontFamily.Optima,
                "Palatino" => FontFamily.Palatino,
                "Parkavenue" => FontFamily.Parkavenue,
                "Perpetua" => FontFamily.Perpetua,
                "Rockwell" => FontFamily.Rockwell,
                "Rockwell Extra Bold" => FontFamily.RockwellExtraBold,
                "Segoe UI" => FontFamily.SegoeUI,
                "Snell Roundhan" => FontFamily.SnellRoundhan,
                "Times New Roman" => FontFamily.TimesNewRoman,
                "Trebuchet MS" => FontFamily.TrebuchetMS,
                "URW Chancery" => FontFamily.URWChancery,
                "Verdana" => FontFamily.Verdana,
                "Zapf Chancery" => FontFamily.ZapfChancery,
                _ => FontFamily.Custom
            };
        }
    }
}