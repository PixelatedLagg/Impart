using System.Text;

namespace Impart.Templates
{
    public static class Sidebar
    {
        public static void AddSidebarTemplate(this WebPage obj, Alignment alignment, params Text[] args)
        {
            StringBuilder result = new StringBuilder();
            switch (alignment)
            {
                case Alignment.Left:
                    result.Append($"<div style=\"float: left; padding: 20px;\">");
                    break;
                case Alignment.Right:
                    result.Append($"<div style=\"float: right; padding: 20px;\">");
                    break;
                case Alignment.Center:
                case Alignment.Justify:
                    throw new ImpartError("Cannot use justify/center as an alignment for this template.");
            }
            foreach (Text t in args)
            {
                if (t.id == null)
                {
                    result.Append($"<p{t.attributeBuilder.ToString()}{t.style}>{t.text}</p>");
                }
                else
                {
                    result.Append($"<p id=\"{t.id}\"{t.attributeBuilder.ToString()}{t.style}>{t.text}</p>");
                }
            }
            result.Append("</div>");
            obj.textBuilder.Append(result.ToString());
        }
    }
}