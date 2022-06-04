namespace Impart
{
    /// <summary>Arguments for setting the Video player.</summary>
    public class VideoOptions
    {
        /// <value>Whether to show or hide the controls.</value>
        public readonly bool ShowControls;

        /// <value>Whether to autoplay the Video.</value>
        public readonly bool Autoplay;

        /// <value>Whether to start the Video muted.</value>
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