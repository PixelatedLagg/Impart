using System;

namespace Impart
{
    /// <summary>The class for managing Color conversions.</summary>
    public static class ConvertColor
    {
        /// <summary>Converts a Rgb instance to Hsl.</summary>
        /// <returns>A Hsl instance.</returns>
        /// <param name="rgb">The Rgb value to convert.</param>
        public static Hsl ToHsl(this Rgb rgb)
        {
            Hsl hsl = new Hsl(1, 1, 1);
            float r = (rgb.rgb.r / 255.0f), g = (rgb.rgb.g / 255.0f), b = (rgb.rgb.b / 255.0f);
            float min = Math.Min(Math.Min(r, g), b), max = Math.Max(Math.Max(r, g), b);
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
            hsl.hsl.l *= 100;
            return hsl;
        }

        /// <summary>Converts a Rgb instance to Hex.</summary>
        /// <returns>A Hex instance.</returns>
        /// <param name="rgb">The Rgb value to convert.</param>
        public static Hex ToHex(this Rgb rgb)
        {
            System.Drawing.Color myColor = System.Drawing.Color.FromArgb(rgb.rgb.r, rgb.rgb.g, rgb.rgb.b);
            return new Hex(myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2"));
        }

        /// <summary>Converts a Hsl instance to Rgb.</summary>
        /// <returns>A Rgb instance.</returns>
        /// <param name="hsl">The Hsl value to convert.</param>
        public static Rgb ToRgb(this Hsl hsl)
        {
            (float h, float s, float l) temphsl = hsl.hsl;
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
        /// <returns>A Hex instance.</returns>
        /// <param name="hsl">The Hsl value to convert.</param>
        public static Hex ToHex(this Hsl hsl)
        {
            return hsl.ToRgb().ToHex();
        }

        /// <summary>Converts a Hex instance to Rgb.</summary>
        /// <returns>A Rgb instance.</returns>
        /// <param name="hex">The Hex value to convert.</param>
        public static Rgb ToRgb(this Hex hex)
        {
            char[] temp = hex.hex.ToCharArray();
            return new Rgb(System.Convert.ToInt32($"{temp[0]}{temp[1]}", 16), System.Convert.ToInt32($"{temp[2]}{temp[3]}", 16), System.Convert.ToInt32($"{temp[4]}{temp[5]}", 16));
        }

        /// <summary>Converts a Color instance to Rgb.</summary>
        /// <returns>A Rgb instance.</returns>
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
        /// <returns>A Hsl instance.</returns>
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
        /// <returns>A Hex instance.</returns>
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