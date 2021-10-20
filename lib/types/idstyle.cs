using System;
using System.Drawing;

namespace Csweb
{
    public class idstyle
    {
        private string textCache;
        private string id;
        private int colorCheck;
        public idstyle(string id)
        {
            Timer.StartTimer();
            this.id = id;
            textCache = $"#{id} {{%^";
            colorCheck = 0;
            Debug.CallObjectEvent("[idstyle] created idstyle");
        }
        public void AddColor(Color color)
        {
            Timer.StartTimer();
            colorCheck++;
            textCache = $"{textCache}{CheckLB()}    color: {color.ToKnownColor()};";
            Debug.CallObjectEvent("[idstyle] added color (normal)");
        }
        public void AddHexColor(string hex)
        {
            Timer.StartTimer();
            colorCheck++;
            if (hex.Length != 6)
            {
                throw new IDStyleError("Invalid hex value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: #{hex};";
            Debug.CallObjectEvent("[idstyle] added color (hex)");
        }
        public void AddRGBColor(int x, int y, int z)
        {
            Timer.StartTimer();
            colorCheck++;
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new IDStyleError("Invalid RGB value!");
            }
            textCache = $"{textCache}{CheckLB()}    color: rgb({x},{y},{z});";
            Debug.CallObjectEvent("[idstyle] added color (rgb)");
        }
        public void AddAlign(string alignment)
        {
            Timer.StartTimer();
            if (!Alignment.Any(alignment))
            {
                throw new IDStyleError("Invalid alignment value!");
            }
            textCache = $"{textCache}{CheckLB()}    text-align: {alignment};";
            Debug.CallObjectEvent("[idstyle] added alignment");
        }
        private string CheckLB()
        {
            if (textCache == $"#{id} {{%^")
            {
                return "";
            }
            return "%^";
        }
        internal string Render()
        {
            if (colorCheck > 1)
            {
                throw new IDStyleError("Cannot assign multiple color instances!");
            }
            textCache = textCache.Replace("%^", Environment.NewLine);
            return $"{textCache}{Environment.NewLine}}}";
        }
    }
}