using System;
namespace Impart
{
    public class ImpartCommon
    {
        public ImpartCommon()
        {
            if (ImpartConfig.CommonInitialization == 0)
            {
                ImpartConfig.Initialize();
            }
            else
            {
                throw new ConfigError("Cannot initialize ImpartCommon more than once!");
            }
        }
        public static Text Text(string text, string id = null)
        {
            return new Text(text, id);
        }
        public static Image Image(string path, string id = null)
        {
            return new Image(path, id);
        }
        public static Header Header(int num, string text, string id = null)
        {
            return new Header(num, text, id);
        }
        public static Link Link(Text text, string path, string id = null)
        {
            return new Link(text, path, id);
        }
        public static Link Link(Image image, string path, string id = null)
        {
            return new Link(image, path, id);
        }
        public static Style Style(StyleType style, string id)
        {
            return new Style(style, id);
        }
        public static Division Division(IDType? type = null, string id = null)
        {
            return new Division(type, id);
        }
        public static Rgb Rgb(int r, int g, int b)
        {
            return new Rgb(r, g, b);
        }
        public static Hsl Hsl(float h, float s, float l)
        {
            return new Hsl(h, s, l);
        }
        public static Hex Hex(string hex)
        {
            return new Hex(hex);
        }
        public static List List(int type, string id = null)
        {
            return new List(type, id);
        }
        public static Scrollbar Scrollbar(Axis axis, string id, IDType type, int width, Color color, Color colorThumb, int? rounded = null)
        {
            return new Scrollbar(axis, id, type, width, color, colorThumb, rounded);
        }
        public enum ListTypes
        {
            Unordered = 0,
            Ordered = 1
        }
        public enum StyleType
        {
            IDStyle = 0,
            EStyle = 1,
            ClassStyle = 2
        }
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