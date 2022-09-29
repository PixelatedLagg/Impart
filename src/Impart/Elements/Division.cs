using System;
using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Division element.</summary>
    public class Division : IElement, IDisposable, INested
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                foreach (ExtAttr ext in ExtAttrs)
                {
                    if (ext.Type == ExtAttrType.ID)
                    {
                        return ext.Value;
                    }
                }
                return null;
            }
        }
        
        /// <summary>The Attr values of the instance.</summary>
        public List<Attr> Attrs { get; set; } = new List<Attr>();

        /// <summary>The ExtAttr values of the instance.</summary>
        public List<ExtAttr> ExtAttrs { get; set; } = new List<ExtAttr>();

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        internal int _IOID;
        internal EventManager _Events = new EventManager();
        internal List<IStyleElement> _StyleElements = new List<IStyleElement>();
        private List<IElement> _Elements = new List<IElement>();

        /// <summary>Creates a Division instance.</summary>
        public Division() 
        {
            _IOID = Ioid.Generate();
        }

        internal Division(int ioid)
        {
            _IOID = ioid;
        }

        /// <summary>Add a Text to the Division.</summary>
        /// <param name="text">The Text instance to add.</param>
        public Division Add(Text text)
        {
            _Elements.Add(text);
            return this;
        }

        /// <summary>Add a Image to the Division.</summary>
        /// <param name="image">The Image instance to add.</param>
        public Division Add(Image image)
        {
            _Elements.Add(image);
            return this;
        }

        /// <summary>Add a Header to the Division.</summary>
        /// <param name="header">The Header instance to add.</param>
        public Division Add(Header header)
        {
            _Elements.Add(header);
            return this;
        }

        /// <summary>Add a Link to the Division.</summary>
        /// <param name="link">The Link instance to add.</param>
        public Division Add(Link link)
        {
            _Elements.Add(link);
            return this;
        }

        /// <summary>Add a Table to the Division.</summary>
        /// <param name="table">The Table instance to add.</param>
        public Division Add(Table table)
        {
            _Elements.Add(table);
            return this;
        }

        /// <summary>Add a Division to the Division.</summary>
        /// <param name="division">The Division instance to add.</param>
        public Division Add(Division division)
        {
            _Elements.Add(division);
            return this;
        }

        /// <summary>Add a Form to the Division.</summary>
        /// <param name="form">The Form instance to add.</param>
        public Division Add(Form form)
        {
            _Elements.Add(form);
            return this;
        }

        /// <summary>Add a Button to the Division.</summary>
        /// <param name="button">The Button instance to add.</param>
        public Division Add(Button button)
        {
            _Elements.Add(button);
            return this;
        }

        /// <summary>Add a Nest to the Division.</summary>
        /// <param name="nest">The Nest instance to add.</param>
        public Division Add(Nest nest)
        {
            _Elements.Add(nest);
            return this;
        }

        /// <summary>Add a Video to the Division.</summary>
        /// <param name="video">The Video instance to add.</param>
        public Division Add(Video video)
        {
            _Elements.Add(video);
            return this;
        }

        /// <summary>Add an EFrame to the Division.</summary>
        /// <param name="eFrame">The EFrame instance to add.</param>
        public Division Add(EFrame eFrame)
        {
            _Elements.Add(eFrame);
            return this;
        }

        /// <summary>Sets a Style to the Division.</summary>
        /// <param name="style">The Style instance to use.</param>
        public Division SetStyle(Style style)
        {
            foreach (Attr attribute in style.Attrs)
            {
                Attrs.Add(attribute);
            }
            return this;
        }

        /// <summary>Sets a Scrollbar to the Division.</summary>
        /// <param name="scrollbar">The scrollbar to add.</param>
        public Division SetScrollbar(Scrollbar scrollbar)
        {
            switch (scrollbar.Axis)
            {
                case Axis.X:
                    Attrs.Add(new Attr(AttrType.OverflowX, true));
                    break;
                case Axis.Y:
                    Attrs.Add(new Attr(AttrType.OverflowY, true));
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            _StyleElements.Add(new DivisionScrollbar(scrollbar, _IOID.ToString(), ((IStyleElement)(scrollbar)).IOID));
            return this;
        }

        /// <summary>Dispose of the Division instance.</summary>
        public void Dispose() { }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<div class=\"{_IOID}\"{_Events}");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            result.Append('>');
            foreach (IElement e in _Elements)
            {
                result.Append(e);
            }
            return result.Append("</div>").ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Division result = new Division();
            result.Attrs = Attrs;
            result._Elements = _Elements;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._StyleElements = _StyleElements;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Division result = new Division();
            result.Attrs = Attrs;
            result._Elements = _Elements;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._StyleElements = _StyleElements;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            string render = ToString();
            return render.Remove(render.Length - 6);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</div>";
        }
    } 
}