namespace Csweb
{
    public static class Elements
    {
        public const string Text = "p";
        public const string Image = "img";
        public const string Header1 = "h1";
        public const string Header2 = "h2";
        public const string Header3 = "h3";
        public const string Header4 = "h4";
        public const string Header5 = "h5";
        internal static bool Any(string element)
        {
            switch (element)
            {
                case "p":
                case "img":
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "h5":
                    return true;
                default:
                    return false;
            }
        }
    }
}