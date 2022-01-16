using System;

namespace Impart
{
    /// <summary>Class that represents a division.</summary>
    public struct Division : Element, IDisposable
    {
        public (int? x, int? y) size;
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
                    throw new ImpartError("Type and ID must both be null or not null!");
                case (ImpartCommon.IDType, string) c when c.type != null && c.id == null:
                    throw new ImpartError("Type and ID must both be null or not null!");
            }
            styleCache = " margin: 0px;";
            size = (null, null);
            cssCache = "";
            attributeCache = "";
            elementCache = "";
        }

        /// <summary>Method for adding text to the division.</summary>
        public void AddText(Text text)
        {
            if (text.id == null)
            {
                elementCache += $"%^        <p{text.attributes}{text.style}>{text.text}</p>";
            }
            else
            {
                elementCache += $"%^        <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>";
            }
        }

        /// <summary>Method for adding an image to the division.</summary>
        public void AddImage(Image image)
        {
            if (image.id == null)
            {
                elementCache += $"%^        <img src=\"{image.path}\"{image.attributes}{image.style}>";
            }
            else
            {
                elementCache += $"%^        <img src=\"{image.path}\" id=\"{image.id}\"{image.attributes}{image.style}>";
            }
        }

        /// <summary>Method for setting the division color.</summary>
        public Division SetColor(Color color)
        {
            if (color == null)
            {
                throw new ImpartError("Color cannot be null!");
            }
            switch (color)
            {
                case Rgb rgb:
                    styleCache += $" background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case Hsl hsl:
                    styleCache += $" background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case Hex hex:
                    styleCache += $" background-color: #{hex.hex};";
                    break;
            }
            return this;
        }

        /// <summary>Method for setting the division border.</summary>
        public Division SetBorder(int pixels, string border, Color color, int roundedPixels = 0)
        {
            if (!Border.Any(border))
            {
                throw new ImpartError("Invalid border value!");
            }
            switch (color)
            {
                case Rgb rgb:
                    styleCache += $" border: {pixels}px {border} rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case Hsl hsl:
                    styleCache += $" border: {pixels}px {border} hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case Hex hex:
                    styleCache += $" border: {pixels}px {border} #{hex};";
                    break;
            }
            if (roundedPixels > 0)
            {
                styleCache += $" border-radius: {roundedPixels}px;";
            }
            return this;
        }

        /// <summary>Method for setting the division to follow the scroll.</summary>
        public Division SetFollowScroll(bool obj)
        {
            if (obj)
            {
                styleCache += " position: fixed;";
            }
            else
            {
                throw new ImpartError("Default value for follow scroll is false!");
            }
            return this;
        }

        /// <summary>Method for setting the division margin.</summary>
        public Division SetMargin(int pixels)
        {
            if (pixels == 0)
            {
                throw new ImpartError("Border thickness is default 0 pixels!");
            }
            if (pixels < 0)
            {
                throw new ImpartError("Margin thickness must be above 0!");
            }
            styleCache += $" margin: {pixels}px;";
            return this;
        }

        /// <summary>Method for setting the division size.</summary>
        public Division SetSize(int? x, int? y)
        {
            if (y < 0 || x < 0)
            {
                throw new ImpartError("Invalid size values!");
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

        /// <summary>Method for setting the division scrollbar.</summary>
        public Division SetScrollBar(Scrollbar scrollbar)
        {
            if (scrollbar.divID != id && id != null)
            {
                throw new ImpartError("ID of division must either be null or match the ID inputted to the scrollbar!");
            }
            if (id == null)
            {
                id = scrollbar.divID;
            }
            cssCache += scrollbar.cssCache;
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