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
            Timer.StartTimer();
            estyles.Add(style);
            Debug.CallObjectEvent("[cswebobj] added style (estyle)");
        }
        public void AddStyle(idstyle style)
        {
            Timer.StartTimer();
            idstyles.Add(style);
            Debug.CallObjectEvent("[cswebobj] added style (idstyle)");
        }
        public void AddStyle(classstyle style)
        {
            Timer.StartTimer();
            classstyles.Add(style);
            Debug.CallObjectEvent("[cswebobj] added style (classstyle)");
        }
        public void AddText(string text, string id = null)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(text)) 
            {
                throw new CSWebObjError("Text cannot be empty or null!", this);
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
                throw new CSWebObjError("Title cannot be null or empty!", this);
            }
            textCache = $"{textCache}%^    <title>{title}</title>";
            Debug.CallObjectEvent("[cswebobj] set title");
        }
        public void AddImage(string path, Nullable<(int x, int y)> dimensions, string id = null)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(path)) 
            {
                throw new CSWebObjError("Image path cannot be empty or null!", this);
            }
            if (!File.Exists(path))
            {
                throw new CSWebObjError("Image file not found!", this);
            }
            if (!Common.IsImage(Path.GetExtension(path)))
            {
                throw new CSWebObjError("Unsupported file extension!", this);
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
        public void AddHeader(Header header)
        {
            Timer.StartTimer();
            if (header.id != null)
            {
                textCache = $"{textCache}%^    <h{header.num} id=\"{header.id}\">{header.text}</h{header.num}>";
            }
            else
            {
                textCache = $"{textCache}%^    <h{header.num}>{header.text}</h{header.num}>";
            }
            Debug.CallObjectEvent("[cswebobj] added header");
        }
        public void AddLink(Link link)
        {
            if (link.image != null)
            {
                switch (link.id, link.image.id)
                {
                    case (null, null):
                        textCache = $"{textCache}%^    <a href=\"{link.path}\">%^        <img src=\"{link.image.path}\">%^    </a>";
                        break;
                    case (string, string) a when a.Item1 != null && a.Item2 != null:
                        textCache = $"{textCache}%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <img src=\"{link.image.path}\" id=\"{link.image.id}\">%^    </a>";
                        break;
                    case (string, string) b when b.Item1 == null && b.Item2 != null:
                        textCache = $"{textCache}%^    <a href=\"{link.path}\">%^        <img src=\"{link.image.path}\" id=\"{link.image.id}\">%^    </a>";
                        break;
                    case (string, string) c when c.Item1 != null && c.Item2 == null:
                        textCache = $"{textCache}%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <img src=\"{link.image.path}\">%^    </a>";
                        break;
                }
            }
        }
        public void AddTable(int rowNum, params string[] obj)
        {
            Timer.StartTimer();
            if (rowNum > obj.Length)
            {
                throw new CSWebObjError("Number of table rows cannot be bigger than number of table entries!", this);
            }
            string tempCache = "%^    <table>";
            tempCache = $"{tempCache}%^        <tr>";
            for (int x = 0; x < rowNum; x++)
            {
                tempCache = $"{tempCache}%^            <th>{obj[x]}</th>";
            }
            tempCache = $"{tempCache}%^        </tr>"; 
            int vertRowNum = (int)Math.Round(Convert.ToDouble(((double)obj.Length - (double)rowNum) / (double)rowNum), MidpointRounding.AwayFromZero);
            if ((obj.Length - rowNum) % rowNum > 0)
            {
                int _currentobj = 0;
                for (int a = 0; a < vertRowNum + 1; a++)
                {
                    tempCache = $"{tempCache}%^        <tr>";
                    for (int x = 0; x < rowNum; x++)
                    {
                        if (obj.Length <= rowNum + _currentobj)
                        {
                            tempCache = $"{tempCache}%^        </tr>"; 
                            textCache = $"{textCache}{tempCache}%^    </table>";
                            Debug.CallObjectEvent("[cswebobj] added table");
                            return;
                        }
                        tempCache = $"{tempCache}%^            <td>{obj[rowNum + _currentobj]}</td>";
                        _currentobj++;
                    }
                    tempCache = $"{tempCache}%^        </tr>"; 
                }
            }
            int currentObj = 0;
            for (int a = 0; a < vertRowNum; a++)
            {
                tempCache = $"{tempCache}%^        <tr>";
                for (int x = 0; x < rowNum; x++)
                {
                    tempCache = $"{tempCache}%^            <td>{obj[rowNum + currentObj]}</td>";
                    currentObj++;
                }
                tempCache = $"{tempCache}%^        </tr>"; 
            }
            textCache = $"{textCache}{tempCache}%^    </table>";
            Debug.CallObjectEvent("[cswebobj] added table");
        }
        public void AddTemplate(Template templates, string[] args = null)
        {
            Timer.StartTimer();
            textCache = $"{textCache}    {Templates.Templates.RenderTemplate(templates, args)}";
            Debug.CallObjectEvent("[cswebobj] added template (generic)");
        }
        public void AddCustomTemplate(string name)
        {
            Timer.StartTimer();
            if (!Templates.Templates.CustomTemplates.ContainsKey(name))
            {
                throw new TemplateError("Template does not exist!");
            }
            textCache = $"{textCache}{Templates.Templates.CustomTemplates[name]}";
            Debug.CallObjectEvent("[cswebobj] added template (custom)");
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