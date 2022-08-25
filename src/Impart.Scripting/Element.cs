namespace Impart.Scripting
{
    public static class Element
    {
        public static Edit GetMany(ElementType type, AttrType attrType, params object[] args)
        {
            return new Edit($@"Array.from(document.getElementsByTagName('{type switch
            {
                ElementType.RegularText => "p",
                ElementType.BoldText => "b",
                ElementType.DeleteText => "del",
                ElementType.EmphasizeText => "em",
                ElementType.ImportantText => "strong",
                ElementType.InsertText => "ins",
                ElementType.ItalicText => "i",
                ElementType.MarkText => "mark",
                ElementType.SmallText => "small",
                ElementType.SubscriptText => "sub",
                ElementType.SuperscriptText => "sup",
                ElementType.OrderedList => "ol",
                ElementType.UnorderedList => "ul",
                ElementType.Link => "a",
                ElementType.Image => "img",
                ElementType.Header1 => "h1",
                ElementType.Header2 => "h2",
                ElementType.Header3 => "h3",
                ElementType.Header4 => "h4",
                ElementType.Header5 => "h5",
                ElementType.EFrame => "iframe",
                ElementType.Division => "div",
                ElementType.Button => "button",
                ElementType.Video => "video",
                ElementType.TableRow => "tr",
                ElementType.Table => "table",
                ElementType.Form => "form",
                ElementType.FormInput => "input",
                _ => throw new ImpartError("Invalid type parameters.")
            }}')).forEach(e => {{e.{GetEdit(type, attrType, args)}}});");
        }

        public static Edit Get(ElementType type, AttrType attrType, params object[] args)
        {
            return new Edit($@"document.getElementsByTagName('{type switch
            {
                ElementType.RegularText => "p",
                ElementType.BoldText => "b",
                ElementType.DeleteText => "del",
                ElementType.EmphasizeText => "em",
                ElementType.ImportantText => "strong",
                ElementType.InsertText => "ins",
                ElementType.ItalicText => "i",
                ElementType.MarkText => "mark",
                ElementType.SmallText => "small",
                ElementType.SubscriptText => "sub",
                ElementType.SuperscriptText => "sup",
                ElementType.OrderedList => "ol",
                ElementType.UnorderedList => "ul",
                ElementType.Link => "a",
                ElementType.Image => "img",
                ElementType.Header1 => "h1",
                ElementType.Header2 => "h2",
                ElementType.Header3 => "h3",
                ElementType.Header4 => "h4",
                ElementType.Header5 => "h5",
                ElementType.EFrame => "iframe",
                ElementType.Division => "div",
                ElementType.Button => "button",
                ElementType.Video => "video",
                ElementType.TableRow => "tr",
                ElementType.Table => "table",
                ElementType.Form => "form",
                ElementType.FormInput => "input",
                _ => throw new ImpartError("Invalid type parameters.")
            }}')[0].{GetEdit(type, attrType, args)}");
        }

        private static string GetEdit(ElementType type, AttrType attrType, object[] args)
        {
            return $@"style.{attrType switch
            {
                AttrType.BackgroundColor => $@"backgroundColor = '{Color.Convert(args[0]) switch
                    {
                        Rgb rgb => rgb,
                        Hsl hsl => hsl,
                        Hex hex => hex,
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    }}';",
                AttrType.ForegroundColor => $@"foregroundColor = '{Color.Convert(args[0]) switch
                    {
                        Rgb rgb => rgb,
                        Hsl hsl => hsl,
                        Hex hex => hex,
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    }}';",
                AttrType.Alignment => args[0] switch
                    {
                        Alignment.Center => "margin = 'auto';",
                        Alignment.Justify => "justifyContent = 'center';",
                        Alignment.Left => "cssFloat = 'left';",
                        Alignment.Right => "cssFloat = 'right';",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    },
                AttrType.FontFamily => $@"fontFamily = '{args[0] switch
                    {
                        FontFamily.AndaleMono => "Andale Mono",
                        FontFamily.AppleChancery => "Apple Chancery",
                        FontFamily.Arial => "Arial",
                        FontFamily.AvantaGarde => "Avanta Garde",
                        FontFamily.Baskerville => "Baskerville",
                        FontFamily.BigCaslon => "Big Caslon",
                        FontFamily.BodoniMT => "Bodoni MT",
                        FontFamily.BookAntiqua => "Book Antiqua",
                        FontFamily.Bookman => "font-family Book Antiqua",
                        FontFamily.BradleyHand => " Bradley Hand",
                        FontFamily.BrushScriptMT => "Brush Script MT",
                        FontFamily.BrushScriptStd => "Brush Script Std",
                        FontFamily.Calibri => "Calibri",
                        FontFamily.CalistoMT => "Calisto MT",
                        FontFamily.Cambria => "Cambria",
                        FontFamily.Candara => "Candara",
                        FontFamily.CenturyGothic => "Century Gothic",
                        FontFamily.ComicSans => "Comic Sans",
                        FontFamily.ComicSansMS => "Comic Sans MS",
                        FontFamily.Consolas => "Consolas",
                        FontFamily.Coronetscript => "Coronet script",
                        FontFamily.Courier => "Courier",
                        FontFamily.CourierNew => "Courier New",
                        FontFamily.Didot => "Didot",
                        FontFamily.Florence => "Florence",
                        FontFamily.FranklinGothicMedium => "Franklin Gothic Medium",
                        FontFamily.Futara => "Futara",
                        FontFamily.Garamond => "Garamond",
                        FontFamily.Geneva => "Geneva",
                        FontFamily.Georgia => "Georgia",
                        FontFamily.GillSans => "Gill Sans",
                        FontFamily.GoudyOldStyle => "Goudy Old Style",
                        FontFamily.Helvetica => "Helvetica",
                        FontFamily.HoeflerText => "Hoefler Text",
                        FontFamily.LucidaBright => "Lucida Bright",
                        FontFamily.LucidaConsole => "Lucida Console",
                        FontFamily.LucidaSans => "Lucida Sans",
                        FontFamily.LucidaSansTypewriter => "Lucida Sans Typewriter",
                        FontFamily.Monaco => "Monaco",
                        FontFamily.NewCenturySchoolbook => "New Century Schoolbook",
                        FontFamily.Noto => "Noto",
                        FontFamily.Optima => "Optima",
                        FontFamily.Palatino => "Palatino",
                        FontFamily.Parkavenue => "Parkavenue",
                        FontFamily.Perpetua => "Perpetua",
                        FontFamily.Rockwell => "Rockwell",
                        FontFamily.RockwellExtraBold => "Rockwell Extra Bold",
                        FontFamily.SegoeUI => "Segoe UI",
                        FontFamily.SnellRoundhan => "Snell Roundhan",
                        FontFamily.TimesNewRoman => "Times New Roman",
                        FontFamily.TrebuchetMS => "Trebuchet MS",
                        FontFamily.URWChancery => "URW Chancery",
                        FontFamily.Verdana => "Verdana",
                        FontFamily.ZapfChancery => "Zapf Chancery",
                        _ => throw new ImpartError("Invalid attribute parameters.")
                    }}';",
                _ => ""
            }}";
        }
    }
}