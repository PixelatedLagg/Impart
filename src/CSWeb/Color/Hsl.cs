using System;

namespace CSWeb
{
    public struct Hsl : Color
    {
        public (float h, float s, float l) hsl;
        public Hsl(float h, float s, float l)
        {
            hsl = (h, s, l);
            if (h > 360 || h < 0)
            {
                throw new ColorError("Invalid hue value!", this);
            }
            if (s > 100 || s < 0)
            {
                throw new ColorError("Invalid saturation value!", this);
            }
            if (l > 100 || l < 0)
            {
                throw new ColorError("Invalid luminosity value!", this);
            }
        }
        public static bool operator ==(Hsl hsl1, Hsl hsl2)
        {
            return (hsl1.hsl.h == hsl2.hsl.h && hsl1.hsl.s == hsl2.hsl.s && hsl1.hsl.l == hsl2.hsl.l);
        }
        public static bool operator !=(Hsl hsl1, Hsl hsl2)
        {
            return !(hsl1.hsl.h == hsl2.hsl.h && hsl1.hsl.s == hsl2.hsl.s && hsl1.hsl.l == hsl2.hsl.l);
        }
        public static bool operator ==(Hsl hsl1, (float h, float s, float l) hsl2)
        {
            return (hsl1.hsl.h == hsl2.h && hsl1.hsl.s == hsl2.s && hsl1.hsl.l == hsl2.l);
        }
        public static bool operator !=(Hsl hsl1, (float h, float s, float l) hsl2)
        {
            return !(hsl1.hsl.h == hsl2.h && hsl1.hsl.s == hsl2.s && hsl1.hsl.l == hsl2.l);
        }
        public static bool operator ==(Hsl hsl1, Tuple<float, float, float> hsl2)
        {
            return (hsl1.hsl.h == hsl2.Item1 && hsl1.hsl.s == hsl2.Item2 && hsl1.hsl.l == hsl2.Item3);
        }
        public static bool operator !=(Hsl hsl1, Tuple<float, float, float> hsl2)
        {
            return !(hsl1.hsl.h == hsl2.Item1 && hsl1.hsl.s == hsl2.Item2 && hsl1.hsl.l == hsl2.Item3);
        }
        public override bool Equals(object obj)
        {
            if (this == obj as Hsl? || this == obj as Tuple<float, float, float>)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}