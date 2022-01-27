namespace Impart
{
    public class ImpartCommon
    {
        public enum IDType
        {
            ID = 1,
            Class = 0
        }
        public enum Axis
        {
            X = 0,
            Y = 1
        }
    }
    public interface Element {}
    public interface Color {}
    public enum Border
    {
        Normal,
        Dashed,
        Dotted,
        Double,
        In3D,
        Out3D
    }
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