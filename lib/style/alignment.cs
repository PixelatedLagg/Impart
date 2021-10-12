namespace Csweb
{
    public static class Alignment
    {
        public const string Left = "left";
        public const string Right = "right";
        public const string Center = "center";
        public const string Justify = "justify";
        internal static bool Any(string alignment)
        {
            switch (alignment)
            {
                case "left":
                case "right":
                case "center":
                case "justify":
                    return true;
                default:
                    return false;
            }
        }
    }
}