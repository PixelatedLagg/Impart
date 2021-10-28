using System.Linq.Expressions;
using System;
using System.Drawing;

namespace Csweb
{
    public class Division : IDisposable
    {
        private int colorCheck;
        private string elementCache;
        private string styleCache;
        private string attributeCache;
        internal string textCache 
        {
            get 
            {
                return $"%^    <div{attributeCache} style=\"{$"\"{styleCache}".Replace("\" ", "")}\">{elementCache}%^    </div>";
            }
        }
        public Division(DivisionType? type = null, string id = null)
        {
            if (String.IsNullOrEmpty(id))
            {
                id = null;
            }
            switch (type, id)
            {
                case (DivisionType, string) a when a != (null, null):
                    if (type == DivisionType.ID)
                    {
                        attributeCache = $"id=\"{id}\"";
                    }
                    else
                    {
                        attributeCache = $"class=\"{id}\"";
                    }
                    break;
                case (DivisionType, string) b when b.type == null && b.id != null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
                case (DivisionType, string) c when c.type != null && c.id == null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
            }
            colorCheck = 0;
            styleCache = " margin: 0px;";
        }
        public void AddText(Text text)
        {
            Timer.StartTimer();
            if (text.id == null)
            {
                elementCache += $"%^        <p{text.attributes}{text.style}>{text.text}</p>";
            }
            else
            {
                elementCache += $"%^        <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>";
            }
            Debug.CallObjectEvent("[division] added text element");
        }
        public void AddImage(Image image)
        {
            Timer.StartTimer();
            if (image.id == null)
            {
                elementCache += $"%^        <img src=\"{image.path}\"{image.attributes}{image.style}>";
            }
            else
            {
                elementCache += $"%^        <img src=\"{image.path}\" id=\"{image.id}\"{image.attributes}{image.style}>";
            }
            Debug.CallObjectEvent("[division] added image element");
        }
        public Division SetColor(string hex)
        {
            Timer.StartTimer();
            if (colorCheck > 1)
            {
                throw new DivisionError("Cannot set color more than once!", this);
            }
            colorCheck++;
            if (hex == null || hex.Length != 6)
            {
                throw new DivisionError("Invalid hex value!", this);
            }
            styleCache += $" background-color: #{hex};";
            Debug.CallObjectEvent("[division] added color");
            return this;
        }
        public Division SetBorder(int pixels, string border, string hex, int roundedPixels = 0)
        {
            Timer.StartTimer();
            if (!Border.Any(border))
            {
                throw new DivisionError("Invalid border value!", this);
            }
            styleCache += $" border: {pixels}px {border} #{hex};";
            if (roundedPixels > 0)
            {
                styleCache += $" border-radius: {roundedPixels}px;";
            }
            Debug.CallObjectEvent("[division] added border");
            return this;
        }
        public Division SetFollowScroll(bool obj)
        {
            if (obj)
            {
                styleCache += " position: fixed;";
            }
            else
            {
                throw new DivisionError("Default value for follow scroll is false!", this);
            }
            return this;
        }
        public Division SetMargin(float percent)
        {
            if (percent > 1 || percent < 0)
            {
                throw new DivisionError("Percent number must be between 1-0!", this);
            }
            styleCache += $" margin: {percent * 100}%;";
            return this;
        }
        public Division SetMargin(int pixels)
        {
            if (pixels == 0)
            {
                throw new DivisionError("Border thickness is default 0 pixels!", this);
            }
            if (pixels < 0)
            {
                throw new DivisionError("Margin thickness must be above 0!", this);
            }
            styleCache += $" margin: {pixels}px;";
            return this;
        }
        public Division SetWidth(int pixels)
        {
            if (pixels < 0)
            {
                throw new DivisionError("Width must be above 0!", this);
            }
            styleCache += $" max-width: {pixels}px;";
            return this;
        }
        public void Break()
        {
            elementCache += $"%^        <br>";
        }
        public void Dispose() {}
    } 
    public enum DivisionType
    {
        ID = 1,
        Class = 0
    }
}