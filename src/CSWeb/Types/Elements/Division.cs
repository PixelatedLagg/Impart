using System;

namespace CSWeb
{
    public class Division : Element, IDisposable
    {
        public (int? x, int? y) size;
        private int colorCheck;
        private string elementCache;
        private string styleCache;
        private string attributeCache;
        internal string cssCache;
        internal string id;
        internal string textCache 
        {
            get 
            {
                if (styleCache == "")
                {
                    return $"%^    <div{id}{attributeCache}>{elementCache}%^    </div>";
                }
                return $"%^    <div{id}{attributeCache} style=\"{$"\"{styleCache}".Replace("\" ", "")}\">{elementCache}%^    </div>";
            }
        }
        public Division(ICSWeb.IDType? type = null, string id = null)
        {
            if (String.IsNullOrEmpty(id))
            {
                id = null;
            }
            switch (type, id)
            {
                case (ICSWeb.IDType, string) a when a != (null, null):
                    if (type == ICSWeb.IDType.ID)
                    {
                        this.id = $" id=\"{id}\"";
                    }
                    else
                    {
                        this.id = $" class=\"{id}\"";
                    }
                    break;
                case (ICSWeb.IDType, string) b when b.type == null && b.id != null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
                case (ICSWeb.IDType, string) c when c.type != null && c.id == null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
            }
            colorCheck = 0;
            styleCache = " margin: 0px;";
            size = (null, null);
            cssCache = "";
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
        public Division SetColor(Hex hex)
        {
            if (colorCheck > 1)
            {
                throw new DivisionError("Cannot set color more than once!", this);
            }
            colorCheck++;
            styleCache += $" background-color: #{hex.hex};";
            return this;
        }
        public Division SetColor(Hsl hsl)
        {
            if (colorCheck > 1)
            {
                throw new DivisionError("Cannot set color more than once!", this);
            }
            colorCheck++;
            styleCache += $" background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
            return this;
        }
        public Division SetColor(Rgb rgb)
        {
            if (colorCheck > 1)
            {
                throw new DivisionError("Cannot set color more than once!", this);
            }
            colorCheck++;
            styleCache += $" background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
            return this;
        }
        public Division SetBorder(int pixels, string border, Hex hex, int roundedPixels = 0)
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
        public Division SetBorder(int pixels, string border, Hsl hsl, int roundedPixels = 0)
        {
            Timer.StartTimer();
            if (!Border.Any(border))
            {
                throw new DivisionError("Invalid border value!", this);
            }
            styleCache += $" border: {pixels}px {border} hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
            if (roundedPixels > 0)
            {
                styleCache += $" border-radius: {roundedPixels}px;";
            }
            Debug.CallObjectEvent("[division] added border");
            return this;
        }
        public Division SetBorder(int pixels, string border, Rgb rgb, int roundedPixels = 0)
        {
            Timer.StartTimer();
            if (!Border.Any(border))
            {
                throw new DivisionError("Invalid border value!", this);
            }
            styleCache += $" border: {pixels}px {border} rgb({rgb.rgb.r}, {rgb.rgb.g}, {rgb.rgb.b});";
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
        public Division SetSize(int? x, int? y)
        {
            if (y < 0 || x < 0)
            {
                throw new DivisionError("Invalid size values!", this);
            }
            if (x != null)
            {
                styleCache += $" max-width: {x}px;";
            }
            if (y != null)
            {
                styleCache += $" max-height: {y}px;";
            }
            return this;
        }
        public Division SetScrollBar(Scrollbar scrollbar)
        {
            if (scrollbar.divID != id && id != null)
            {
                throw new ScrollbarError("ID of division must either be null or match the ID inputted to the scrollbar!", scrollbar);
            }
            if (id == null)
            {
                id = scrollbar.divID;
            }
            cssCache += scrollbar.cssCache;
            return this;
        }
        public void Break()
        {
            elementCache += $"%^        <br>";
        }
        public void Dispose() {}
    } 
}