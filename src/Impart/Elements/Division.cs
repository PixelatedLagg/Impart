using System;
using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Division element.</summary>
    public struct Division : IElement, IDisposable, INested
    {
        /// <value>The Attr values of the Division.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the Division.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <value>The ExtAttr values of the instance.</value>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }
        internal List<IStyleElement> _StyleElements = new List<IStyleElement>();
        private List<IElement> _Elements = new List<IElement>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a Division instance.</summary>
        public Division() { }

        /// <summary>Add a Text to the Division.</summary>
        /// <param name="text">The Text instance to add.</param>
        public Division AddText(Text text)
        {
            _Elements.Add(text);
            Changed = true;
            return this;
        }

        /// <summary>Add a Image to the Division.</summary>
        /// <param name="image">The Image instance to add.</param>
        public Division AddImage(Image image)
        {
            _Elements.Add(image);
            Changed = true;
            return this;
        }

        /// <summary>Add a Header to the Division.</summary>
        /// <param name="header">The Header instance to add.</param>
        public Division AddHeader(Header header)
        {
            _Elements.Add(header);
            Changed = true;
            return this;
        }

        /// <summary>Add a Link to the Division.</summary>
        /// <param name="link">The Link instance to add.</param>
        public Division AddLink(Link link)
        {
            _Elements.Add(link);
            Changed = true;
            return this;
        }

        /// <summary>Add a Table to the Division.</summary>
        /// <param name="table">The Table instance to add.</param>
        public Division AddTable(Table table)
        {
            _Elements.Add(table);
            Changed = true;
            return this;
        }

        /// <summary>Add a Division to the Division.</summary>
        /// <param name="division">The Division instance to add.</param>
        public Division AddDivision(Division division)
        {
            _Elements.Add(division);
            Changed = true;
            return this;
        }

        /// <summary>Add a Form to the Division.</summary>
        /// <param name="form">The Form instance to add.</param>
        public Division AddForm(Form form)
        {
            _Elements.Add(form);
            Changed = true;
            return this;
        }

        /// <summary>Add a Button to the Division.</summary>
        /// <param name="button">The Button instance to add.</param>
        public Division AddButton(Button button)
        {
            _Elements.Add(button);
            Changed = true;
            return this;
        }

        /// <summary>Add a Nest to the Division.</summary>
        /// <param name="nest">The Nest instance to add.</param>
        public Division AddNest(Nest nest)
        {
            _Elements.Add(nest);
            Changed = true;
            return this;
        }

        /// <summary>Add a Video to the Division.</summary>
        /// <param name="video">The Video instance to add.</param>
        public Division AddVideo(Video video)
        {
            _Elements.Add(video);
            Changed = true;
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
            Changed = true;
            return this;
        }

        /// <summary>Sets a Scrollbar to the Division.</summary>
        /// <param name="scrollbar">The scrollbar to add.</param>
        public Division SetScrollbar(Scrollbar scrollbar)
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
            _StyleElements.Add(new DivisionScrollbar(scrollbar, _IOID.ToString(), ((IStyleElement)(scrollbar)).IOID));
            Changed = true;
            return this;
        }

        /// <summary>Dispose of the Division instance.</summary>
        public void Dispose() { }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed && !Attrs.Changed && !ExtAttrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            ExtAttrs.Changed = false;
            StringBuilder result = new StringBuilder("<div ");
            if (Attrs.Count != 0)
            {
                result.Append("style=\"");
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
            Render = result.Append("</div>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Division result = new Division();
            result.Attrs = Attrs;
            result._Elements = _Elements;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._StyleElements = _StyleElements;
            result.Render = Render;
            return result;
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
            result.Render = Render;
            return result;
        }

        string INested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - 6);
        }

        string INested.Last()
        {
            return "</div>";
        }
    } 
}