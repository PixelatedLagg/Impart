using System.Text;

namespace Impart.Templates
{
    public static class Sidebar
    {
        public static void AddSidebarTemplate(this WebPage obj, string alignment, params Text[] args)
        {
            string result = "";
            if (!Alignment.Any(alignment) || alignment == "justify")
            {
                throw new ImpartError("Enter a valid alignment value!");
            }
            result += $"<div style=\"float: {alignment}; padding: 20px;\">";
            foreach (Text t in args)
            {
                if (t.id == null)
                {
                    result += $"%^    <p{t.attributes}{t.style}>{t.text}</p>";
                }
                else
                {
                    result += $"%^    <p id=\"{t.id}\"{t.attributes}{t.style}>{t.text}</p>";
                }
            }
            result += "%^</div>";
            obj.WriteText(result);
        }
    }
}