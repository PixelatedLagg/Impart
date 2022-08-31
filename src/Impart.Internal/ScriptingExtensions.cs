using Impart.Scripting;

namespace Impart.Internal
{
    /// <summary>Common methods used by multiple extensions in the Impart.Scripting namespace.</summary>
    public static class ScriptingExtensions
    {
        /// <summary>Get an event string finding elements by internally generated class.</summary>
        /// <param name="attrType">The AttrType that is changed in the event.</param>
        /// <param name="args">The Attr value(s) to assign to the AttrType in the event.</param>
        public static string GetEdit(AttrType attrType, object[] args)
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

        /// <summary>Get the event name from the EventType.</summary>
        /// <param name="eventType">The EventType to get the name from.</param>
        public static string GetEventName(EventType eventType)
        {
            return (eventType) switch 
            {
                EventType.Click => "click",
                EventType.Hover => "mouseover",
                EventType.DoubleClick => "dblclick",
                _ => ""
            };
        }
    }
}