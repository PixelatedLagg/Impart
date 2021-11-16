using System;
using System.Reflection;
using System.Diagnostics;

namespace Impart
{
    public class ImpartCommon
    {
        internal static void Config()
        {
            string fullName = "";
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    if (Type.GetType(method.Name)?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance) != null)
                    {
                        Debug.ObjectEvent += (Action<Log>)Delegate.CreateDelegate(typeof(Action<Log>), null, Type.GetType(method.Name).GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance));
                    }
                    ImpartConfig.Initialize();
                    return;
                }
                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));
            if (Type.GetType(fullName)?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance) != null)
            {
                Debug.ObjectEvent += (Action<Log>)Delegate.CreateDelegate(typeof(Action<Log>), null, Type.GetType(fullName)?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance));
            }
            ImpartConfig.Initialize();
        }
        public static Text text(string text, string id = null)
        {
            return new Text(text, id);
        }
        public static Image image(string path, string id = null)
        {
            return new Image(path, id);
        }
        public static Header header(int num, string text, string id = null)
        {
            return new Header(num, text, id);
        }
        public static Link link(Text text, string path, string id = null)
        {
            return new Link(text, path, id);
        }
        public static Link link(Image image, string path, string id = null)
        {
            return new Link(image, path, id);
        }
        public static Style style(StyleType style, string id)
        {
            return new Style(style, id);
        }
        public static Division division(IDType? type = null, string id = null)
        {
            return new Division(type, id);
        }
        public static Rgb rgb(int r, int g, int b)
        {
            return new Rgb(r, g, b);
        }
        public static Hsl hsl(float h, float s, float l)
        {
            return new Hsl(h, s, l);
        }
        public static Hex hex(string hex)
        {
            return new Hex(hex);
        }
        public static List list(int type, string id = null)
        {
            return new List(type, id);
        }
        public static Scrollbar scrollbar(Axis axis, Measurement width, Color color, Color colorThumb, Division division = null, Measurement rounded = null)
        {
            return new Scrollbar(axis, width, color, colorThumb, division, rounded);
        }
        public static Form form()
        {
            return new Form();
        }
        public static TextField textField(string text, string inputid)
        {
            return new TextField(text, inputid);
        }
        public static TextField textField(Text text, string inputid, string id = null)
        {
            return new TextField(text, inputid, id);
        }
        public static CheckField checkField(string text, string inputid)
        {
            return new CheckField(text, inputid);
        }
        public static CheckField checkField(Text text, string inputid, string id = null)
        {
            return new CheckField(text, inputid, id);
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