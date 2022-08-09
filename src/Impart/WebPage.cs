using System.Linq;
using System.Text;
using Impart.Scripting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

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
        internal Script _Script;
        private List<IElement> _Elements = new List<IElement>();
        private List<IStyleElement> _StyleElements = new List<IStyleElement>();
        private List<string> _Styles = new List<string>();
        private List<string> _ExternalStyles = new List<string>();
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a WebPage instance.</summary>
        protected WebPage() { }

        /// <summary>Gets the specified IElement from the WebPage.</summary>
        /// <param name="selector">The specifications to be met by the IElement.</param>
        protected T1 Get<T1>(Func<T1, bool> selector) where T1 : class, IElement
        {
            foreach (IElement element in _Elements)
            {
                if (element as T1 == null)
                {
                    continue;
                }
                if (selector.Invoke((T1)element))
                {
                    return (T1)element;
                }
            }
            return null;
        }

        /// <summary>Gets the many specified IElements from the WebPage.</summary>
        /// <param name="selector">The specifications to be met by the IElements.</param>
        protected T1[] GetMany<T1>(Func<T1, bool> selector) where T1 : class, IElement
        {
            List<T1> results = new List<T1>();
            foreach (IElement element in _Elements)
            {
                if (element as T1 == null)
                {
                    continue;
                }
                if (selector.Invoke((T1)element))
                {
                    results.Add((T1)element);
                }
            }
            return results.ToArray();
        }

        /// <summary>Checks if an Element exists.</summary>
        /// <param name="element">The Element reference to check.</param>
        protected bool Exists(ElementRef element)
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

        /// <summary>Checks if an Element exists.</summary>
        /// <param name="element">The Element instance to check.</param>
        protected bool Exists(IElement element)
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

        /// <summary>Checks if an IStyleElement exists.</summary>
        /// <param name="element">The instance of the IStyleElement to check.</param>
        protected bool Exists(IStyleElement element)
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

        /// <summary>Removes an IElement.</summary>
        /// <param name="element">The instance of the IElement to remove.</param>
        protected void Remove(IElement element)
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

        /// <summary>Removes an IStyleElement.</summary>
        /// <param name="element">The instance of the IStyleElement to remove.</param>
        protected void Remove(IStyleElement element)
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

        /// <summary>Edits an IElement.</summary>
        /// <param name="element">The instance of the IElement to edit.</param>
        protected void Edit(IElement element)
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

        /// <summary>Edits an IStyleElement.</summary>
        /// <param name="element">The instance of the IStyleElement to edit.</param>
        protected void Edit(IStyleElement element)
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

        /// <summary>Adds a Style.</summary>
        /// <param name="style">The Style instance to add.</param>
        protected void Add(Style style)
        {
            _Styles.Add(style.ToString());
            Changed = true;
        }

        /// <summary>Removes a Style.</summary>
        /// <param name="index">The index of the Style to remove.</param>
        protected void Add(int index)
        {
            _Styles.RemoveAt(index);
            Changed = true;
        }

        /// <summary>Adds a Text to the WebPage.</summary>
        /// <param name="text">The Text instance to add.</param>
        protected void Add(Text text)
        {
            _Elements.Add(text);
            Changed = true;
        }

        /// <summary>Adds an Image to the WebPage.</summary>
        /// <param name="image">The Image instance to add.</param>
        protected void Add(Image image)
        {
            _Elements.Add(image);
            Changed = true;
        }

        /// <summary>Adds a Header to the WebPage.</summary>
        /// <param name="header">The Header instance to add.</param>
        protected void Add(Header header)
        {
            _Elements.Add(header);
            Changed = true;
        }

        /// <summary>Adds a Link to the WebPage.</summary>
        /// <param name="link">The Link instance to add.</param>
        protected void Add(Link link)
        {
            _Elements.Add(link);
            Changed = true;
        }

        /// <summary>Adds a Table to the WebPage.</summary>
        /// <param name="table">The Table instance to add.</param>
        protected void Add(Table table)
        {
            _Elements.Add(table);
            Changed = true;
        }

        /// <summary>Adds a Division to the WebPage.</summary>
        /// <param name="division">The Division instance to add.</param>
        protected void Add(Division division)
        {
            _Elements.Add(division);
            foreach (IStyleElement element in division._StyleElements)
            {
                _StyleElements.Add(element);
            }
            Changed = true;
        }

        /// <summary>Adds a List to the WebPage.</summary>
        /// <param name="list">The List instance to add.</param>
        protected void Add(List list)
        {
            _Elements.Add(list);
            Changed = true;
        }

        /// <summary>Adds a Scrollbar to the WebPage.</summary>
        /// <param name="scrollbar">The Scrollbar instance to add.</param>
        protected void Add(Scrollbar scrollbar)
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

        /// <summary>Adds a Form to the WebPage.</summary>
        /// <param name="form">The Form instance to add.</param>
        protected void Add(Form form)
        {
            _Elements.Add(form);
            Changed = true;
        }

        /// <summary>Adds a Button to the WebPage.</summary>
        /// <param name="button">The Button instance to add.</param>
        protected void Add(Button button)
        {
            _Elements.Add(button);
            Changed = true;
        }

        /// <summary>Adds a Nest to the WebPage.</summary>
        /// <param name="nest">The Nest instance to add.</param>
        protected void Add(Nest nest)
        {
            _Elements.Add(nest);
            Changed = true;
        }

        /// <summary>Adds a Video to the WebPage.</summary>
        /// <param name="video">The Video instance to add.</param>
        protected void Add(Video video)
        {
            _Elements.Add(video);
            Changed = true;
        }

        /// <summary>Adds an external Style to the WebPage.</summary>
        /// <param name="url">The URL of the external Style to add.</param>
        protected void AddExternalStyle(string url)
        {
            _ExternalStyles.Add(url);
            Changed = true;
        }

        /// <summary>Adds an Animation to the WebPage.</summary>
        /// <param name="animation">The Animation to add.</param>
        protected void Add(Animation animation)
        {
            _StyleElements.Add(animation);
            Changed = true;
        }

        /// <summary>Adds multiple Texts to the WebPage.</summary>
        /// <param name="texts">The Texts to add.</param>
        protected void AddMany(params Text[] texts)
        {
            foreach (Text text in texts)
            {
                Add(text);
            }
        }

        /// <summary>Adds multiple Images to the WebPage.</summary>
        /// <param name="images">The Images to add.</param>
        protected void AddMany(params Image[] images)
        {
            foreach (Image image in images)
            {
                Add(image);
            }
        }

        /// <summary>Adds multiple Headers to the WebPage.</summary>
        /// <param name="headers">The Headers to add.</param>
        protected void AddMany(params Header[] headers)
        {
            foreach (Header header in headers)
            {
                Add(header);
            }
        }

        /// <summary>Adds multiple Links to the WebPage.</summary>
        /// <param name="links">The Links to add.</param>
        protected void AddMany(params Link[] links)
        {
            foreach (Link link in links)
            {
                Add(link);
            }
        }

        /// <summary>Adds multiple Tables to the WebPage.</summary>
        /// <param name="tables">The Tables to add.</param>
        protected void AddMany(params Table[] tables)
        {
            foreach (Table table in tables)
            {
                Add(table);
            }
        }

        /// <summary>Adds multiple Divisions to the WebPage.</summary>
        /// <param name="divisions">The Divisions to add.</param>
        protected void AddMany(params Division[] divisions)
        {
            foreach (Division division in divisions)
            {
                Add(division);
            }
        }

        /// <summary>Adds multiple Lists to the WebPage.</summary>
        /// <param name="lists">The Lists to add.</param>
        protected void AddMany(params List[] lists)
        {
            foreach (List list in lists)
            {
                Add(list);
            }
        }

        /// <summary>Adds multiple Forms to the WebPage.</summary>
        /// <param name="forms">The Forms to add.</param>
        protected void AddMany(params Form[] forms)
        {
            foreach (Form form in forms)
            {
                Add(form);
            }
        }

        /// <summary>Adds multiple Buttons to the WebPage.</summary>
        /// <param name="buttons">The Buttons to add.</param>
        protected void AddMany(params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                Add(button);
            }
        }

        /// <summary>Adds multiple Nests to the WebPage.</summary>
        /// <param name="nests">The Nests to add.</param>
        protected void AddMany(params Nest[] nests)
        {
            foreach (Nest nest in nests)
            {
                Add(nest);
            }
        }

        /// <summary>Adds multiple Videos to the WebPage.</summary>
        /// <param name="videos">The Videos to add.</param>
        protected void AddMany(params Video[] videos)
        {
            foreach (Video video in videos)
            {
                Add(video);
            }
        }

        /// <summary>Adds multiple Animations to the WebPage.</summary>
        /// <param name="animations">The Animations to add.</param>
        protected void AddMany(params Animation[] animations)
        {
            foreach (Animation animation in animations)
            {
                Add(animation);
            }
        }

        /// <summary>Removes an external Style from the WebPage.</summary>
        /// <param name="index">The index of the URL of the external Style to remove.</param>
        protected void Remove(int index)
        {
            _ExternalStyles.RemoveAt(index);
            Changed = true;
        }

        /// <summary>Forces the WebPage to be rendered again, regardless of any new changes.</summary>
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