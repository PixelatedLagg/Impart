using System.Collections.Generic;

namespace Impart.Templates
{
    static class Templates
    {
        internal static Dictionary<string, string> CustomTemplates = new Dictionary<string, string>();
        public static void AddCustomTemplate(string name, WebPage obj)
        {
            CustomTemplates.Add(name, obj.textBuilder.ToString());
        }
        public static void AddCustomTemplate(this WebPage obj, string name)
        {
            if (!CustomTemplates.ContainsKey(name))
            {
                throw new ImpartError("Template does not exist!");
            }
            obj.textBuilder.Append(CustomTemplates[name]);
        }
    }
}