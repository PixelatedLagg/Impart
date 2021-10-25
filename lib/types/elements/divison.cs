using System.Linq.Expressions;
using System;

namespace Csweb
{
    public class Division : IDisposable
    {
        private string _textCache;
        internal string textCache 
        {
            get 
            {
                return $"{_textCache}%^    </div>";
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
                        _textCache = $"%^    <div id=\"{id}\">%^";
                    }
                    else
                    {
                        _textCache = $"%^    <div class=\"{id}\">%^";
                    }
                    break;
                case (DivisionType, string) b when b.type == null && b.id != null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
                case (DivisionType, string) c when c.type != null && c.id == null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
                default:
                    _textCache = $"%^    <div>";
                    break;
            }
        }
        public void AddText(Text text)
        {
            Timer.StartTimer();
            if (text.id == null)
            {
                _textCache += $"%^        <p{text.attributes}{text.style}>{text.text}</p>";
            }
            else
            {
                _textCache += $"%^        <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>";
            }
            Debug.CallObjectEvent("[division] added text element");
        }
        public void AddImage(Image image)
        {
            Timer.StartTimer();
            if (image.id == null)
            {
                _textCache += $"%^        <img src=\"{image.path}\"{image.attributes}{image.style}>";
            }
            else
            {
                _textCache += $"%^        <img src=\"{image.path}\" id=\"{image.id}\"{image.attributes}{image.style}>";
            }
            Debug.CallObjectEvent("[division] added image element");
        }
        public void Dispose() {}
    } 
    public enum DivisionType
    {
        ID = 1,
        Class = 0
    }
}