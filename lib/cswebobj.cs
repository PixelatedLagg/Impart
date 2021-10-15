using System;
using System.IO;
using System.Collections.Generic;

namespace Csweb
{
    public class cswebobj
    {
        private string path;
        internal string textCache;
        private string cssPath;
        private List<idstyle> idstyles = new List<idstyle>();
        private List<estyle> estyles = new List<estyle>();
        public cswebobj(string path, string cssPath)
        {
            Timer.StartTimer();
            this.path = path;
            textCache = "";
            this.cssPath = cssPath;
            Debug.CallObjectEvent(new Log("[cswebobj] created cswebobj", Timer.GetTime()));
        }
        public void AddStyle(estyle style)
        {
            estyles.Add(style);
        }
        public void AddStyle(idstyle style)
        {
            idstyles.Add(style);
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
            Debug.CallObjectEvent(new Log("[cswebobj] added text element", Timer.GetTime()));
        }
        public void SetTitle(string title)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title cannot be null or empty!");
            }
            textCache = $"{textCache}%^    <title>{title}</title>";
            Debug.CallObjectEvent(new Log("[cswebobj] set title", Timer.GetTime()));
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
            Debug.CallObjectEvent(new Log("[cswebobj] added image element", Timer.GetTime()));
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
            Debug.CallObjectEvent(new Log("[cswebobj] added header", Timer.GetTime()));
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
            Debug.CallObjectEvent(new Log("[cswebobj] rendered cswebobj", Timer.GetTime()));
        }
    }
}