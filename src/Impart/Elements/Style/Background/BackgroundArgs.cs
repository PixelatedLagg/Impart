namespace Impart
{
    /// <summary>Arguments for setting the background image.</summary>
    public class BackgroundArgs
    {
        /// <value>The background image.</value>
        public readonly string Image;

        /// <value>The background image options.</value>
        public readonly Background Background;

        /// <summary>Creates a BackgroundArgs instance.</summary>
        /// <param name="image">The background image.</param>
        /// <param name="background">The background image options.</param>
        public BackgroundArgs(string image, Background background)
        {
            Image = image;
            Background = background;
        }
    }
}