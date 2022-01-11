using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Impart.Scripting
{
    public static class WebPageExtensions
    {
        /*public static void DeleteByID(this WebPage webpage, string id)
        {
            CheckRazor(webpage);
            List<string> elements = new List<string>();
            foreach (string s in File.ReadLines(webpage.razorPath).ToArray())
            {
                if (s.Contains($"id=\"{id}\""))
                {
                    elements.Add(s);
                }
            }
            foreach (string s in elements)
            {
                contents.Replace(s, "");
            }
            File.WriteAllText(webpage.path, contents);
        }
        public static void DeleteByClass(this WebPage webpage, string cls)
        {
            CheckRazor(webpage);
            List<string> elements = new List<string>();
            foreach (string s in File.ReadLines(webpage.razorPath).ToArray())
            {
                if (s.Contains($"class=\"{cls}\""))
                {
                    elements.Add(s);
                }
            }
            foreach (string s in elements)
            {
                contents.Replace(s, "");
            }
            File.WriteAllText(webpage.path, contents);
        }
        private static void CheckRazor(WebPage webpage)
        {
            if (webpage.path != null)
            {
                throw new ScriptingError("Page must be a razor page!");
            }
            if (webpage.rendered == false)
            {
                throw new ScriptingError("You must render the page to edit it!");
            }
        }*/
    }
}