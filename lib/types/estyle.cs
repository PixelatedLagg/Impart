using System;
using System.Drawing;

namespace Csweb
{
    public class estyle
    {
        private string textCache;
        private string element;
        private bool hasColor;
        public estyle(string element)
        {
            Timer.StartTimer();
            if (!Elements.Any(element))
            {
                throw new EStyleError("Invalid element value!");
            }
            this.element = element;
            textCache = $"{element} {{%^";
            hasColor = false;
            Debug.CallObjectEvent("[estyle] created estyle");
        }
        public void AddColor(Color color)
        {
            Timer.StartTimer();
            CheckColor();
            textCache = $"{textCache}{CheckLB()}    color: {color.ToKnownColor()};";
            hasColor = true;
            Debug.CallObjectEvent("[estyle] added color (normal)");
        }
        public void AddHexColor(string hex)
        {
            Timer.StartTimer();
            CheckColor();
            if (hex.Length != 6)
            {
                throw new EStyleError("Invalid hex value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: #{hex};";
            Debug.CallObjectEvent("[estyle] added color (hex)");
        }
        public void AddRGBColor(int x, int y, int z)
        {
            Timer.StartTimer();
            CheckColor();
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new EStyleError("Invalid RGB value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: rgb({x},{y},{z});";
            Debug.CallObjectEvent("[estyle] added color (rgb)");
        }
        public void AddAlign(string alignment)
        {
            Timer.StartTimer();
            if (!Alignment.Any(alignment))
            {
                throw new EStyleError("Invalid alignment value!");
            }
            textCache = $"{textCache}{CheckLB()}    text-align: {alignment};";
            Debug.CallObjectEvent("[estyle] added alignment");
        }
        private string CheckLB()
        {
            if (textCache == $"{element} {{%^")
            {
                return "";
            }
            return "%^";
        }
        private void CheckColor()
        {
            if (hasColor)
            {
                throw new EStyleError("Style already has color!");
            }
        }
        internal string Render()
        {
            textCache = textCache.Replace("%^", Environment.NewLine);
            return $"{textCache}{Environment.NewLine}}}";
        }
    }
}