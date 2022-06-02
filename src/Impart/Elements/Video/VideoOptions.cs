namespace Impart
{
    public class VideoOptions
    {
        public readonly bool ShowControls;
        public readonly bool Autoplay;
        public readonly bool Mute;
        public VideoOptions(bool showControls, bool autoplay, bool mute)
        {
            ShowControls = showControls;
            Autoplay = autoplay;
            Mute = mute;
        }
    }
}