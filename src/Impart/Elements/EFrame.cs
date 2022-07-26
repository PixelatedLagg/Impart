using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Embed a page into your website in the form of a frame.</summary>
    public class EFrame : IElement, INested
    {
        private int _Width;

        /// <value>The width (in pixels).</value>
        public int Width
        {
            get => _Width;
            set
            {
                _Width = value;
            }
        }
        private int _Height;

        /// <value>The height (in pixels).</value>
        public int Height
        {
            get => _Height;
            set
            {
                _Height = value;
            }
        }
        private string _Source;

        /// <value>The source link of the page to embed.</value>
        public string Source
        {
            get => _Source;
            set
            {
                _Source = value;
            }
        }
        /// <value>The Attr values of the instance.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the instance.</value>
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
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates an EFrame instance.</summary>
        /// <param name="width">The width (in pixels).</param>
        /// <param name="height">The height (in pixels).</param>
        /// <param name="source">The source link of the page to embed.</param>
        public EFrame(int width, int height, string source)
        {
            _Width = width;
            _Height = height;
            _Source = source;
        }

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
            StringBuilder result = new StringBuilder($"<iframe width=\"{_Width}\" height=\"{_Height}\" src=\"{_Source}\"");
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
            Render = result.Append("></iframe>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            EFrame result = new EFrame(_Width, _Height, _Source);
            result._IOID = _IOID;
            return result;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            EFrame result = new EFrame(_Width, _Height, _Source);
            result._IOID = _IOID;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            if (!Changed && !Attrs.Changed && !ExtAttrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            ExtAttrs.Changed = false;
            StringBuilder result = new StringBuilder($"<iframe width=\"{_Width}\" height=\"{_Height}\" src=\"{_Source}\"");
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
            Render = result.Append('>').ToString();
            return Render;
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</iframe>";
        }
    }
}