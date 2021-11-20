using System.Linq;
using System.Collections.Generic;
using System;

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
                    var tempHslsLight = new List<(float, Hsl)>();
                    foreach (Hsl hsl in ToHsls(colors))
                    {
                        tempHslsLight.Add((hsl.hsl.l, hsl));
                    }
                    return (Color)(tempHslsLight.OrderBy(x => x.Item1).ToList()[i].Item2);
                case ColorSort.Dark:
                    var tempHslsDark = new List<(float, Hsl)>();
                    foreach (Hsl hsl in ToHsls(colors))
                    {
                        tempHslsDark.Add((hsl.hsl.l, hsl));
                    }
                    return (Color)(tempHslsDark.OrderByDescending(x => x.Item1).ToList()[i].Item2);
                case ColorSort.Red:
                    break;
                case ColorSort.Blue:
                    break;
                case ColorSort.Green:
                    break;
            }
            return null;
        }
        private List<Rgb> ToRgbs(List<Color> colors)
        {
            List<Rgb> rgbs = new List<Rgb>();
            foreach (Color c in colors)
            {
                switch (c.GetType().FullName)
                {
                    case "Impart.Rgb":
                        rgbs.Add((Rgb)c);
                        break;
                    case "Impart.Hsl":
                    rgbs.Add(((Hsl)c).ToRgb());
                    break;
                    case "Impart.Hex":
                        rgbs.Add(((Hex)c).ToRgb());
                        break;
                }
            }
            return rgbs;
        }
        private List<Hsl> ToHsls(List<Color> colors)
        {
            List<Hsl> hsls = new List<Hsl>();
            foreach (Color c in colors)
            {
                switch (c.GetType().FullName)
                {
                    case "Impart.Rgb":
                        hsls.Add(((Rgb)c).ToHsl());
                        break;
                    case "Impart.Hsl":
                        hsls.Add((Hsl)c);
                        break;
                    case "Impart.Hex":
                        hsls.Add(((Hex)c).ToRgb().ToHsl());
                        break;
                }
            }
            return hsls;
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