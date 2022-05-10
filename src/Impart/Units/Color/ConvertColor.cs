using System;

namespace Impart
{
    /// <summary>The class for managing Color conversions.</summary>
    public static class ConvertColor
    {
        /// <summary>Converts a Rgb instance to Hsl.</summary>
        /// <param name="rgb">The Rgb value to convert.</param>
        public static Hsl ToHsl(this Rgb rgb)
        {
            (int r, int g, int b) tempRgb = (((int r, int g, int b))rgb);
            float h, s, l;
            float r = (tempRgb.r / 255.0f), g = (tempRgb.g / 255.0f), b = (tempRgb.b / 255.0f);
            float min = Math.Min(Math.Min(r, g), b), max = Math.Max(Math.Max(r, g), b);
            float delta = max - min;
            l = (max + min) / 2;
            if (delta == 0)
            {
                h = 0;
                s = 0.0f;
            }
            else
            {
                s = (l <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));
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
                h = (int)(hue * 360);
            }
            s *= 100;
            l *= 100;
            return new Hsl(h, s, l);
        }

        /// <summary>Converts a Rgb instance to Hex.</summary>
        /// <param name="rgb">The Rgb value to convert.</param>
        public static Hex ToHex(this Rgb rgb)
        {
            (int r, int g, int b) tempRgb = (((int r, int g, int b))rgb);
            System.Drawing.Color myColor = System.Drawing.Color.FromArgb(tempRgb.r, tempRgb.g, tempRgb.b);
            return new Hex(myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2"));
        }

        /// <summary>Converts a Hsl instance to Rgb.</summary>
        /// <param name="hsl">The Hsl value to convert.</param>
        public static Rgb ToRgb(this Hsl hsl)
        {
            (float h, float s, float l) temphsl = (((float h, float s, float l))hsl);
            temphsl.s /= 100;
            temphsl.l /= 100;
            float r = 0;
            float g = 0;
            float b = 0;
            if (temphsl.s == 0)
            {
                r = g = b = temphsl.l * 255;
            }
            else
            {
                float v1, v2;
                float hue = temphsl.h / 360;
                v2 = (temphsl.l < 0.5) ? (temphsl.l * (1 + temphsl.s)) : ((temphsl.l + temphsl.s) - (temphsl.l * temphsl.s));
                v1 = 2 * temphsl.l - v2;
                r = 255 * HueToRgb(v1, v2, hue + (1.0f / 3));
                g = 255 * HueToRgb(v1, v2, hue);
                b = 255 * HueToRgb(v1, v2, hue - (1.0f / 3));
            }
            return new Rgb((int)r, (int)g, (int)b);
        }

        /// <summary>Converts a Hsl instance to Hex.</summary>
        /// <param name="hsl">The Hsl value to convert.</param>
        public static Hex ToHex(this Hsl hsl)
        {
            return hsl.ToRgb().ToHex();
        }

        /// <summary>Converts a Hex instance to Rgb.</summary>
        /// <param name="hex">The Hex value to convert.</param>
        public static Rgb ToRgb(this Hex hex)
        {
            char[] temp = hex.ToString().ToCharArray();
            return new Rgb(System.Convert.ToInt32($"{temp[0]}{temp[1]}", 16), System.Convert.ToInt32($"{temp[2]}{temp[3]}", 16), System.Convert.ToInt32($"{temp[4]}{temp[5]}", 16));
        }

        /// <summary>Converts a Color instance to Rgb.</summary>
        /// <param name="color">The Color value to convert.</param>
        public static Rgb ToRgb(this Color color)
        {
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            return (Rgb)color;
        }

        /// <summary>Converts a Color instance to Hsl.</summary>
        /// <param name="color">The Color value to convert.</param>
        public static Hsl ToHsl(this Color color)
        {
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            return (Hsl)color;
        }
        
        /// <summary>Converts a Color instance to Hex.</summary>
        /// <param name="color">The Color value to convert.</param>
        public static Hex ToHex(this Color color)
        {
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            return (Hex)color;
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
    }
}