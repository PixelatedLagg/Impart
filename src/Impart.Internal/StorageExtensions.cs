using System;

namespace Impart.Internal
{
    public static class StorageExtensions
    {
        /*public static Attr GetAttr(string attrType, string attrValue)
        {
            string[] args = attrValue.Split(' ');
            switch (attrType)
            {
                case "background-color":
                    return new Attr(AttrType.BackgroundColor, GetColor(args[0]));
            }
        }
        public static ExtAttr GetExtAttr(string extAttrType, string extAttrValue)
        {
        }*/
        public static Color GetColor(string colorValue)
        {
            switch (colorValue[0])
            {
                case '#':
                    return new Hex(colorValue.Remove(0, 1));
                case 'r':
                    string[] rbgValues = colorValue.Remove(0, 4).Remove(colorValue.Length - 5, 1).Split(',');
                    return new Rgb(Int32.Parse(rbgValues[0]), Int32.Parse(rbgValues[1]), Int32.Parse(rbgValues[2]));
                default:
                    string[] hslValues = colorValue.Remove(0, 4).Remove(colorValue.Length - 5, 1).Split(',');
                    return new Hsl(Int32.Parse(hslValues[0]), Int32.Parse(hslValues[1].Remove(hslValues[1].Length - 1, 1)), Int32.Parse(hslValues[2].Remove(hslValues[2].Length - 1, 1)));
            }
        }
        public static Length GetLength(string lengthValue)
        {
            switch (lengthValue[lengthValue.Length - 1])
            {
                case '%':
                    return new Percent(Int32.Parse(lengthValue.Remove(lengthValue.Length - 1, 1)));
                case 'x':
                    return new Pixels(Int32.Parse(lengthValue.Remove(lengthValue.Length - 2, 2)));
                case 'h':
                    return new ViewHeight(Int32.Parse(lengthValue.Remove(lengthValue.Length - 2, 2)));
                default:
                    return new ViewWidth(Int32.Parse(lengthValue.Remove(lengthValue.Length - 2, 2)));
            }
        }
    }
}