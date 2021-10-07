using System.Threading;
using System.Globalization;
using System;

namespace Csweb
{
    public class cswebobj
    {
        private string path;
        public string id;
        internal string textCache;
        public cswebobj(cswebroot root, string id)
        {
            path = root.path;
            this.id = id;
            textCache = "";
        }
        //how the developer adds elements
        public void AddComponent(int component, string text, string style)
        {
            switch (component)
            {
                case 0:
                    if (String.IsNullOrEmpty(style))
                    {
                        textCache = $"{textCache}%^<p>{text}</p>";
                        break;
                    }
                    textCache = StyleParser.Parse(style);
                    break;
                case 1:
                //image
                break;
            }
        }
        //how the developer writes the text cache to file
        public void Render()
        {
            Changer.Change(path, textCache.Replace("%^", Environment.NewLine));
        }
    }
}