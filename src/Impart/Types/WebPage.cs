using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Impart
{
    public class WebPage
    {
        internal string path;
        internal string textCache;
        internal string cssPath;
        internal string bodyStyle;
        private string styleCache;
        private int defaultMargin;
        public WebPage(string path, string cssPath)
        {
            Timer.StartTimer();
            if (Debug.TimesConfig == null)
            {
                string fullName = "";
                Type declaringType;
                int skipFrames = 1;
                do
                {
                    MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                    declaringType = method.DeclaringType;
                    if (declaringType == null)
                    {
                        if (Type.GetType(method.Name)?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance) != null)
                        {
                            Debug.ObjectEvent += (Action<Log>)Delegate.CreateDelegate(typeof(Action<Log>), null, Type.GetType(method.Name).GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance));
                        }
                        ImpartConfig.Initialize();
                        return;
                    }
                    skipFrames++;
                    fullName = declaringType.FullName;
                }
                while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));
                if (Type.GetType(fullName)?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance) != null)
                {
                    Debug.ObjectEvent += (Action<Log>)Delegate.CreateDelegate(typeof(Action<Log>), null, Type.GetType(fullName)?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance));
                }
                ImpartConfig.Initialize();
                Debug.TimesConfig = 1;
            }
            this.path = path;
            textCache = "";
            this.cssPath = cssPath;
            styleCache = "";
            bodyStyle = "";
            defaultMargin = 0;
            Debug.CallObjectEvent("[webpage] created cswebobj");
        }
        public void AddStyle(Style style)
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
            Debug.CallObjectEvent("[webpage] added style");
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
            Debug.CallObjectEvent("[webpage] added text element");
        }
        public void SetTitle(string title)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(title))
            {
                throw new WebPageError("Title cannot be null or empty!", this);
            }
            textCache += $"%^    <title>{title}</title>";
            Debug.CallObjectEvent("[webpage] set title");
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
            Debug.CallObjectEvent("[webpage] added image element");
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
            Debug.CallObjectEvent("[webpage] added header");
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
                throw new WebPageError("Number of table rows cannot be bigger than number of table entries!", this);
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
                            Debug.CallObjectEvent("[webpage] added table");
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
            Debug.CallObjectEvent("[webpage] added table");
        }
        public void AddDivision(Division div)
        {
            textCache += div.textCache;
            styleCache += div.cssCache;
        }
        public void AddList(List list)
        {
            textCache += list.Render();
        }
        public void SetScrollBar(Scrollbar scrollbar)
        {
            bodyStyle += scrollbar.bodyCache;
            styleCache += scrollbar.cssCache;
        }
        public void AddForm(Form form)
        {
            textCache += form.Render();
        }
        public void SetDefaultMargin(int pixels)
        {
            if (pixels <= 0)
            {
                throw new WebPageError("Margin pixel value must be above 0!", this);
            }
            defaultMargin = pixels;
        }
        public void Render()
        {
            Timer.StartTimer();
            if (styleCache != "")
            {
                styleCache += $"%^* {{%^    padding: 0;%^    margin: {defaultMargin}px;%^{bodyStyle}}}";
            }
            Common.Change(path, $"<!-- Generated by CSWeb - https://github.com/PixelatedLagg/CSWeb-lib -->{Environment.NewLine}<!DOCTYPE html>"
            + $"{Environment.NewLine}<html xmlns=\"http://www.w3.org/1999/xhtml\">{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{cssPath}\">{Environment.NewLine}    <body>"
            + $"{textCache.Replace("%^", $"{Environment.NewLine}    ")}{Environment.NewLine}    </body>{Environment.NewLine}</html>");
            Common.Change(cssPath, styleCache.Replace("%^", Environment.NewLine));
            if (!Debug.Formatting)
            {
                Common.Change(path, Common.GetAllText(path).Replace("    ", ""));
                Common.Change(cssPath, Common.GetAllText(cssPath).Replace("    ", ""));
            }
            Debug.CallObjectEvent("[webpage] rendered webpage");
        }
    }
}