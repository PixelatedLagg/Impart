using System.Text;
using System;

namespace Impart
{
    /// <summary>Generate HTML and CSS with some Javascript functionality soon to be added.</summary>
    /// <remarks>This is the main class used in Impart.</remarks>
    public class WebPage
    {
        internal string path;
        internal string cssPath;
        private string defaultMargin;
        internal StringBuilder textBuilder;
        internal StringBuilder styleBuilder;
        internal StringBuilder bodyBuilder;

        /// <value>The HTML path of the WebPage.</value>
        public string HTMLPath
        {
            get
            {
                return path;
            }
        }

        /// <value>The CSS path of the WebPage.</value>
        public string CSSPATH
        {
            get
            {
                return cssPath;
            }
        }

        /// <summary>Creates a WebPage instance with <paramref name="path"/> as the HTML path and <paramref name="cssPath"/> as the CSS path.</summary>
        /// See <see cref="WebPage.Render()"/> to render the WebPage.
        /// <returns>A WebPage instance.</returns>
        /// <example>
        /// <code>
        /// public class Example : WebPage
        /// {
        ///     public Example() : base("example.html", "example.css")
        ///     {
        ///         
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <param name="path">The HTML file path.</param>
        /// <param name="cssPath">The CSS file path.</param>
        protected WebPage(string path, string cssPath)
        {
            this.path = path;
            this.cssPath = cssPath;
            defaultMargin = "0px";
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

        /// <summary>Add <paramref name="style"/> to the WebPage.</summary>
        /// See <see cref="Style.Style(ImpartCommon.StyleType, string)"/> to create a Style instance.
        /// <example>
        /// <code>
        /// AddStyle(style);
        /// </code>
        /// </example>
        /// <param name="style">The Style instance to add.</param>
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

        /// <summary>Add <paramref name="text"/> to the WebPage.</summary>
        /// See <see cref="Text.Text(string, string)"/> to create a Text instance.
        /// <example>
        /// <code>
        /// AddText(text);
        /// </code>
        /// </example>
        /// <param name="text">The Text instance to add.</param>
        protected void AddText(Text text)
        {
            switch (text.type)
            {
                case TextType.Regular:
                    if (text.id == null)
                    {
                        WriteText($"%^    <p{text.attributes}{text.style}>{text.text}</p>");
                    }
                    else
                    {
                        WriteText($"%^    <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>");
                    }
                    break;
                case TextType.Bold:
                    if (text.id == null)
                    {
                        WriteText($"%^    <b{text.attributes}{text.style}>{text.text}</b>");
                    }
                    else
                    {
                        WriteText($"%^    <b id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</b>");
                    }
                    break;
                case TextType.Delete:
                    if (text.id == null)
                    {
                        WriteText($"%^    <del{text.attributes}{text.style}>{text.text}</del>");
                    }
                    else
                    {
                        WriteText($"%^    <del id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</del>");
                    }
                    break;
                case TextType.Emphasize:
                    if (text.id == null)
                    {
                        WriteText($"%^    <em{text.attributes}{text.style}>{text.text}</em>");
                    }
                    else
                    {
                        WriteText($"%^    <em id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</em>");
                    }
                    break;
                case TextType.Important:
                    if (text.id == null)
                    {
                        WriteText($"%^    <strong{text.attributes}{text.style}>{text.text}</strong>");
                    }
                    else
                    {
                        WriteText($"%^    <strong id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</strong>");
                    }
                    break;
                case TextType.Insert:
                    if (text.id == null)
                    {
                        WriteText($"%^    <ins{text.attributes}{text.style}>{text.text}</ins>");
                    }
                    else
                    {
                        WriteText($"%^    <ins id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</ins>");
                    }
                    break;
                case TextType.Italic:
                    if (text.id == null)
                    {
                        WriteText($"%^    <i{text.attributes}{text.style}>{text.text}</i>");
                    }
                    else
                    {
                        WriteText($"%^    <i id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</i>");
                    }
                    break;
                case TextType.Mark:
                    if (text.id == null)
                    {
                        WriteText($"%^    <mark{text.attributes}{text.style}>{text.text}</mark>");
                    }
                    else
                    {
                        WriteText($"%^    <mark id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</mark>");
                    }
                    break;
                case TextType.Small:
                    if (text.id == null)
                    {
                        WriteText($"%^    <small{text.attributes}{text.style}>{text.text}</small>");
                    }
                    else
                    {
                        WriteText($"%^    <small id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</small>");
                    }
                    break;
                case TextType.Subscript:
                    if (text.id == null)
                    {
                        WriteText($"%^    <sub{text.attributes}{text.style}>{text.text}</sub>");
                    }
                    else
                    {
                        WriteText($"%^    <sub id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</sub>");
                    }
                    break;
                case TextType.Superscript:
                    if (text.id == null)
                    {
                        WriteText($"%^    <sup{text.attributes}{text.style}>{text.text}</sup>");
                    }
                    else
                    {
                        WriteText($"%^    <sup id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</sup>");
                    }
                    break;
            }
        }

        /// <summary>Set the WebPage title to <paramref name="title"/>.</summary>
        /// <example>
        /// <code>
        /// SetTitle(title);
        /// </code>
        /// </example>
        /// <param name="title">The string to set the title to.</param>
        protected void SetTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ImpartError("Title cannot be null or empty!");
            }
            WriteText($"%^    <title>{title}</title>");
        }

        /// <summary>Add <paramref name="image"/> to the WebPage.</summary>
        /// See <see cref="Image.Image(string, string)"/> to create an Image instance.
        /// <example>
        /// <code>
        /// AddImage(image);
        /// </code>
        /// </example>
        /// <param name="image">The Image instance to add.</param>
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

        /// <summary>Add <paramref name="header"/> to the WebPage.</summary>
        /// See <see cref="Header.Header(int, string, string)"/> to create a Header instance.
        /// <example>
        /// <code>
        /// AddHeader(header);
        /// </code>
        /// </example>
        /// <param name="header">The Header instance to add.</param>
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

        /// <summary>Add <paramref name="link"/> to the WebPage.</summary>
        /// See <see cref="Link.Link(Image, string, string)"/> to create a Link instance with an image.
        /// See <see cref="Link.Link(Text, string, string)"/> to create a Link instance with text.
        /// <example>
        /// <code>
        /// AddLink(link);
        /// </code>
        /// </example>
        /// <param name="link">The Link instance to add.</param>
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

        /// <summary>Add a table to the WebPage with <paramref name="rowNum"/> as the number of rows and a string[] as the entries.</summary>
        /// <example>
        /// <code>
        /// AddTable(number, entryArray);
        /// </code>
        /// </example>
        /// <param name="rowNum">The number of rows.</param>
        /// <param name="obj">An array of strings to add as entries.</param>
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

        /// <summary>Add <paramref name="div"/> to the WebPage.</summary>
        /// See <see cref="Division.Division(ImpartCommon.IDType?, string)"/> to create a Division instance.
        /// <example>
        /// <code>
        /// AddDivision(div);
        /// </code>
        /// </example>
        /// <param name="div">The Division instance to add.</param>
        protected void AddDivision(Division div)
        {
            WriteText(div.textCache);
            WriteStyle(div.cssCache);
        }

        /// <summary>Add <paramref name="list"/> to the WebPage.</summary>
        /// See <see cref="List.List(int, string, Text[])"/> to create a List instance.
        /// <example>
        /// <code>
        /// AddList(list);
        /// </code>
        /// </example>
        /// <param name="list">The List instance to add.</param>
        protected void AddList(List list)
        {
            WriteText(list.Render());
        }

        /// <summary>Add <paramref name="scrollbar"/> to the WebPage.</summary>
        /// See <see cref="Scrollbar.Scrollbar(ImpartCommon.Axis, Measurement, Color, Color, Division, Measurement)"/> to create a Scrollbar instance for a division.
        /// See <see cref="Scrollbar.Scrollbar(ImpartCommon.Axis, Measurement, Color, Color, Measurement)"/> to create a Scrollbar instance for a WebPage.
        /// <example>
        /// <code>
        /// SetScrollBar(scrollbar);
        /// </code>
        /// </example>
        /// <param name="scrollbar">The Scrollbar instance to add.</param>
        protected void SetScrollBar(Scrollbar scrollbar)
        {
            WriteBody(scrollbar.bodyCache);
            WriteStyle(scrollbar.cssCache);
        }

        /// <summary>Add <paramref name="form"/> to the WebPage.</summary>
        /// See <see cref="Form.Form()"/> to create a Form instance.
        /// <example>
        /// <code>
        /// AddForm(form);
        /// </code>
        /// </example>
        /// <param name="form">The Form instance to add.</param>
        protected void AddForm(Form form)
        {
            WriteText(form.Render());
        }

        /// <summary>Add <paramref name="button"/> to the WebPage.</summary>
        /// See <see cref="Button.Button(Text, string)"/> to create a Button instance with a Text instance.
        /// See <see cref="Button.Button(string, string)"/> to create a Button instance with default text.
        /// <example>
        /// <code>
        /// AddButton(button);
        /// </code>
        /// </example>
        /// <param name="button">The Button instance to add.</param>
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

        /// <summary>Set the default margin of the WebPage to <paramref name="size"/>.</summary>
        /// See <see cref="Pixels.Pixels(int)"/> to create a Pixels instance.
        /// See <see cref="Percent.Percent(int)"/> to create a Percent instance.
        /// <example>
        /// <code>
        /// SetDefaultMargin(size);
        /// </code>
        /// </example>
        /// <param name="size">The Measurement instance to add.</param>
        protected void SetDefaultMargin(Measurement size)
        {
            switch (size)
            {
                case Pixels pixels:
                    defaultMargin = $"{pixels.pixels}px";
                    break;
                case Percent percent:
                    defaultMargin = $"{percent.percent}%";
                    break;
            }
        }

        /// <summary>Render the WebPage.</summary>
        /// <example>
        /// <code>
        /// Render();
        /// </code>
        /// </example>
        protected void Render()
        {
            if (styleBuilder.ToString() != "")
            {
                WriteStyle($"%^* {{%^    padding: 0;%^    margin: {defaultMargin};%^{bodyBuilder.ToString()}}}");
            }
            File.Change(path, $"<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart -->{Environment.NewLine}<!DOCTYPE html>{Environment.NewLine}<html xmlns=\"http://www.w3.org/1999/xhtml\">{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{cssPath}\">{Environment.NewLine}    <body>{textBuilder.Replace("%^", $"{Environment.NewLine}    ").ToString()}{Environment.NewLine}    </body>{Environment.NewLine}</html>");
            File.Change(cssPath, styleBuilder.Replace("%^", Environment.NewLine).ToString());
        }
    }
}