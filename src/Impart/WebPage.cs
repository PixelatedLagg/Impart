using System.Text;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Generate a webpage for a Website.</summary>
    public class WebPage
    {
        private string _Title;

        /// <summary>The title value of the WebPage.</summary>
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

        private Length _DefaultMargin = 0;

        /// <summary>The default margin value.</summary>
        public Length DefaultMargin
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

        private Length _DefaultPadding = 0;

        /// <summary>The default padding value.</summary>
        public Length DefaultPadding
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


        /// <summary>The Attribute values of the WebPage.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The Font values of the WebPage.</summary>
        public FontList Fonts = new FontList();
        internal Script _Script = new Script("");
        private List<IElement> _Elements = new List<IElement>();
        private List<IStyleElement> _StyleElements = new List<IStyleElement>();
        private List<string> _Styles = new List<string>();
        private List<string> _ExternalStyles = new List<string>();
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a WebPage instance.</summary>
        protected WebPage() { }

        /// <summary>Check if an Element exists.</summary>
        /// <param name="element">The instance of the Element to check.</param>
        protected bool ElementExists(IElement element)
        {
            foreach (IElement entry in _Elements)
            {
                if (entry.IOID == element.IOID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Check if a IStyleElement exists.</summary>
        /// <param name="element">The instance of the IStyleElement to check.</param>
        protected bool StyleElementExists(IStyleElement element)
        {
            foreach (IStyleElement entry in _StyleElements)
            {
                if (entry.IOID == element.IOID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Check if an IElement exists by ID.</summary>
        /// <param name="id">The ID of the IElement to check.</param>
        protected bool ElementExistsByID(string id)
        {
            foreach (IElement entry in _Elements)
            {
                if (entry.ExtAttrs[ExtAttrType.ID].Value == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Get an IElement by ID.</summary>
        /// <param name="id">The ID of the IElement to get.</param>
        protected IElement GetElementByID(string id)
        {
            foreach (IElement entry in _Elements)
            {
                if (entry.ExtAttrs[ExtAttrType.ID].Value == id)
                {
                    return entry;
                }
            }
            return null;
        }

        /// <summary>Remove an IElement by ID.</summary>
        /// <param name="id">The ID of the IElement to remove.</param>
        protected void RemoveElementByID(string id)
        {
            foreach (IElement entry in _Elements.ToArray())
            {
                if (entry.ExtAttrs[ExtAttrType.ID].Value == id)
                {
                    Changed = true;
                    _Elements.Remove(entry);
                }
            }
        }

        /// <summary>Remove an IElement.</summary>
        /// <param name="element">The instance of the IElement to remove.</param>
        protected void RemoveElement(IElement element)
        {
            foreach (IElement entry in _Elements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _Elements.Remove(entry);
                }
            }
        }

        /// <summary>Remove a IStyleElement.</summary>
        /// <param name="element">The instance of the IStyleElement to remove.</param>
        protected void RemoveStyleElement(IStyleElement element)
        {
            foreach (IStyleElement entry in _Elements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _StyleElements.Remove(entry);
                }
            }
        }

        /// <summary>Change an IElement.</summary>
        /// <param name="element">The instance of the IElement to change.</param>
        protected void ChangeElement(IElement element)
        {
            foreach (IElement entry in _Elements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _Elements[_Elements.IndexOf(entry)] = element;
                }
            }
        }

        /// <summary>Change a IStyleElement.</summary>
        /// <param name="element">The instance of the IStyleElement to change.</param>
        protected void ChangeStyleElement(IStyleElement element)
        {
            foreach (IStyleElement entry in _StyleElements.ToArray())
            {
                if (entry.IOID == element.IOID)
                {
                    Changed = true;
                    _StyleElements[_StyleElements.IndexOf(entry)] = element;
                }
            }
        }

        /// <summary>Add a Style.</summary>
        /// <param name="style">The Style instance to add.</param>
        protected void AddStyle(Style style)
        {
            _Styles.Add(style.ToString());
            Changed = true;
        }

        /// <summary>Remove a Style.</summary>
        /// <param name="index">The index of the Style to remove.</param>
        protected void RemoveStyle(int index)
        {
            _Styles.RemoveAt(index);
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
            _Elements.Add(division);
            foreach (IStyleElement element in division._StyleElements)
            {
                _StyleElements.Add(element);
            }
            Changed = true;
        }

        /// <summary>Add a List to the WebPage.</summary>
        /// <param name="list">The List instance to add.</param>
        protected void AddList(List list)
        {
            _Elements.Add(list);
            Changed = true;
        }

        /// <summary>Add a Scrollbar to the WebPage.</summary>
        /// <param name="scrollbar">The Scrollbar instance to add.</param>
        protected void SetScrollBar(Scrollbar scrollbar)
        {
            switch (scrollbar.Axis)
            {
                case Axis.X:
                    Attrs.Add(AttrType.OverflowX, true);
                    break;
                case Axis.Y:
                    Attrs.Add(AttrType.OverflowY, true);
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            _StyleElements.Add(scrollbar);
            Changed = true;
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

        /// <summary>Add a Video to the WebPage.</summary>
        /// <param name="video">The Video instance to add.</param>
        protected void AddVideo(Video video)
        {
            _Elements.Add(video);
            Changed = true;
        }

        /// <summary>Add an external Style to the WebPage.</summary>
        /// <param name="url">The URL of the external Style to add.</param>
        protected void AddExternalStyle(string url)
        {
            _ExternalStyles.Add(url);
            Changed = true;
        }

        /// <summary>Remove an external Style from the WebPage.</summary>
        /// <param name="index">The index of the URL of the external Style to remove.</param>
        protected void RemoveExternalStyle(int index)
        {
            _ExternalStyles.RemoveAt(index);
            Changed = true;
        }

        /// <summary>Add an Animation to the WebPage.</summary>
        /// <param name="animation">The Animation to add.</param>
        protected void AddAnimation(Animation animation)
        {
            _StyleElements.Add(animation);
            Changed = true;
        }

        /// <summary>Force the WebPage to be rendered again, regardless of any new changes.</summary>
        protected void ForceRender()
        {
            Changed = true;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed && !Attrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            StringBuilder result = new StringBuilder("<!-- Generated by Impart - https://github.com/PixelatedLagg/Impart --><!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\">");
            foreach (string url in _ExternalStyles)
            {
                result.Append($"<link type=\"text/css\" rel=\"stylesheet\" href=\"{url}\">");
            }
            foreach (string url in GlobalStyles.ExternalStyles)
            {
                result.Append($"<link type=\"text/css\" rel=\"stylesheet\" href=\"{url}\">");
            }
            result.Append($"<meta charset=\"UTF-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><style>");
            foreach (Font font in Fonts)
            {
                result.Append($"@font-face {{{font}}}");
            }
            foreach (string style in _Styles)
            {
                result.Append(style);
            }
            foreach (IStyleElement element in _StyleElements)
            {
                result.Append(element);
            }
            result.Append($"* {{padding: {_DefaultPadding};margin: {_DefaultMargin};}}</style>");
            if (Attrs.Count != 0)
            {
                result.Append("<body style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append("\">");
            }
            else
            {
                result.Append("<body>");
            }
            foreach (IElement entry in _Elements)
            {
                result.Append(entry);
            }
            Render = result.Append($"</body>{_Script}</html>").ToString();
            return Render;
        }
    }
}