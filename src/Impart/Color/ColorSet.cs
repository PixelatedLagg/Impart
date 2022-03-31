using System.Linq;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>The class for a set of Colors.</summary>
    public sealed class ColorSet
    {
        private List<Color> colors;

        /// <summary>Creates a ColorSet instance with Color[] as the entries.</summary>
        /// <returns>A ColorSet instance.</returns>
        /// <param name="colors">The Colors to add.</param>
        public ColorSet(params Color[] colors)
        {
            this.colors = new List<Color>();
            foreach (Color c in colors)
            {
                if (c == null)
                {
                    throw new ImpartError("Color cannot be null!");
                }
                this.colors.Add(c);
            }
        }

        /// <summary>Temporarily sorts the ColorSet and returns the value at the index of <paramref name="i"/>.</summary>
        /// <returns>A Color instance.</returns>
        /// <param name="sort">The ColorSort sorting pattern.</param>
        /// <param name="i">The index value to return.</param>
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
                    var tempRgbsRed = new List<(int, Rgb)>();
                    foreach (Rgb rgb in ToRgbs(colors))
                    {
                        tempRgbsRed.Add((rgb.rgb.r, rgb));
                    }
                    return (Color)(tempRgbsRed.OrderByDescending(x => x.Item1).ToList()[i].Item2);
                case ColorSort.Green:
                    var tempRgbsGreen = new List<(int, Rgb)>();
                    foreach (Rgb rgb in ToRgbs(colors))
                    {
                        tempRgbsGreen.Add((rgb.rgb.g, rgb));
                    }
                    return (Color)(tempRgbsGreen.OrderByDescending(x => x.Item1).ToList()[i].Item2);
                case ColorSort.Blue:
                    var tempRgbsBlue = new List<(int, Rgb)>();
                    foreach (Rgb rgb in ToRgbs(colors))
                    {
                        tempRgbsBlue.Add((rgb.rgb.b, rgb));
                    }
                    return (Color)(tempRgbsBlue.OrderByDescending(x => x.Item1).ToList()[i].Item2);
            }
            return null;
        }
        /// <summary>Return the value at the index of <paramref name="i"/>.</summary>
        /// <returns>A Color instance.</returns>
        /// <param name="i">The index value to return.</param>
        public Color this[int i]
        {
            get
            {
                return colors[i];
            }
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
    }
}