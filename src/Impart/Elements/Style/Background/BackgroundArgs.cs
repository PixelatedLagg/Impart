namespace Impart
{
    /// <summary>Arguments for setting the background image.</summary>
    public class BackgroundArgs
    {
        /// <summary>The background image.</summary>
        public readonly string Image;

        /// <summary>The background image options.</summary>
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