using System.Text;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Generate a webpage for a Website.</summary>
    public class WebPage
    {
        private string _Title;

        /// <value>The title value of the WebPage.</value>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                Changed = true;
                _Title = value;
            }
        }

        private Measurement _DefaultMargin = 0;

        /// <value>The default margin value.</value>
        public Measurement DefaultMargin
        {
            get
            {
                return _DefaultMargin;
            }
            set
            {
                Changed = true;
                _DefaultMargin = value;
            }
        }

        private Measurement _DefaultPadding = 0;

        /// <value>The default padding value.</value>
        public Measurement DefaultPadding
        {
            get
            {
                return _DefaultPadding;
            }
            set
            {
                Changed = true;
                _DefaultPadding = value;
            }
        }

        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The Attribute values of the WebPage.</value>
        public Attribute[] Attributes
        {
            get 
            {
                return _Attributes.ToArray();
            }
        }

        internal Script _Script = new Script("");
        private List<Element> _Elements = new List<Element>();
        private List<StyleElement> _StyleElements = new List<StyleElement>();
        private Dictionary<string, string> _Styles = new Dictionary<string, string>();
        private List<string> _Includes = new List<string>();
        private StringBuilder _ScrollbarCache = new StringBuilder();
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a WebPage instance.</summary>
        protected WebPage() { }

        /// <summary>Check if an Element exists.</summary>
        /// <param name="element">The Element's instance.</param>
        protected bool ElementExists(Element element)
        {
            foreach (Element entry in _Elements)
            {
                if (entry.IOID == element.IOID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Check if a StyleElement exists.</summary>
        /// <param name="element">The StyleElement's instance.</param>
        protected bool StyleElementExists(StyleElement element)
        {
            foreach (StyleElement entry in _StyleElements)
            {
                if (entry.IOID == element.IOID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Check if an Element exists by ID.</summary>
        /// <param name="id">The ID to check.</param>
        protected bool ElementExistsByID(string id)
        {
            foreach (Element entry in _Elements)
            {
                if (entry.ID == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Get the Element by ID.</summary>
        /// <param name="id">The Element's ID.</param>
        protected Element GetElementByID(string id)
        {
            foreach (Element entry in _Elements)
            {
                if (entry.ID == id)
                {
                    return entry;
                }
            }
            return null;
        }

        /// <summary>Remove the Element by ID.</summary>
        /// <param name="id">The Element's ID.</param>
        protected void RemoveElementByID(string id)
        {
            foreach (Element entry in _Elements.ToArray())
            {
                if (entry.ID == id)
                {
                    Changed = true;
                    _Elements.Remove(entry);
                }
            }
        }

        /// <summary>Remove the Element.</summary>
        /// <param name="element">The Element's instance.</param>
        protected void RemoveElement(Element element)
        {
            foreach (Element entry in _Elements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _Elements.Remove(entry);
                }
            }
        }

        /// <summary>Remove the StyleElement.</summary>
        /// <param name="element">The StyleElement's instance.</param>
        protected void RemoveStyleElement(StyleElement element)
        {
            foreach (StyleElement entry in _Elements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _StyleElements.Remove(entry);
                }
            }
        }

        /// <summary>Change the Element.</summary>
        /// <param name="element">The Element's instance.</param>
        protected void ChangeElement(Element element)
        {
            foreach (Element entry in _Elements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _Elements[_Elements.IndexOf(entry)] = element;
                }
            }
        }

        /// <summary>Change the StyleElement.</summary>
        /// <param name="element">The StyleElement's instance.</param>
        protected void ChangeStyleElement(StyleElement element)
        {
            foreach (StyleElement entry in _StyleElements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _StyleElements[_StyleElements.IndexOf(entry)] = element;
                }
            }
        }

        /// <summary>Add a Style to the WebPage.</summary>
        /// <param name="style">The Style instance to add.</param>
        protected void AddStyle(Style style)
        {
            _Styles.Add(style.ID, style.ToString());
            Changed = true;
        }

        /// <summary>Add a Text to the WebPage.</summary>
        /// <param name="text">The Text instance to add.</param>
        protected void AddText(Text text)
        {
            _Elements.Add(text);
            Changed = true;
        }

        /// <summary>Add an Image to the WebPage.</summary>
        /// <param name="image">The Image instance to add.</param>
        protected void AddImage(Image image)
        {
            _Elements.Add(image);
            Changed = true;
        }

        /// <summary>Add a Header to the WebPage.</summary>
        /// <param name="header">The Header instance to add.</param>
        protected void AddHeader(Header header)
        {
            _Elements.Add(header);
            Changed = true;
        }

        /// <summary>Add a Link to the WebPage.</summary>
        /// <param name="link">The Link instance to add.</param>
        protected void AddLink(Link link)
        {
            _Elements.Add(link);
            Changed = true;
        }

        /// <summary>Add a Table to the WebPage.</summary>
        /// <param name="table">The Table instance to add.</param>
        protected void AddTable(Table table)
        {
            _Elements.Add(table);
            Changed = true;
        }

        /// <summary>Add a Division to the WebPage.</summary>
        /// <param name="division">The Division instance to add.</param>
        protected void AddDivision(Division division)
        {
            if (division.ID != null)
            {
                _Styles.Add(division.ID, division._ScrollbarCache.ToString());
            }
            _Elements.Add(division);
            Changed = true;
        }

        /// <summary>Add a List to the WebPage.</summary>
        /// <param name="list">The List instance to add.</param>
        protected void AddList(List list)
        {
            _Elements.Add(list);
            Changed = true;
        }

        /// <summary>Add a Scrollbar.</summary>
        /// <param name="scrollbar">The Scrollbar instance to add.</param>
        protected void SetScrollBar(Scrollbar scrollbar)
        {
            switch (scrollbar.Axis)
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
            _StyleElements.Add(scrollbar);
            Changed = true;
        }

        /// <summary>Remove the Scrollbar from the WebPage.</summary>
        protected void RemoveScrollBar()
        {
            _ScrollbarCache.Clear();
        }

        /// <summary>Add a Form to the WebPage.</summary>
        /// <param name="form">The Form instance to add.</param>
        protected void AddForm(Form form)
        {
            _Elements.Add(form);
            Changed = true;
        }

        /// <summary>Add a Button to the WebPage.</summary>
        /// <param name="button">The Button instance to add.</param>
        protected void AddButton(Button button)
        {
            _Elements.Add(button);
            Changed = true;
        }

        /// <summary>Add a Nest to the WebPage.</summary>
        /// <param name="nest">The Nest instance to add.</param>
        protected void AddNest(Nest nest)
        {
            _Elements.Add(nest);
            Changed = true;
        }

        /// <summary>Add an external CSS document to the WebPage.</summary>
        /// <param name="url">The URL of the document to add.</param>
        protected void AddExternalStyle(string url)
        {
            _Includes.Add(url);
            Changed = true;
        }

        /// <summary>Add an Animation to the WebPage.</summary>
        /// <param name="animation">The Animation to add.</param>
        protected void AddAnimation(Animation animation)
        {
            _StyleElements.Add(animation);
            Changed = true;
        }

        /// <summary>Returns the instance as a String.</summary>
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
            result.Append($"<meta charset=\"UTF-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta name\"viewport\" content=\"width=device-width, initial-scale=1.0\"><style>{_ScrollbarCache.ToString()}");
            foreach (KeyValuePair<string, string> s in _Styles)
            {
                result.Append(s.Value);
            }
            foreach (Element element in _StyleElements)
            {
                result.Append(element);
            }
            result.Append($"* {{padding: {_DefaultPadding};margin: {_DefaultMargin};}}</style><body");
            foreach (Attribute attribute in _Attributes)
            {
                result.Append(attribute);
            }
            result.Append('>');
            foreach (Element entry in _Elements)
            {
                result.Append(entry);
            }
            Render = result.Append($"</body>{_Script}</html>").ToString();
            return Render;
        }
    }
}