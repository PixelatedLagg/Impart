using System;

namespace Impart
{
    /// <summary>Class that represents a division.</summary>
    public struct Division : Element, IDisposable
    {
        public (int? x, int? y) size;
        private bool[] setProperties;
        private string elementCache;
        private string styleCache;
        private string attributeCache;
        internal string cssCache;
        internal string id;
        internal string scrollId;
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

        /// <summary>Constructor for the division class.</summary>
        public Division(ImpartCommon.IDType? type = null, string id = null)
        {
            Timer.StartTimer();
            this.id = "";
            scrollId = "";
            if (String.IsNullOrEmpty(id))
            {
                id = null;
                scrollId = null;
            }
            switch (type, id)
            {
                case (ImpartCommon.IDType, string) a when a != (null, null):
                    if (type == ImpartCommon.IDType.ID)
                    {
                        this.id = $" id=\"{id}\"";
                        scrollId = $"#{id}";
                    }
                    else
                    {
                        this.id = $" class=\"{id}\"";
                        scrollId = $".{id}";
                    }
                    break;
                case (ImpartCommon.IDType, string) b when b.type == null && b.id != null:
                    throw new DivisionError("Type and ID must both be null or not null!");
                case (ImpartCommon.IDType, string) c when c.type != null && c.id == null:
                    throw new DivisionError("Type and ID must both be null or not null!");
            }
            styleCache = " margin: 0px;";
            size = (null, null);
            cssCache = "";
            attributeCache = "";
            setProperties = new bool[] {false, false, false, false, false, false};
            elementCache = "";
            Debug.CallObjectEvent("[division] initialized division");
        }

        /// <summary>Method for adding text to the division.</summary>
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
            Debug.CallObjectEvent("[division] added text");
        }

        /// <summary>Method for adding an image to the division.</summary>
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
            Debug.CallObjectEvent("[division] added image");
        }

        /// <summary>Method for setting the division color.</summary>
        public Division SetColor(Color color)
        {
            Timer.StartTimer();
            if (setProperties[0])
            {
                throw new DivisionError("Cannot set properties twice!");
            }
            if (color == null)
            {
                throw new ColorError("Color cannot be null!");
            }
            setProperties[0] = true;
            switch (color.GetType().FullName)
            {
                case "Impart.Rgb":
                    Rgb rgb = (Rgb)color;
                    styleCache += $" background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case "Impart.Hsl":
                    Hsl hsl = (Hsl)color;
                    styleCache += $" background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case "Impart.Hex":
                    Hex hex = (Hex)color;
                    styleCache += $" background-color: #{hex.hex};";
                    break;
            }
            Debug.CallObjectEvent("[division] set color");
            return this;
        }

        /// <summary>Method for setting the division border.</summary>
        public Division SetBorder(int pixels, string border, Color color, int roundedPixels = 0)
        {
            Timer.StartTimer();
            if (setProperties[1])
            {
                throw new DivisionError("Cannot set properties twice!");
            }
            setProperties[1] = true;
            if (!Border.Any(border))
            {
                throw new DivisionError("Invalid border value!");
            }
            switch (color.GetType().FullName)
            {
                case "Impart.Rgb":
                    Rgb rgb = (Rgb)color;
                    styleCache += $" border: {pixels}px {border} rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case "Impart.Hsl":
                    Hsl hsl = (Hsl)color;
                    styleCache += $" border: {pixels}px {border} hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case "Impart.Hex":
                    Hex hex = (Hex)color;
                    styleCache += $" border: {pixels}px {border} #{hex};";
                    break;
            }
            if (roundedPixels > 0)
            {
                styleCache += $" border-radius: {roundedPixels}px;";
            }
            Debug.CallObjectEvent("[division] set border");
            return this;
        }

        /// <summary>Method for setting the division to follow the scroll.</summary>
        public Division SetFollowScroll(bool obj)
        {
            Timer.StartTimer();
            if (setProperties[2])
            {
                throw new DivisionError("Cannot set properties twice!");
            }
            setProperties[2] = true;
            if (obj)
            {
                styleCache += " position: fixed;";
            }
            else
            {
                throw new DivisionError("Default value for follow scroll is false!");
            }
            Debug.CallObjectEvent("[division] set follow scroll");
            return this;
        }

        /// <summary>Method for setting the division margin.</summary>
        public Division SetMargin(int pixels)
        {
            Timer.StartTimer();
            if (setProperties[3])
            {
                throw new DivisionError("Cannot set properties twice!");
            }
            setProperties[3] = true;
            if (pixels == 0)
            {
                throw new DivisionError("Border thickness is default 0 pixels!");
            }
            if (pixels < 0)
            {
                throw new DivisionError("Margin thickness must be above 0!");
            }
            styleCache += $" margin: {pixels}px;";
            Debug.CallObjectEvent("[division] set margin");
            return this;
        }

        /// <summary>Method for setting the division size.</summary>
        public Division SetSize(int? x, int? y)
        {
            Timer.StartTimer();
            if (setProperties[4])
            {
                throw new DivisionError("Cannot set properties twice!");
            }
            setProperties[4] = true;
            if (y < 0 || x < 0)
            {
                throw new DivisionError("Invalid size values!");
            }
            if (x != null)
            {
                styleCache += $" max-width: {x}px;";
            }
            if (y != null)
            {
                styleCache += $" max-height: {y}px;";
            }
            Debug.CallObjectEvent("[division] set size");
            return this;
        }

        /// <summary>Method for setting the division scrollbar.</summary>
        public Division SetScrollBar(Scrollbar scrollbar)
        {
            Timer.StartTimer();
            if (setProperties[5])
            {
                throw new DivisionError("Cannot set properties twice!");
            }
            setProperties[5] = true;
            if (scrollbar.divID != id && id != null)
            {
                throw new ScrollbarError("ID of division must either be null or match the ID inputted to the scrollbar!");
            }
            if (id == null)
            {
                id = scrollbar.divID;
            }
            cssCache += scrollbar.cssCache;
            Debug.CallObjectEvent("[division] set scrollbar");
            return this;
        }

        /// <summary>Method for adding a text break in the division.</summary>
        public void Break()
        {
            elementCache += $"%^        <br>";
        }
        public void Dispose() {}
    } 
}