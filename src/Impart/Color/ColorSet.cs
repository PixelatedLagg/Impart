using System.Collections.Generic;

namespace Impart
{
    public class ColorSet
    {
        List<Color> colors;
        public ColorSet(params Color[] colors)
        {
            this.colors = new List<Color>();
            foreach (Color c in colors)
            {
                this.colors.Add(c);
            }
        }
        public Color Sort(ColorSort sort, int i)
        {
            switch (sort)
            {
                case ColorSort.Light:
                    break;
                case ColorSort.Dark:
                    break;
                case ColorSort.Red:
                    break;
                case ColorSort.Blue:
                    break;
                case ColorSort.Green:
                    break;
            }
            return null;
        }
        public Color this[int i]
        {
            get
            {
                return colors[i];
            }
        }
    }
    public enum ColorSort
    {
        Light = 0,
        Dark = 1,
        Red = 2,
        Blue = 3,
        Green = 4
    }
}