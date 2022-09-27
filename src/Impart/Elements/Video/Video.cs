using System.Text;
using Impart.Internal;
using Impart.Scripting;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Video element.</summary>
    public class Video : IElement, INested
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

        /// <summary>The Video source file.</summary>
        public string Source;

        /// <summary>The Video player size.</summary>
        public (Length Width, Length Height) Size;

        /// <summary>Options for the Video player.</summary>
        public VideoOptions Options;
        
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

        internal int _IOID = Ioid.Generate();
        internal EventManager _Events = new EventManager();

        /// <summary>Creates a Video instance.</summary>
        /// <param name="source">The Video source file.</param>
        /// <param name="width">The Video player width.</param>
        /// <param name="height">The Video player height.</param>
        /// <param name="options">Options for the Video player.</param>
        public Video(string source, Length width, Length height, VideoOptions options)
        {
            Source = source;
            Size = (width, height);
            Options = options;
        }

        internal Video(int ioid)
        {
            _IOID = ioid;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"<video src=\"{Source}\" width=\"{Size.Width}\" height=\"{Size.Height}\"{(Options.Autoplay ? " autoplay " : "")}{(Options.ShowControls ? " controls " : "")}{(Options.Mute ? " muted " : "")}");
            if (Attrs.Count != 0)
            {
                result.Append("style=\"");
                foreach (Attr attr in Attrs)
                {
                    result.Append(attr);
                }
                result.Append($"\"class=\"{_IOID}\"{_Events}");
            }
            foreach (ExtAttr extAttrs in ExtAttrs)
            {
                result.Append(extAttrs);
            }
            return result.Append("></video>").ToString();
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Video result = new Video(Source, Size.Width, Size.Height, Options);
            result._IOID = _IOID;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Video result = new Video(Source, Size.Width, Size.Height, Options);
            result._IOID = _IOID;
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            string render = ToString();
            return render.Remove(render.Length - 8);
        }

        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return "</video>";
        }
    }
}