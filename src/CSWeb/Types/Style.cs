using System;
using System.Drawing;

namespace CSWeb
{
    public class style
    {
        private string textCache;
        private string id;
        private int colorCheck;
        public style(Style style, string id)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(id))
            {
                throw new StyleError("ID/Class/Element cannot be null!", this);
            }
            switch (style)
            {
                case Style.IDStyle:
                    textCache = $"#{id} {{%^";
                    break;
                case Style.EStyle:
                    textCache = $"{id} {{%^";
                    break;
                case Style.ClassStyle:
                    textCache = $".{id} {{%^";
                    break;
            }
            this.id = id;
            colorCheck = 0;
            Debug.CallObjectEvent("[style] created idstyle");
        }
        public style AddColor(Color color)
        {
            Timer.StartTimer();
            colorCheck++;
            textCache += $"    color: {color.ToKnownColor()};%^";
            Debug.CallObjectEvent("[style] added color");
            return this;
        }
        public style AddColor(string hex)
        {
            Timer.StartTimer();
            colorCheck++;
            if (hex.Length != 6)
            {
                throw new StyleError("Invalid hex value!", this);
            }
            textCache += $"    color: #{hex};%^";
            Debug.CallObjectEvent("[style] added color");
            return this;
        }
        public style AddColor(int x, int y, int z)
        {
            Timer.StartTimer();
            colorCheck++;
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new StyleError("Invalid RGB value!", this);
            }
            textCache += $"    color: rgb({x}, {y}, {z});%^";
            Debug.CallObjectEvent("[style] added color");
            return this;
        }
        public style AddAlign(string alignment)
        {
            Timer.StartTimer();
            if (!Alignment.Any(alignment))
            {
                throw new StyleError("Invalid alignment value!", this);
            }
            textCache += $"    text-align: {alignment};%^";
            Debug.CallObjectEvent("[style] added alignment");
            return this;
        }
        public style SetMargin(int pixels)
        {
            if (pixels < 0)
            {
                throw new StyleError("Invalid margin value!", this);
            }
            textCache += $"    margin: {pixels}px;%^";
            Debug.CallObjectEvent("[style] added margin");
            return this;
        }
        internal string Render()
        {
            if (colorCheck > 1)
            {
                throw new StyleError("Cannot assign multiple color instances!", this);
            }
            return $"{textCache}}}".Replace("%^", Environment.NewLine);
        }
    }
}