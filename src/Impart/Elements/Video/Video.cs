using System.Text;

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
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            Render = $"<video src=\"{_Source}\" width=\"{_Size.Width}\" height=\"{_Size.Height}\"{(_Options.Autoplay ? " autoplay" : "")}{(_Options.ShowControls ? " controls" : "")}{(_Options.Mute ? " muted" : "")}>";
            return Render;
        }
    }
}