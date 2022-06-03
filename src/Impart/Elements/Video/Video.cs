using System.Text;
using Impart.Internal;

namespace Impart
{
    public struct Video : Element
    {
        private string _Source;
        public string Source
        {
            get
            {
                return _Source;
            }
            set
            {
                Changed = true;
                _Source = value;
            }
        }
        private (Length Width, Length Height) _Size;
        public (Length Width, Length Height) Size
        {
            get
            {
                return _Size;
            }
            set
            {
                Changed = true;
                _Size = value;
            }
        }
        private VideoOptions _Options;
        public VideoOptions Options
        {
            get
            {
                return _Options;
            }
            set
            {
                Changed = true;
                _Options = value;
            }
        }

        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }
        
        /// <value>The Attr values of the Text.</value>
        public AttrList Attrs = new AttrList();

        /// <value>The ExtAttr values of the Text.</value>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <value>The ExtAttr values of the instance.</value>
        ExtAttrList Element.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }
        private bool Changed = true;
        private string Render = "";
        public Video(string url, Length width, Length height, VideoOptions options)
        {
            _Source = url;
            _Size = (width, height);
            _Options = options;
        }
        public override string ToString()
        {
            if (!Changed && !Attrs.Changed && !ExtAttrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            ExtAttrs.Changed = false;
            StringBuilder result = new StringBuilder($"<video src=\"{_Source}\" width=\"{_Size.Width}\" height=\"{_Size.Height}\"{(_Options.Autoplay ? " autoplay " : "")}{(_Options.ShowControls ? " controls " : "")}{(_Options.Mute ? " muted " : "")}");
            if (Attrs.Count != 0)
            {
                result.Append("style=\"");
                foreach (Attr attr in Attrs)
                {
                    result.Append(attr);
                }
                result.Append('"');
            }
            foreach (ExtAttr extAttrs in ExtAttrs)
            {
                result.Append(extAttrs);
            }
            Render = result.Append('>').ToString();
            return Render;
        }
        Element Element.Clone()
        {
            Video result = new Video();
            result._IOID = _IOID;
            result._Options = _Options;
            result._Size = _Size;
            result._Source = _Source;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            return result;
        }
    }
}