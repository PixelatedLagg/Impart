using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using s = System;

namespace Csweb
{
    static public class ConvertColor
    {
        static public (int, int, int) ToRgb(int x, int y, int z)
        {
            return (0, 0, 0);
        }
        public static Rgb HslToRgb(Hsl hsl)
        {
            float r = 0;
            float g = 0;
            float b = 0;
            if (hsl.hsl.s == 0)
            {
                r = g = b = hsl.hsl.l * 255;
            }
            else
            {
                float v1, v2;
                float hue = hsl.hsl.h / 360;
                v2 = (hsl.hsl.l < 0.5) ? (hsl.hsl.l * (1 + hsl.hsl.s)) : ((hsl.hsl.l + hsl.hsl.s) - (hsl.hsl.l * hsl.hsl.s));
                v1 = 2 * hsl.hsl.l - v2;
                r = 255 * HueToRgb(v1, v2, hue + (1.0f / 3));
                g = 255 * HueToRgb(v1, v2, hue);
                b = 255 * HueToRgb(v1, v2, hue - (1.0f / 3));
            }
            return new Rgb((int)r, (int)g, (int)b);
        }
        static public Hsl RgbToHsl(Rgb rgb)
        {
            Hsl hsl = new Hsl();
            float r = (rgb.rgb.r / 255.0f);
            float g = (rgb.rgb.g / 255.0f);
            float b = (rgb.rgb.b / 255.0f);
            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);
            float delta = max - min;
            hsl.hsl.l = (max + min) / 2;
            if (delta == 0)
            {
                hsl.hsl.h = 0;
                hsl.hsl.s = 0.0f;
            }
            else
            {
                hsl.hsl.s = (hsl.hsl.l <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));
                float hue;
                if (r == max)
                {
                    hue = ((g - b) / 6) / delta;
                }
                else if (g == max)
                {
                    hue = (1.0f / 3) + ((b - r) / 6) / delta;
                }
                else
                {
                    hue = (2.0f / 3) + ((r - g) / 6) / delta;
                }
                if (hue < 0)
                {
                    hue += 1;
                }
                else if (hue > 1)
                {
                    hue -= 1;
                }
                hsl.hsl.h = (int)(hue * 360);
            }
            hsl.hsl.s *= 100;
            hsl.hsl.l = (int)Math.Round(hsl.hsl.l *= 100, MidpointRounding.ToZero);
            return hsl;
        }
        static public Rgb HexToRgb(Hex hex)
        {
            char[] temp = hex.hex.ToCharArray();
            return new Rgb(s.Convert.ToInt32($"{temp[0]}{temp[1]}", 16), s.Convert.ToInt32($"{temp[2]}{temp[3]}", 16), s.Convert.ToInt32($"{temp[4]}{temp[5]}", 16));
        }
        private static float HueToRgb(float v1, float v2, float vH)
        {
            if (vH < 0)
            {
                vH += 1;
            }
            if (vH > 1)
            {
                vH -= 1;
            }
            if ((6 * vH) < 1)
            {
                return v1 + (v2 - v1) * 6 * vH;
            }
            if ((2 * vH) < 1)
            {
                return v2;
            }
            if ((3 * vH) < 2)
            {
                return v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6;
            }
            return v1;
        }
        private static int Max(params int[] numbers)
        {
            return numbers.Max();
        }
        private static int Min(params int[] numbers)
        {
            return numbers.Min();
        }
    }
}