using System.Linq;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Impart
{
    /// <summary>Class that represents the html page.</summary>
    public class WebPage
    {
        internal string path;
        internal string textCache;
        internal string cssPath;
        internal string bodyStyle;
        internal string razorPath;
        private string styleCache;
        private int defaultMargin;
        internal bool rendered;

        /// <summary>Constructor for the html/css webpage class.</summary>
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
            razorPath = null;
            rendered = false;
            Debug.CallObjectEvent("[webpage] created cswebobj");
        }

        /// <summary>Constructor for the html/css/c# webpage class.</summary>
        public WebPage(string path)
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
            razorPath = path;
            this.path = null;
            textCache = "";
            styleCache = "";
            bodyStyle = "";
            defaultMargin = 0;
            Debug.CallObjectEvent("[webpage] created cswebobj");
        }

        /// <summary>Method for adding a style to the webpage.</summary>
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

        /// <summary>Method for adding text to the webpage.</summary>
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

        /// <summary>Method for setting the webpage title.</summary>
        public void SetTitle(string title)
        {
            Timer.StartTimer();
            if (String.IsNullOrEmpty(title))
            {
                throw new WebPageError("Title cannot be null or empty!");
            }
            textCache += $"%^    <title>{title}</title>";
            Debug.CallObjectEvent("[webpage] set title");
        }

        /// <summary>Method for adding an image to the webpage.</summary>
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

        /// <summary>Method for adding a header to the webpage.</summary>
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

        /// <summary>Method for adding a link to the webpage.</summary>
        public void AddLink(Link link)
        {
            Timer.StartTimer();
            if (link.linkType == typeof(Image))
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
            Debug.CallObjectEvent("[webpage] added link");
        }

        /// <summary>Method for adding a table to the webpage.</summary>
        public void AddTable(int rowNum, params string[] obj)
        {
            Timer.StartTimer();
            if (rowNum > obj.Length)
            {
                throw new WebPageError("Number of table rows cannot be bigger than number of table entries!");
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

        /// <summary>Method for adding a division to the webpage.</summary>
        public void AddDivision(Division div)
        {
            Timer.StartTimer();
            textCache += div.textCache;
            styleCache += div.cssCache;
            Debug.CallObjectEvent("[webpage] added division");
        }

        /// <summary>Method for adding a list to the webpage.</summary>
        public void AddList(List list)
        {
            Timer.StartTimer();
            textCache += list.Render();
            Debug.CallObjectEvent("[webpage] added division");
        }

        /// <summary>Method for setting the webpage scrollbar.</summary>
        public void SetScrollBar(Scrollbar scrollbar)
        {
            Timer.StartTimer();
            bodyStyle += scrollbar.bodyCache;
            styleCache += scrollbar.cssCache;
            Debug.CallObjectEvent("[webpage] set scrollbar");
        }

        /// <summary>Method for adding a form to the webpage.</summary>
        public void AddForm(Form form)
        {
            Timer.StartTimer();
            textCache += form.Render();
            Debug.CallObjectEvent("[webpage] added form");
        }

        /// <summary>Method for adding a button to the webpage.</summary>
        public void AddButton(Button button)
        {
            Timer.StartTimer();
            if (button.id == null)
            {
                textCache += $"%^    <button{button.attributes}{button.style}>{button.text}</button>";
            }
            else
            {
                textCache += $"%^    <button id=\"{button.id}\"{button.attributes}{button.style}>{button.text}</button>";
            }
            Debug.CallObjectEvent("[webpage] added button");
        }

        /// <summary>Method for setting the webpage scrollbar.</summary>
        public void SetDefaultMargin(int pixels)
        {
            Timer.StartTimer();
            if (pixels <= 0)
            {
                throw new WebPageError("Margin pixel value must be above 0!");
            }
            defaultMargin = pixels;
            Debug.CallObjectEvent("[webpage] set default margin");
        }

        /// <summary>Method for rendering the webpage.</summary>
        public void Render()
        {
            Timer.StartTimer();
            if (styleCache != "")
            {
                styleCache += $"%^* {{%^    padding: 0;%^    margin: {defaultMargin}px;%^{bodyStyle}}}";
            }
            if (razorPath == null)
            {
                Common.Change(path, $"<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart -->{Environment.NewLine}<!DOCTYPE html>"
                + $"{Environment.NewLine}<html xmlns=\"http://www.w3.org/1999/xhtml\">{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{cssPath}\">{Environment.NewLine}    <body>"
                + $"{textCache.Replace("%^", $"{Environment.NewLine}    ")}{Environment.NewLine}    </body>{Environment.NewLine}</html>");
                Common.Change(cssPath, styleCache.Replace("%^", Environment.NewLine));
            }
            else
            {
                Common.Change(razorPath, $"@page \"/{razorPath.Replace(".razor", "").Split("/").Last()}\"{Environment.NewLine}<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart -->"
                + $"{Environment.NewLine}<link rel=\"stylesheet\" href=\"{cssPath}\">{Environment.NewLine}<style>{Environment.NewLine}{styleCache.Replace("%^", Environment.NewLine) + Environment.NewLine}</style>{Environment.NewLine}<body>"
                + $"{textCache.Replace("%^", $"{Environment.NewLine}")}{Environment.NewLine}</body>");
            }
            if (!Debug.Formatting)
            {
                Common.Change(path, Common.GetAllText(path).Replace("    ", ""));
                Common.Change(cssPath, Common.GetAllText(cssPath).Replace("    ", ""));
            }
            rendered = true;
            Debug.CallObjectEvent("[webpage] rendered webpage");
        }
        internal string RenderRazor()
        {
            return ($"@page \"/{razorPath.Replace(".razor", "").Split("/").Last()}\"{Environment.NewLine}<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart -->"
                + $"{Environment.NewLine}<link rel=\"stylesheet\" href=\"{cssPath}\">{Environment.NewLine}<style>{Environment.NewLine}{styleCache.Replace("%^", Environment.NewLine) + Environment.NewLine}</style>{Environment.NewLine}<body>"
                + $"{textCache.Replace("%^", $"{Environment.NewLine}")}{Environment.NewLine}</body>");
        }
    }
}