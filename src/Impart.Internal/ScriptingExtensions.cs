using Impart.Scripting;

namespace Impart.Internal
{
    public static class ScriptingExtensions
    {
        public static Edit Get(Text text, AttrType attrType, params object[] args)
        {
            return new Edit($@"document.getElementsByClassName('{text._IOID}')[0].style.{attrType switch
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
                AttrType.Alignment => "",
                _ => ""
            }}");
        }
    }
}