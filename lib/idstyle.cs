using System;
using System.Drawing;

namespace Csweb
{
    public class idstyle
    {
        private string textCache;
        private string id;
        private bool hasColor;
        public idstyle(string id)
        {
            this.id = id;
            hasColor = false;
            textCache = $"#{id} {{%^";
        }
        public void AddColor(Color color)
        {
            CheckColor();
            textCache = $"{textCache}{CheckLB()}    color: {color.ToKnownColor()};";
            hasColor = true;
        }
        public void AddHexColor(string hex)
        {
            CheckColor();
            if (hex.Length != 6)
            {
                throw new ArgumentException("Invalid hex value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: #{hex};";
        }
        public void AddRGBColor(int x, int y, int z)
        {
            CheckColor();
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new ArgumentException("Invalid RGB value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: rgb({x},{y},{z});";
        }
        public void AddAlign(string alignment)
        {
            if (!Alignment.Any(alignment))
            {
                throw new ArgumentException("Invalid alignment value!");
            }
            textCache = $"{textCache}{CheckLB()}    text-align: {alignment};";
        }
        private string CheckLB()
        {
            if (textCache == $"#{id} {{%^")
            {
                return "";
            }
            return "%^";
        }
        private void CheckColor()
        {
            if (hasColor)
            {
                throw new ArgumentException("Style already has color!");
            }
        }
        internal string Render()
        {
            textCache = textCache.Replace("%^", Environment.NewLine);
            return $"{textCache}{Environment.NewLine}}}";
        }
    }
}