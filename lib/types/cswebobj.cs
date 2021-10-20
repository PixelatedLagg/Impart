using System;
using System.IO;
using System.Collections.Generic;
using Csweb.Templates;

namespace Csweb
{
    public class cswebobj
    {
        internal string path;
        internal string textCache;
        internal string cssPath;
        private List<idstyle> idstyles = new List<idstyle>();
        private List<estyle> estyles = new List<estyle>();
        private List<classstyle> classstyles = new List<classstyle>();
        public cswebobj(string path, string cssPath)
        {
            CSwebconfig.Initialize();
            Timer.StartTimer();
            this.path = path;
            textCache = "";
            this.cssPath = cssPath;
            Debug.CallObjectEvent("[cswebobj] created cswebobj");
        }
        public void AddStyle(estyle style)
        {
            estyles.Add(style);
        }
        public void AddStyle(idstyle style)
        {
            idstyles.Add(style);
        }
        public void AddStyle(classstyle style)
        {
            classstyles.Add(style);
        }
        public void AddText(string text, string id)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(text)) 
            {
                throw new ArgumentException("Text cannot be empty or null!");
            }
            if (String.IsNullOrEmpty(id))
            {
                textCache = $"{textCache}%^    <p>{text}</p>";
            }
            else
            {
                textCache = $"{textCache}%^    <p id=\"{id}\">{text}</p>";
            }
            Debug.CallObjectEvent("[cswebobj] added text element");
        }
        public void SetTitle(string title)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title cannot be null or empty!");
            }
            textCache = $"{textCache}%^    <title>{title}</title>";
            Debug.CallObjectEvent("[cswebobj] set title");
        }
        public void AddImage(string path, Nullable<(int x, int y)> dimensions, string id)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(path)) 
            {
                throw new ArgumentException("Image path cannot be empty or null!");
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Image file not found!");
            }
            if (!Common.IsImage(Path.GetExtension(path)))
            {
                throw new ArgumentException("Unsupported file extension!");
            }
            if (dimensions != null)
            {
                if (String.IsNullOrEmpty(id))
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\" width=\"{dimensions.Value.x}\" height=\"{dimensions.Value.y}\"></img>";
                }
                else 
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\" id=\"{id}\" width=\"{dimensions.Value.x}\" height=\"{dimensions.Value.y}\"></img>";
                }
            }
            else
            {
                if (String.IsNullOrEmpty(id))
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\"></img>";
                }
                else 
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\" id=\"{id}\"></img>";
                }
            }
            Debug.CallObjectEvent("[cswebobj] added image element");
        }
        public void AddHeader(int number, string text, string id)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text cannot be null of empty!");
            }
            if (number < 1 || number > 5)
            {
                throw new ArgumentException("Number must be between 1-5!");
            }
            if (id != null)
            {
                textCache = $"{textCache}%^    <h{number} id=\"{id}\">{text}</h{number}>";
            }
            else
            {
                textCache = $"{textCache}%^    <h{number}>{text}</h{number}>";
            }
            Debug.CallObjectEvent("[cswebobj] added header");
        }
        public void AddTemplate(Template templates, object[] args = null)
        {
            Templates.Templates.RenderTemplate(templates, args);
        }
        public void AddCustomTemplate(string name)
        {
            if (!Templates.Templates.CustomTemplates.ContainsKey(name))
            {
                throw new ArgumentException("Template does not exist!");
            }
            textCache = $"{textCache}{Templates.Templates.CustomTemplates[name]}";
        }
        public void Render()
        {
            Timer.StartTimer();
            string tempCache = "";
            foreach (estyle style in estyles)
            {
                if (tempCache == "")
                {
                    tempCache = $"{tempCache}{style.Render()}{Environment.NewLine}";
                }
                else
                {
                    tempCache = $"{tempCache}{Environment.NewLine}{style.Render()}{Environment.NewLine}";
                }
            }
            foreach (classstyle style in classstyles)
            {
                if (tempCache == "")
                {
                    tempCache = $"{tempCache}{style.Render()}{Environment.NewLine}";
                }
                else
                {
                    tempCache = $"{tempCache}{Environment.NewLine}{style.Render()}{Environment.NewLine}";
                }
            }
            foreach (idstyle style in idstyles)
            {
                if (tempCache == "")
                {
                    tempCache = $"{tempCache}{style.Render()}{Environment.NewLine}";
                }
                else
                {
                    tempCache = $"{tempCache}{Environment.NewLine}{style.Render()}{Environment.NewLine}";
                }
            }
            Common.Change(path, $"<!-- Generated by CSWeb - https://github.com/PixelatedLagg/CSWeb-lib -->"
            + $"{Environment.NewLine}<html>{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{cssPath}\">"
            + $"{textCache.Replace("%^", Environment.NewLine)}{Environment.NewLine}</html>");
            Common.Change(cssPath, tempCache);
            Debug.CallObjectEvent("[cswebobj] rendered cswebobj");
        }
    }
}