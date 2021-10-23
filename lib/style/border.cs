namespace Csweb
{
    public static class Border
    {
        public const string Normal = "solid";
        public const string Dashed = "dashed";
        public const string Dotted = "dotted";
        public const string Double = "double";
        public const string In3D = "inset";
        public const string Out3D = "outset";
        internal static bool Any(string border)
        {
            switch (border)
            {
                case "solid":
                case "dashed":
                case "dotted":
                case "double":
                case "inset":
                case "outset":
                    return true;
                default:
                    return false;
            }
        }
    }
}