using System.Text;
using System;

namespace Impart
{
    /// <summary>Class that represents the html page.</summary>
    public class WebPage
    {
        internal string path;
        internal string cssPath;
        private int defaultMargin;
        internal StringBuilder textBuilder;
        internal StringBuilder styleBuilder;
        internal StringBuilder bodyBuilder;

        /// <summary>Constructor for the WebPage class.</summary>
        protected WebPage(string path, string cssPath)
        {
            this.path = path;
            this.cssPath = cssPath;
            defaultMargin = 0;
            textBuilder = new StringBuilder(1000);
            styleBuilder = new StringBuilder(1000);
            bodyBuilder = new StringBuilder(1000);
        }

        internal void WriteText(string text)
        {
            if (text.Length + textBuilder.Length > textBuilder.Capacity)
            {
                textBuilder.Capacity += 1000;
            }
            textBuilder.Append(text);
        }
        internal void WriteStyle(string text)
        {
            if (text.Length + styleBuilder.Length > styleBuilder.Capacity)
            {
                styleBuilder.Capacity += 1000;
            }
            styleBuilder.Append(text);
        }
        internal void WriteBody(string text)
        {
            if (text.Length + bodyBuilder.Length > bodyBuilder.Capacity)
            {
                bodyBuilder.Capacity += 1000;
            }
            bodyBuilder.Append(text);
        }

        /// <summary>Add a style to the webpage.</summary>
        protected void AddStyle(Style style)
        {
            if (styleBuilder.ToString() == "")
            {
                WriteStyle($"{style.Render()}");
            }
            else
            {
                WriteStyle($"%^{style.Render()}");
            }
        }

        /// <summary>Add text to the webpage.</summary>
        protected void AddText(Text text)
        {
            if (text.id == null)
            {
                Console.WriteLine($"%^    <p{text.attributes}{text.style}>{text.text}</p>");
                WriteText($"%^    <p{text.attributes}{text.style}>{text.text}</p>");
            }
            else
            {
                WriteText($"%^    <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>");
            }
        }

        /// <summary>Set the webpage title.</summary>
        protected void SetTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ImpartError("Title cannot be null or empty!");
            }
            WriteText($"%^    <title>{title}</title>");
        }

        /// <summary>Add an image to the webpage.</summary>
        protected void AddImage(Image image)
        {
            if (image.id == null)
            {
                WriteText($"%^    <img src=\"{image.path}\"{image.attributes}{image.style}>");
            }
            else
            {
                WriteText($"%^    <img src=\"{image.path}\" id=\"{image.id}\"{image.attributes}{image.style}>");
            }
        }

        /// <summary>Add a header to the webpage.</summary>
        protected void AddHeader(Header header)
        {
            if (header.id == null)
            {
                WriteText($"%^    <h{header.num}{header.attributes}{header.style}>{header.text}</h{header.num}>");
            }
            else
            {
                WriteText($"%^    <h{header.num} id=\"{header.id}\"{header.attributes}{header.style}>{header.text}</h{header.num}>");
            }
        }

        /// <summary>Add a link to the webpage.</summary>
        protected void AddLink(Link link)
        {
            if (link.linkType == typeof(Image))
            {
                switch (link.id, link.image.id)
                {
                    case (null, null):
                        WriteText($"%^    <a href=\"{link.path}\">%^        <img src=\"{link.image.path}\" {link.image.style}>%^    </a>");
                        break;
                    case (string, string) a when a.Item1 != null && a.Item2 != null:
                        WriteText($"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style}>%^    </a>");
                        break;
                    case (string, string) b when b.Item1 == null && b.Item2 != null:
                        WriteText($"%^    <a href=\"{link.path}\">%^        <img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style}>%^    </a>");
                        break;
                    case (string, string) c when c.Item1 != null && c.Item2 == null:
                        WriteText($"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <img src=\"{link.image.path}\" {link.image.style}>%^    </a>");
                        break;
                }
            }
            else
            {
                switch (link.id, link.text.id)
                {
                    case (null, null):
                        WriteText($"%^    <a href=\"{link.path}\">%^        <p>{link.text.text}</p>%^    </a>");
                        break;
                    case (string, string) a when a.Item1 != null && a.Item2 != null:
                        WriteText($"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <p id=\"{link.image.id}\">{link.text.text}</p>%^    </a>");
                        break;
                    case (string, string) b when b.Item1 == null && b.Item2 != null:
                        WriteText($"%^    <a href=\"{link.path}\">%^        <p id=\"{link.image.id}\">{link.text.text}</p>%^    </a>");
                        break;
                    case (string, string) c when c.Item1 != null && c.Item2 == null:
                        WriteText($"%^    <a href=\"{link.path}\" id=\"{link.id}\">%^        <p>{link.text.text}</p>%^    </a>");
                        break;
                }
            }
        }

        /// <summary>Add a table to the webpage.</summary>
        protected void AddTable(int rowNum, params string[] obj)
        {
            if (rowNum > obj.Length)
            {
                throw new ImpartError("Number of table rows cannot be bigger than number of table entries!");
            }
            int length = 0;
            foreach (string s in obj)
            {
                length += s.Length;
            }
            StringBuilder tempBuilder = new StringBuilder(length * (rowNum + 10));
            tempBuilder.Append("%^    <table>");
            tempBuilder.Append($"%^        <tr>");
            for (int x = 0; x < rowNum; x++)
            {
                tempBuilder.Append($"%^            <th>{obj[x]}</th>");
            }
            tempBuilder.Append($"%^        </tr>"); 
            int vertRowNum = (int)Math.Round(System.Convert.ToDouble(((double)obj.Length - (double)rowNum) / (double)rowNum), MidpointRounding.AwayFromZero);
            if ((obj.Length - rowNum) % rowNum > 0)
            {
                int _currentobj = 0;
                for (int a = 0; a < vertRowNum + 1; a++)
                {
                    tempBuilder.Append($"%^        <tr>");
                    for (int x = 0; x < rowNum; x++)
                    {
                        if (obj.Length <= rowNum + _currentobj)
                        {
                            tempBuilder.Append($"%^        </tr>");
                            WriteText($"%^    </table>");
                            return;
                        }
                        tempBuilder.Append($"%^            <td>{obj[rowNum + _currentobj]}</td>");
                        _currentobj++;
                    }
                    tempBuilder.Append($"%^        </tr>");
                }
            }
            int currentObj = 0;
            for (int a = 0; a < vertRowNum; a++)
            {
                tempBuilder.Append($"%^        <tr>");
                for (int x = 0; x < rowNum; x++)
                {
                    tempBuilder.Append($"%^            <td>{obj[rowNum + currentObj]}</td>");
                    currentObj++;
                }
                tempBuilder.Append($"%^        </tr>");
            }
            WriteText($"{tempBuilder.ToString()}%^    </table>");
        }

        /// <summary>Add a division to the webpage.</summary>
        protected void AddDivision(Division div)
        {
            WriteText(div.textCache);
            WriteStyle(div.cssCache);
        }

        /// <summary>Add a list to the webpage.</summary>
        protected void AddList(List list)
        {
            WriteText(list.Render());
        }

        /// <summary>Set the webpage scrollbar.</summary>
        protected void SetScrollBar(Scrollbar scrollbar)
        {
            WriteBody(scrollbar.bodyCache);
            WriteStyle(scrollbar.cssCache);
        }

        /// <summary>Add a form to the webpage.</summary>
        protected void AddForm(Form form)
        {
            WriteText(form.Render());
        }

        /// <summary>Add a button to the webpage.</summary>
        protected void AddButton(Button button)
        {
            if (button.id == null)
            {
                WriteText($"%^    <button{button.attributes}{button.style}>{button.text}</button>");
            }
            else
            {
                WriteText($"%^    <button id=\"{button.id}\"{button.attributes}{button.style}>{button.text}</button>");
            }
        }

        /// <summary>Set the webpage scrollbar.</summary>
        protected void SetDefaultMargin(int pixels)
        {
            if (pixels <= 0)
            {
                throw new ImpartError("Margin pixel value must be above 0!");
            }
            defaultMargin = pixels;
        }

        /// <summary>Render the webpage.</summary>
        protected void Render()
        {
            if (styleBuilder.ToString() != "")
            {
                WriteStyle($"%^* {{%^    padding: 0;%^    margin: {defaultMargin}px;%^{bodyBuilder.ToString()}}}");
            }
            File.Change(path, $"<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart -->{Environment.NewLine}<!DOCTYPE html>{Environment.NewLine}<html xmlns=\"http://www.w3.org/1999/xhtml\">{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{cssPath}\">{Environment.NewLine}    <body>{textBuilder.Replace("%^", $"{Environment.NewLine}    ").ToString()}{Environment.NewLine}    </body>{Environment.NewLine}</html>");
            File.Change(cssPath, styleBuilder.Replace("%^", Environment.NewLine).ToString());
        }
    }
}