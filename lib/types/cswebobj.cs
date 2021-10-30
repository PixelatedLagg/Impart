using System.Collections;
using System;
using Csweb.Templates;

namespace Csweb
{
    public class cswebobj
    {
        internal string path;
        internal string textCache;
        internal string cssPath;
        private string styleCache;
        public cswebobj(string path, string cssPath)
        {
            CSwebconfig.Initialize();
            Timer.StartTimer();
            this.path = path;
            textCache = "";
            this.cssPath = cssPath;
            styleCache = $"body {{%^    padding: 0;%^    margin: 0;%^}}";
            Debug.CallObjectEvent("[cswebobj] created cswebobj");
        }
        public void AddStyle(style style)
        {
            Timer.StartTimer();
            if (styleCache == "")
            {
                styleCache = $"{style.Render()}";
            }
            else
            {
                styleCache += $"%^{style.Render()}";
            }
            Debug.CallObjectEvent("[cswebobj] added style");
        }
        public void AddText(Text text)
        {
            Timer.StartTimer();
            if (text.id == null)
            {
                textCache += $"%^    <p{text.attributes}{text.style}>{text.text}</p>";
            }
            else
            {
                textCache += $"%^    <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>";
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
            textCache += $"%^    <title>{title}</title>";
            Debug.CallObjectEvent("[cswebobj] set title");
        }
        public void AddImage(Image image)
        {
            Timer.StartTimer();
            if (image.id == null)
            {
                textCache += $"%^    <img src=\"{image.path}\"{image.attributes}{image.style}>";
            }
            else
            {
                textCache += $"%^    <img src=\"{image.path}\" id=\"{image.id}\"{image.attributes}{image.style}>";
            }
            Debug.CallObjectEvent("[cswebobj] added image element");
        }
        public void AddHeader(Header header)
        {
            Timer.StartTimer();
            if (header.id == null)
            {
                textCache += $"%^    <h{header.num}{header.attributes}{header.style}>{header.text}</h{header.num}>";
            }
            else
            {
                textCache += $"%^    <h{header.num} id=\"{header.id}\"{header.attributes}{header.style}>{header.text}</h{header.num}>";
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
                        textCache += $"%^    <a href=\"{link.path}\">%^        <img src=\"{link.image.path}\" {link.image.style}>%^    </a>";
                        break;
                    case (string, string) a when a.Item1 != null && a.Item2 != null:
                        textCache += $"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style}>%^    </a>";
                        break;
                    case (string, string) b when b.Item1 == null && b.Item2 != null:
                        textCache += $"%^    <a href=\"{link.path}\">%^        <img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style}>%^    </a>";
                        break;
                    case (string, string) c when c.Item1 != null && c.Item2 == null:
                        textCache += $"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <img src=\"{link.image.path}\" {link.image.style}>%^    </a>";
                        break;
                }
            }
            else
            {
                switch (link.id, link.text.id)
                {
                    case (null, null):
                        textCache += $"%^    <a href=\"{link.path}\">%^        <p>{link.text.text}</p>%^    </a>";
                        break;
                    case (string, string) a when a.Item1 != null && a.Item2 != null:
                        textCache += $"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <p id=\"{link.image.id}\">{link.text.text}</p>%^    </a>";
                        break;
                    case (string, string) b when b.Item1 == null && b.Item2 != null:
                        textCache += $"%^    <a href=\"{link.path}\">%^        <p id=\"{link.image.id}\">{link.text.text}</p>%^    </a>";
                        break;
                    case (string, string) c when c.Item1 != null && c.Item2 == null:
                        textCache += $"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <p>{link.text.text}</p>%^    </a>";
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
            tempCache += $"%^        <tr>";
            for (int x = 0; x < rowNum; x++)
            {
                tempCache += $"%^            <th>{obj[x]}</th>";
            }
            tempCache += $"%^        </tr>"; 
            int vertRowNum = (int)Math.Round(System.Convert.ToDouble(((double)obj.Length - (double)rowNum) / (double)rowNum), MidpointRounding.AwayFromZero);
            if ((obj.Length - rowNum) % rowNum > 0)
            {
                int _currentobj = 0;
                for (int a = 0; a < vertRowNum + 1; a++)
                {
                    tempCache += $"%^        <tr>";
                    for (int x = 0; x < rowNum; x++)
                    {
                        if (obj.Length <= rowNum + _currentobj)
                        {
                            tempCache += $"%^        </tr>"; 
                            textCache += $"%^    </table>";
                            Debug.CallObjectEvent("[cswebobj] added table");
                            return;
                        }
                        tempCache += $"%^            <td>{obj[rowNum + _currentobj]}</td>";
                        _currentobj++;
                    }
                    tempCache += $"%^        </tr>"; 
                }
            }
            int currentObj = 0;
            for (int a = 0; a < vertRowNum; a++)
            {
                tempCache += $"%^        <tr>";
                for (int x = 0; x < rowNum; x++)
                {
                    tempCache += $"%^            <td>{obj[rowNum + currentObj]}</td>";
                    currentObj++;
                }
                tempCache += $"%^        </tr>"; 
            }
            textCache += $"{tempCache}%^    </table>";
            Debug.CallObjectEvent("[cswebobj] added table");
        }
        public void AddTemplate(Template templates, string[] args = null)
        {
            Timer.StartTimer();
            textCache += $"    {Templates.Templates.RenderTemplate(templates, args)}";
            Debug.CallObjectEvent("[cswebobj] added template (generic)");
        }
        public void AddCustomTemplate(string name)
        {
            Timer.StartTimer();
            if (!Templates.Templates.CustomTemplates.ContainsKey(name))
            {
                throw new TemplateError("Template does not exist!");
            }
            textCache += $"{Templates.Templates.CustomTemplates[name]}";
            Debug.CallObjectEvent("[cswebobj] added template (custom)");
        }
        public void AddDivision(Division div)
        {
            textCache += $"{div.textCache}";
        }
        public void Render()
        {
            Timer.StartTimer();
            Common.Change(path, $"<!-- Generated by CSWeb - https://github.com/PixelatedLagg/CSWeb-lib -->"
            + $"{Environment.NewLine}<html xmlns=\"http://www.w3.org/1999/xhtml\">{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{cssPath}\">{Environment.NewLine}    <body>"
            + $"{textCache.Replace("%^", $"{Environment.NewLine}    ")}{Environment.NewLine}    </body>{Environment.NewLine}</html>");
            Common.Change(cssPath, styleCache.Replace("%^", Environment.NewLine));
            if (Debug.Formatting == false)
            {
                Common.Change(path, Common.GetAllText(path).Replace("    ", ""));
                Common.Change(cssPath, Common.GetAllText(cssPath).Replace("    ", ""));
            }
            Debug.CallObjectEvent("[cswebobj] rendered cswebobj");
        }
    }
}