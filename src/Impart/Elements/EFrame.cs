using System.Text;
using Impart.Internal;

namespace Impart
{
    public class EFrame : IElement, INested
    {
        private int _Width;
        public int Width
        {
            get => _Width;
            set
            {
                _Width = value;
            }
        }
        private int _Height;
        public int Height
        {
            get => _Height;
            set
            {
                _Height = value;
            }
        }
        private string _Source;
        public string Source
        {
            get => _Source;
            set
            {
                _Source = value;
            }
        }
        /// <value>The Attr values of the Header.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the Header.</value>
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
        IElement IElement.Clone()
        {
            EFrame result = new EFrame(_Width, _Height, _Source);
            result._IOID = _IOID;
            return result;
        }
        public IElement Clone()
        {
            EFrame result = new EFrame(_Width, _Height, _Source);
            result._IOID = _IOID;
            return result;
        }

        public string First()
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
            return result.Append('>').ToString();
        }

        public string Last()
        {
            return "</iframe>";
        }
    }
}