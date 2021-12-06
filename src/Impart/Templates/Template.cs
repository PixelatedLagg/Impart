using System.Collections.Generic;

namespace Impart.Templates
{
    static class Templates
    {
        internal static Dictionary<string, string> CustomTemplates = new Dictionary<string, string>();
        public static void AddCustomTemplate(string name, WebPage obj)
        {
            CustomTemplates.Add(name, obj.textCache);
        }
        public static void AddCustomTemplate(this WebPage obj, string name)
        {
            Timer.StartTimer();
            if (!CustomTemplates.ContainsKey(name))
            {
                throw new TemplateError("Template does not exist!");
            }
            obj.textCache += CustomTemplates[name];
            Debug.CallObjectEvent("[cswebobj] added custom template");
        }
    }
}
