using System.Collections.Generic;
using System.Text;
using System;

namespace Impart
{
    /// <summary>Generate a webpage for the Website</summary>
    /// <remarks>This is the root class of Impart.</remarks>
    public class WebPage
    {
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                Changed = true;
            }
        }

        private Measurement _DefaultMargin = 0;
        public Measurement DefaultMargin
        {
            get
            {
                return _DefaultMargin;
            }
            set
            {
                _DefaultMargin = value;
                Changed = true;
            }
        }

        private Measurement _DefaultPadding = 0;
        public Measurement DefaultPadding
        {
            get
            {
                return _DefaultPadding;
            }
        }
        private List<(string id, Element element)> Elements = new List<(string id, Element element)>();
        private Dictionary<string, string> Styles = new Dictionary<string, string>();
        private List<string> Includes = new List<string>();
        private StringBuilder ScrollbarCache = new StringBuilder();
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a WebPage instance.</summary>
        protected WebPage() { }

        /// <summary>Add a Style.</summary>
        /// <param name="style">The Style instance to add.</param>
        protected void AddStyle(Style style)
        {
            Styles.Add(style.ID, style.ToString());
            Changed = true;
        }

        /// <summary>Add a Text.</summary>
        /// <param name="text">The Text instance to add.</param>
        protected void AddText(Text text)
        {
            Elements.Add((text.ID, text));
            Changed = true;
        }

        /// <summary>Add an Image.</summary>
        /// <param name="image">The Image instance to add.</param>
        protected void AddImage(Image image)
        {
            Elements.Add((image.id, image));
            Changed = true;
        }

        /// <summary>Add a Header.</summary>
        /// <param name="header">The Header instance to add.</param>
        protected void AddHeader(Header header)
        {
            Elements.Add((header.ID, header));
            Changed = true;
        }

        /// <summary>Add a Link.</summary>
        /// <param name="link">The Link instance to add.</param>
        protected void AddLink(Link link)
        {
            Elements.Add((link.id, link));
            Changed = true;
        }

        /// <summary>Add a Table.</summary>
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
            tempBuilder.Append("<table>");
            tempBuilder.Append($"<tr>");
            for (int x = 0; x < rowNum; x++)
            {
                tempBuilder.Append($"<th>{obj[x]}</th>");
            }
            tempBuilder.Append($"</tr>"); 
            int vertRowNum = (int)Math.Round(Convert.ToDouble(((double)obj.Length - (double)rowNum) / (double)rowNum), MidpointRounding.AwayFromZero);
            if ((obj.Length - rowNum) % rowNum > 0)
            {
                int _currentobj = 0;
                for (int a = 0; a < vertRowNum + 1; a++)
                {
                    tempBuilder.Append($"<tr>");
                    for (int x = 0; x < rowNum; x++)
                    {
                        if (obj.Length <= rowNum + _currentobj)
                        {
                            tempBuilder.Append($"</tr>");
                            textBuilder.Append($"</table>");
                            return;
                        }
                        tempBuilder.Append($"<td>{obj[rowNum + _currentobj]}</td>");
                        _currentobj++;
                    }
                    tempBuilder.Append($"</tr>");
                }
            }
            int currentObj = 0;
            for (int a = 0; a < vertRowNum; a++)
            {
                tempBuilder.Append($"<tr>");
                for (int x = 0; x < rowNum; x++)
                {
                    tempBuilder.Append($"<td>{obj[rowNum + currentObj]}</td>");
                    currentObj++;
                }
                tempBuilder.Append($"</tr>");
            }
            textBuilder.Append($"{tempBuilder.ToString()}</table>");
            Changed = true;
        }

        /// <summary>Add a Division.</summary>
        /// <param name="division">The Division instance to add.</param>
        protected void AddDivision(Division division)
        {
            if (division.ID != null)
            {
                Styles.Add(division.ID, division.webPageStyleBuilder.ToString());
            }
            Elements.Add((division.ID, division));
            Changed = true;
        }

        /// <summary>Add a List.</summary>
        /// <param name="list">The List instance to add.</param>
        protected void AddList(List list)
        {
            Elements.Add((list.ID, list));
            Changed = true;
        }

        /// <summary>Add a Scrollbar.</summary>
        /// <param name="scrollbar">The Scrollbar instance to add.</param>
        protected void SetScrollBar(Scrollbar scrollbar)
        {
            switch (scrollbar.axis)
            {
                case Axis.X:
                    ScrollbarCache.Append(" overflow-x: auto;");
                    break;
                case Axis.Y:
                    ScrollbarCache.Append(" overflow-y: auto;");
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            ScrollbarCache.Append($"::-webkit-scrollbar {{width: {scrollbar.width};background-color: #808080; }}::-webkit-scrollbar-track{{background-color: {scrollbar.bgColor};}}::-webkit-scrollbar-thumb {{background-color: {scrollbar.fgColor};");
            if (scrollbar.radius != null)
            {
                ScrollbarCache.Append($"border-radius: {scrollbar.radius}%;}}");
            }
            else
            {
                ScrollbarCache.Append('}');
            }
            Changed = true;
        }

        /// <summary>Add <paramref name="form"/> to the WebPage.</summary>
        /// <param name="form">The Form instance to add.</param>
        protected void AddForm(Form form)
        {
            Elements.Add((form.ID, form));
            Changed = true;
        }

        /// <summary>Add <paramref name="button"/> to the WebPage.</summary>
        /// <param name="button">The Button instance to add.</param>
        protected void AddButton(Button button)
        {
            Elements.Add((button.ID, button));
            Changed = true;
        }

        /// <summary>Add <paramref name="nest"/> to the WebPage.</summary>
        /// <param name="nest">The Nest instance to add.</param>
        protected void AddNest(Nest nest)
        {
            Elements.Add((null, nest));
            Changed = true;
        }

        /// <summary>Add an external css document to the WebPage.</summary>
        /// <param name="url">The url of the document to add.</param>
        protected void AddExternalStyle(string url)
        {
            Includes.Add(url);
            Changed = true;
        }

        /// <summary>Returns the String equivalent of this WebPage instance.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder main = new StringBuilder("<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart --><!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\">");
            foreach (string i in Includes)
            {
                main.Append($"<link rel=\"stylesheet\" href=\"{i}\">");
            }
            main.Append("<style>");
            foreach (KeyValuePair<string, string> s in Styles)
            {
                main.Append(s.Value);
            }
            main.Append($"* {{padding: {_DefaultPadding};margin: {_DefaultMargin};}}</style><body>");
            foreach ((string id, Element element) e in Elements)
            {
                main.Append(e.element);
            }
            main.Append("</body></html>");
            Render = main.ToString();
            return Render;
        }
    }
}