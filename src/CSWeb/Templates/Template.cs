using CSWeb;
using System;
using System.Collections.Generic;

namespace CSWeb.Templates
{
    static class Templates
    {
        internal static Dictionary<string, string> CustomTemplates = new Dictionary<string, string>();
        public static void AddCustomTemplate(string name, cswebobj obj)
        {
            CustomTemplates.Add(name, obj.textCache.Replace($"<html>{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{obj.cssPath}\">", "").Replace("</html>", ""));
        }
        static internal string RenderTemplate(Template templates, params string[] args)
        {
            switch (templates)
            {
                case Template.Article:
                    return Article.Render(args);
                default:
                    return "";
            }
        }
    }
}