namespace Impart.Scripting
{
    /// <summary>The event arguments passed when the user hits a key.</summary>
    public struct KeyEventArgs
    {
        /// <summary>The key type.</summary>
        public Key key;

        /// <summary>Create a KeyEventArgs instance.</summary>
        /// <returns>A KeyEventArgs instance.</returns>
        /// <param name="key">The key type.</param>
        public KeyEventArgs(Key key)
        {
            this.key = key;
        }
    }
}