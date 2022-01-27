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
                    return $"    <div{id}{attributeCache}>{elementCache}    </div>";
                }
                return $"    <div{id}{attributeCache} style=\"{$"\"{styleCache}".Replace("\" ", "")}\">{elementCache}    </div>";
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
                elementCache += $"        <p{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>";
            }
            else
            {
                elementCache += $"        <p id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>";
            }
        }

        /// <summary>Method for adding an image to the division.</summary>
        public void AddImage(Image image)
        {
            if (image.id == null)
            {
                elementCache += $"        <img src=\"{image.path}\"{image.attributes}{image.style}>";
            }
            else
            {
                elementCache += $"        <img src=\"{image.path}\" id=\"{image.id}\"{image.attributes}{image.style}>";
            }
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
            elementCache += $"        <br>";
        }
        public void Dispose() {}
    } 
}