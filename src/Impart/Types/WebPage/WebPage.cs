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

        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The Attribute values of the WebPage.</value>
        public List<Attribute> Attributes
        {
            get 
            {
                return _Attributes;
            }
        }

        private List<(string id, Element element)> _Elements = new List<(string id, Element element)>();
        private Dictionary<string, string> _Styles = new Dictionary<string, string>();
        private List<string> _Includes = new List<string>();
        private StringBuilder _ScrollbarCache = new StringBuilder();
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a WebPage instance.</summary>
        protected WebPage() { }

        protected Element GetElement(string id)
        {
            foreach ((string _id, Element _e) element in _Elements)
            {
                if (element._id == id)
                {
                    return element._e;
                }
            }
            return null;
        }

        protected void RemoveElement(string id)
        {
            foreach ((string _id, Element _e) element in _Elements)
            {
                if (element._id == id)
                {
                    _Elements.Remove(element);
                }
            }
        }

        protected void RemoveElement(Element e)
        {
            foreach ((string _id, Element _e) element in _Elements)
            {
                if (element._e == e)
                {
                    _Elements.Remove(element);
                }
            }
        }

        /// <summary>Add a Style.</summary>
        /// <param name="style">The Style instance to add.</param>
        protected void AddStyle(Style style)
        {
            _Styles.Add(style.ID, style.ToString());
            Changed = true;
        }

        /// <summary>Add a Text.</summary>
        /// <param name="text">The Text instance to add.</param>
        protected void AddText(Text text)
        {
            _Elements.Add((text.ID, text));
            Changed = true;
        }

        /// <summary>Add an Image.</summary>
        /// <param name="image">The Image instance to add.</param>
        protected void AddImage(Image image)
        {
            _Elements.Add((image.ID, image));
            Changed = true;
        }

        /// <summary>Add a Header.</summary>
        /// <param name="header">The Header instance to add.</param>
        protected void AddHeader(Header header)
        {
            _Elements.Add((header.ID, header));
            Changed = true;
        }

        /// <summary>Add a Link.</summary>
        /// <param name="link">The Link instance to add.</param>
        protected void AddLink(Link link)
        {
            _Elements.Add((link.ID, link));
            Changed = true;
        }

        /// <summary>Add a Table.</summary>
        /// <param name="rowNum">The number of rows.</param>
        /// <param name="obj">An array of strings to add as entries.</param>
        protected void AddTable(int rowNum, params string[] obj)
        {
            /*if (rowNum > obj.Length)
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
            Changed = true;*/
        }

        /// <summary>Add a Division.</summary>
        /// <param name="division">The Division instance to add.</param>
        protected void AddDivision(Division division)
        {
            if (division.ID != null)
            {
                _Styles.Add(division.ID, division._ScrollbarCache.ToString());
            }
            _Elements.Add((division.ID, division));
            Changed = true;
        }

        /// <summary>Add a List.</summary>
        /// <param name="list">The List instance to add.</param>
        protected void AddList(List list)
        {
            _Elements.Add((list.ID, list));
            Changed = true;
        }

        /// <summary>Add a Scrollbar.</summary>
        /// <param name="scrollbar">The Scrollbar instance to add.</param>
        protected void SetScrollBar(Scrollbar scrollbar)
        {
            _ScrollbarCache.Clear();
            switch (scrollbar.axis)
            {
                case Axis.X:
                    _Attributes.Add(new Attribute(AttributeType.OverflowX, true));
                    break;
                case Axis.Y:
                    _Attributes.Add(new Attribute(AttributeType.OverflowY, true));
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            _ScrollbarCache.Append($"::-webkit-scrollbar {{width: {scrollbar.width};background-color: #808080; }}::-webkit-scrollbar-track{{background-color: {scrollbar.bgColor};}}::-webkit-scrollbar-thumb {{background-color: {scrollbar.fgColor};");
            if (scrollbar.radius != null)
            {
                _ScrollbarCache.Append($"border-radius: {scrollbar.radius};}}");
            }
            else
            {
                _ScrollbarCache.Append('}');
            }
            Changed = true;
        }

        protected void RemoveScrollBar()
        {
            _ScrollbarCache.Clear();
        }

        /// <summary>Add <paramref name="form"/> to the WebPage.</summary>
        /// <param name="form">The Form instance to add.</param>
        protected void AddForm(Form form)
        {
            _Elements.Add((form.ID, form));
            Changed = true;
        }

        /// <summary>Add <paramref name="button"/> to the WebPage.</summary>
        /// <param name="button">The Button instance to add.</param>
        protected void AddButton(Button button)
        {
            _Elements.Add((button.ID, button));
            Changed = true;
        }

        /// <summary>Add <paramref name="nest"/> to the WebPage.</summary>
        /// <param name="nest">The Nest instance to add.</param>
        protected void AddNest(Nest nest)
        {
            _Elements.Add((null, nest));
            Changed = true;
        }

        /// <summary>Add an external css document to the WebPage.</summary>
        /// <param name="url">The url of the document to add.</param>
        protected void AddExternalStyle(string url)
        {
            _Includes.Add(url);
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
            StringBuilder result = new StringBuilder("<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart --><!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\">");
            foreach (string i in _Includes)
            {
                result.Append($"<link rel=\"stylesheet\" href=\"{i}\">");
            }
            result.Append($"<style>{_ScrollbarCache.ToString()}");
            foreach (KeyValuePair<string, string> s in _Styles)
            {
                result.Append(s.Value);
            }
            result.Append($"* {{padding: {_DefaultPadding};margin: {_DefaultMargin};}}</style><body");
            foreach (Attribute attribute in _Attributes)
            {
                result.Append(attribute);
            }
            result.Append('>');
            foreach ((string id, Element element) e in _Elements)
            {
                result.Append(e.element);
            }
            Render = result.Append("</body></html>").ToString();
            return Render;
        }
    }
}