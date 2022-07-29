namespace Impart
{
    /// <summary>Arguments for setting the Video player.</summary>
    public class VideoOptions
    {
        /// <summary>Whether to show or hide the controls.</summary>
        public readonly bool ShowControls;

        /// <summary>Whether to autoplay the Video.</summary>
        public readonly bool Autoplay;

        /// <summary>Whether to start the Video muted.</summary>
        public readonly bool Mute;

        /// <summary>Creates a VideoOptions instance.</summary>
        /// <param name="showControls">Whether to show or hide the controls.</param>
        /// <param name="autoplay">Whether to autoplay the Video.</param>
        /// <param name="mute">Whether to start the Video muted.</param>
        public VideoOptions(bool showControls, bool autoplay, bool mute)
        {
            ShowControls = showControls;
            Autoplay = autoplay;
            Mute = mute;
        }
    }
}