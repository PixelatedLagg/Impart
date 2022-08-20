namespace Impart.Scripting
{
    public static class TextExtensions
    {
        public static void Set(this Text text, EventType eventType, IFunction function)
        {
            text._Events.Events.Add(new Event(eventType, function));
        }
        public static Edit Get(this Text text, AttrType attrType, params object[] args)
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
                _ => ""
            }}");
        }
    }
}