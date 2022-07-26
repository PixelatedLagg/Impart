using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Video element.</summary>
    public struct Video : IElement, INested
    {
        private string _Source;

        /// <value>The Video source file.</value>
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

        /// <value>The Video player size.</value>
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

        /// <value>Options for the Video player.</value>
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

        /// <summary>Creates a Video instance.</summary>
        /// <param name="source">The Video source file.</param>
        /// <param name="width">The Video player width.</param>
        /// <param name="height">The Video player height.</param>
        /// <param name="options">Options for the Video player.</param>
        public Video(string source, Length width, Length height, VideoOptions options)
        {
            _Source = source;
            _Size = (width, height);
            _Options = options;
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
            Render = result.Append("></video>").ToString();
            return Render;
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement Clone()
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

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
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

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            return ToString().Remove(Render.Length - 8);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</video>";
        }
    }
}