namespace Impart.Templates
{
    public static class Sidebar
    {
        public static void AddSidebarTemplate(this WebPage obj, string alignment, params Text[] args)
        {
            if (!Alignment.Any(alignment) || alignment == "justify")
            {
                throw new TemplateError("Enter a valid alignment value!");
            }
            string textCache = $"<div style=\"float: {alignment}; padding: 20px;\">";
        }
    }
}